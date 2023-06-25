using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Rigidbody2D�ϐ�
    Rigidbody2D rbody;
    //����
    float axisH = 0.0f;
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;

    //�ړ�������R���|�[�l���g������
    private Rigidbody2D myRigidbody;

    //�ړ��ʁi�萔�j
    private float VELOCITY = 2.5f;

    //�W�����v�ʁi�萔�j
    private float JUMPPOWER = 6.0f;

    //�n��
    private bool isGround = false;

    //���n�ł��郌�C���[
    public LayerMask groundLayer;

    //�ړ��\
    private bool isRun = true;

    //�U���I�u�W�F�N�g
    private GameObject attackObject;

    //�v���C���[���G���@���F�u�P�v�@�E�F�Q�@�ACOM�@���F-1�A�E�F�u�|2�v
    public int assign;

    //�U����Prefab
    public GameObject attackPrefab;

    //HPmaga
    private GameObject hpManager;
    //����(�C���X�y�N�^�[�ŃN���X�����Ă��������j
    public GameObject otherPlayer;
    //��ԁi�X�e�[�g�}�V���j
    private int stateNumber;
    //�ėp�^�C�}�[
    private float timerCounter;
    //debag�e�L�X�g
    public GameObject stateText;

    //---------------------------------------------------
    //�X�^�[�g
    //---------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //Rigidbody�R���|�[�l���g���擾
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //HP�}�l�[�W���I�u�W�F�N�g
        hpManager = GameObject.Find("HPManager");

        //�q�I�u�W�F�N�g���擾
        //this.attackObject = transform.Find("Attack").gameObject;

        //�Փ˔���dake���f�B�X�G�[�u��
        //this.attackObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    //�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q
    //�v���C���[
    //�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q
    void Player()
    {
        //�ړ����\
        if (isRun)
        {
            //�ړ�
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //�W�����v
                if (Input.GetKey(KeyCode.Space) && isGround)
                {
                    //���ړ��{�W�����v
                    this.myRigidbody.velocity = new Vector2(-VELOCITY, JUMPPOWER);
                }
                else
                {
                    myAnimator.SetInteger("Run", -1);

                    //���ړ�
                    this.myRigidbody.velocity = new Vector2(-VELOCITY, this.myRigidbody.velocity.y);
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                //�W�����v
                if (Input.GetKey(KeyCode.Space) && isGround)
                {
                    //�E�ړ��{�W�����v
                    this.myRigidbody.velocity = new Vector2(VELOCITY, JUMPPOWER);
                }
                else
                {
                    myAnimator.SetInteger("Run", 1);

                    //�E�ړ�
                    this.myRigidbody.velocity = new Vector2(VELOCITY, this.myRigidbody.velocity.y);
                }
            }
            else
            {
                myAnimator.SetInteger("Run", 0);

                //��~
                this.myRigidbody.velocity = new Vector2(0.0f, this.myRigidbody.velocity.y);

                //�W�����v
                if (Input.GetKey(KeyCode.Space) && isGround)
                {
                    //�����W�����v
                    this.myRigidbody.velocity = new Vector2(0.0f, JUMPPOWER);
                }
            }
        }
        else
        {
            //��~
            this.myRigidbody.velocity = new Vector2(0.0f, this.myRigidbody.velocity.y);
        }

        //�U��
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //�����Ȃ�����
            isRun = false;

            myAnimator.SetTrigger("PreAttack");
        }


    }

    //----------------------------------------------------
    //�֗��Ȋ֐�
    //----------------------------------------------------

    //���������߂�
    float getLength(Vector2 current, Vector2 target)
    {
        return Mathf.Sqrt(((current.x - target.x) * (current.x - target.x)) + ((current.y - target.y) * (current.y - target.y)));
    }
    //���������߂� ���I�C���[�i-180�`0�`+180)
    float getDirection(Vector2 current, Vector2 target)
    {
        Vector3 value = target - current;
        return Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg; //���W�A�����I�C���[
    }

    //----------------------------------------------------
    //�v���C���[�iCOM)
    //----------------------------------------------------
    void PlayerComuter()
    {
        //����̋���
        float length = getLength(this.transform.position, otherPlayer.transform.position);
        //Debug.Log("length" + length);
        //�f�o�b�O ""�͕������Ƃ�������
        stateText.GetComponent<Text>().text = "" + stateNumber;
        if (isRun)
        {
            //�����Ȃ�����
            //isRun = false;
            //myAnimator.SetTrigger("PreAttack");

            //���
            switch(stateNumber)
            {
                //�ҋ@�i�X�^�[�g�j
                case 0:
                    {
                        if(timerCounter > 3.0f)
                        {
                            //��Ԃ�J��
                            stateNumber = 1;
                        }
                    }break;
                //�����߂Â�
                case 1:
                    {
                        //�v���C���[��������
                        if (length > 5.0f)
                        {
                            myAnimator.SetInteger("Run", 1);
                            this.myRigidbody.velocity = new Vector2(VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);
                            
                        }
                        else
                        {
                            //��~
                            myAnimator.SetInteger("Run", 0);
                            this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);

                            //���Z�b�g
                            timerCounter = 0.0f;
                            //��Ԃ�J��
                            stateNumber = 2;
                        }
                    }
                    break;

                    //�ҋ@(���̍s���l����j
                    case 2:
                    {
                        //�^�C�}�[
                        if (timerCounter > 3.0f)
                        {
                            //����������ߕt���Ă�����
                            if (length < 3.0f)
                            {
                                //���Z�b�g
                                timerCounter = 0.0f;
                                //��Ԃ�J��
                                stateNumber = 4;
                            }
                            else
                            {
                                //���Z�b�g
                                timerCounter = 0.0f;
                                //��Ԃ�J��
                                stateNumber = 3;
                            }
                        }
                    }
                    break;
                //�U���\�����ɋ߂Â�
                case 3:
                    {
                        if (length > 2.0f)
                        {
                            myAnimator.SetInteger("Run", 1);
                            this.myRigidbody.velocity = new Vector2(VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);
                            //���Z�b�g
                            timerCounter = 0.0f;
                            //��Ԃ�J��
                            stateNumber = 5;
                        }
                        else
                        {
                            //��~
                            myAnimator.SetInteger("Run", 0);
                            this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
                            //���Z�b�g
                            timerCounter = 0.0f;
                            //��Ԃ�J��
                            stateNumber = 4;
                        }
                    }
                    break;

                    //���������
                    case 4: 
                    {
                        //�v���C���[���߂�
                        if (length < 5.0f)
                        {
                            myAnimator.SetInteger("Run", -1);
                            this.myRigidbody.velocity = new Vector2(-VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);
                            //���Z�b�g
                            timerCounter = 0.0f;
                            //��Ԃ�J��
                            stateNumber = 6;
                        }
                        else
                        {
                            //��~
                            myAnimator.SetInteger("Run", 0);
                            this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
                            //���Z�b�g
                            timerCounter = 0.0f;
                            //��Ԃ�J��
                            stateNumber = 3;
                        }
                    }
                    break;

                    //Attack
                    case 5:
                    {
                        myAnimator.SetTrigger("PreAttack");
                        
                        //���Z�b�g
                        timerCounter = 0.0f;
                        //��Ԃ�J��
                        stateNumber = 2;
                    }
                    break;
                    //Guard
                    case 6:
                    {
                        myAnimator.SetTrigger("Guard");
                        //���Z�b�g
                        timerCounter = 0.0f;
                        //��Ԃ�J��
                        stateNumber = 3;

                    }
                    break;
            }


        }
        //�^�C�}�[
        timerCounter += Time.deltaTime;
    }

    // Update is called once per frame
    //�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q
    //�A�b�v�f�[�g
    //�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q
    void Update()
    {
        if (assign == 1)
        {
            //�v���C���[
            Player();
        }
        else if (assign == -2)
        {
            //�v���C���[�iCOM)
            PlayerComuter();
        }

        //�W�����v�̑J��
        this.myAnimator.SetFloat("Jump", this.myRigidbody.velocity.y);
        this.myAnimator.SetBool("Ground", isGround);
    }

    public void PreAttackStart()
    {
        Debug.Log("PreAttackStart");
    }

    public void AttackStart()
    {
        //�Փ˂��I��
        //this.attackObject.SetActive(true);
        //this.attackObject.GetComponent<BoxCollider2D>().enabled = true;
        //�U���𐶐�
        attackObject = Instantiate(attackPrefab, this.transform.position + new Vector3(1.29f *transform.localScale.x ,1.44f,0f), Quaternion.identity);
        Debug.Log("AttackStart");
    }

    public void AttackEnd()
    {
        //�Փ˂��I�t
        //this.attackObject.SetActive(false);
        //this.attackObject.GetComponent<BoxCollider2D>().enabled = false;
        //�U����j������
        Destroy(attackObject.gameObject);
        isRun = true;
        Debug.Log("AttackEnd");      
    }

    /*
    private void FixedUpdate()
    {
        //�n�㔻��
        isGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        if(isGround || axisH != 0)
        {
            //�n�ʏ�OR���x���O�ł͂Ȃ�
            //���x���X�V����
            rbody.velocity = new Vector2(VELOCITY * axisH,rbody.velocity.y);
        }
    }
    */


    //
    
    //
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack")
        {
            myAnimator.SetTrigger("Damage");
            
            if (assign == 1)
            {
                hpManager.GetComponent<HPManager>().HPLeft--;

                //�v���C���[���_���[�W���󂯂�
                
            }
            else if (assign == -2)
            {
                //�v���C���[�iCOM)���_���[�W���󂯂�
                hpManager.GetComponent<HPManager>().HPRight--;
            }
            
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }


    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
}