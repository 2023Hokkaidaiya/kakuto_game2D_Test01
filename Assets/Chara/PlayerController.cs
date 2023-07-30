using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    private float VELOCITY = 6.5f;

    //�W�����v�ʁi�萔�j
    private float JUMPPOWER = 6.0f;

    //�n��
    private bool isGround = false;

    //���n�ł��郌�C���[
    public LayerMask groundLayer;

    //�ړ��\
    private bool isRun = true;

    //�U���I�u�W�F�N�g(�K�v�ɉ����Ēǉ��j
    private GameObject attackObject;
    //�U���I�u�W�F�N�g�Q�iDeathBrow�j
    private GameObject deathbrowObject;

    //�v���C���[���G���@���F�u�P�v�@�E�F�Q�@�ACOM�@���F-1�A�E�F�u�|2�v
    public int assign;

    //�U����Prefab
    public GameObject attackPrefab;
    public GameObject deathbrowPrefab;

    //HP�}�l�[�W���[
    private GameObject hpManager;

    //����(�C���X�y�N�^�[�ŃN���X�����Ă��������j
    public GameObject otherPlayer;

    //��ԁi�X�e�[�g�}�V���j
    private int stateNumber;

    //�ėp�^�C�}�[
    private float timerCounter;

    //debag�e�L�X�g
    public GameObject stateText;

    //�U�����󂯂�i���肩��m�炳���j
    public bool isBeAttacked = false;

    //�K�[�h���Ă���
    private bool isGuard = false;

    //SoundEffect
    public AudioClip SE1Slashed; //�U�V���b���Ƃ�����
    public AudioClip SE2SoundOfSword; //�K�[�h�������̌�����
    public AudioClip SE3Footsteps; //����

    //isEnd
    private bool isEnd = false;

    //Com�̃K�[�h���@�P�O�O�ŕK���K�[�h�@�O�ł��Ȃ�
    public int guardRate;

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
        //�Փ˔��肾�����f�B�X�G�[�u��
        //this.attackObject.GetComponent<BoxCollider2D>().enabled = false;

        //�A�T�C���P��Leftstate�@�A�T�C���Q��Rightstate(�ʏ�PVS-2�j
        if (assign == 1 || assign == -1)
            {
            //�v���C���[
            this.stateText = GameObject.Find("Leftstate");
        }
        else if (assign == 2 || assign == -2)
            {
            //�v���C���[�iCOM)
            this.stateText = GameObject.Find("Rightstate");
        }

        //�f�o�b�O(�Q�[���X�s�[�h��x������
        Time.timeScale = 1.0f;
    }
    //�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q
    //�v���C���[
    //�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q�Q
    void Player()
    {
        //�����Ă���
        if (isEnd == false)
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
            //�K�[�h
            if (Input.GetKeyDown(KeyCode.X))
            {
                //�����Ȃ�����
                isRun = false;

                myAnimator.SetTrigger("Guard");
            }
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

    // ���(��)
    private enum states : int { Thinking = 0, Forward, Back, Attack, Guard, Damage, Approach, Die };

    //Idoling = 0, Advance, Wait, Approach, Back, Attack,  Guard ,Die};

    void PlayerComuter()
    {
        //����̋���
        float length = getLength(this.transform.position, otherPlayer.transform.position);

        //���
        switch (stateNumber)
        {
            case (int)states.Thinking:
                {
                    //��~
                    myAnimator.SetInteger("Run", 0);
                    this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);

                    //�^�C�}�[
                    if (timerCounter > 1.0f)
                    {
                        //���̍s�������߂�

                        //����܂�5���[�g���ȏ�H
                        if (length > 5.0f)
                        {
                            //�N���A�[
                            timerCounter = 0.0f;

                            //�J��
                            stateNumber = (int)states.Forward;
                        }
                        else
                        {
                            //����܂�2���[�g���ȉ��H
                            if (length < 2.0f)
                            {
                                //�U����������A�܂��͑���̍U����������

                                //���肪�U���J�n�H
                                if (isBeAttacked && Random.Range(0,100) <guardRate)
                                {
                                    //�K�[�h�̊J�n
                                    myAnimator.SetTrigger("Guard");

                                    //�N���A�[
                                    timerCounter = 0.0f;

                                    //�J��
                                    stateNumber = (int)states.Guard;
                                }
                                else
                                {
                                    //�Ⴆ�Α���̃��C�t�Ǝ����̃��C�t���r

                                    //���񂪂񂢂����� = 7
                                    //�ӂ� = 5
                                    //���̂��������� = 3

                                    int random = 3;

                                    //�����_���i30�`70%�j
                                    if (Random.Range(1, 11) <= random)
                                    {
                                        //�U���̊J�n
                                        myAnimator.SetTrigger("PreAttack");

                                        //�N���A�[
                                        timerCounter = 0.0f;

                                        //�J��
                                        stateNumber = (int)states.Attack;
                                    }
                                    else
                                    {
                                        //�N���A�[
                                        timerCounter = 0.0f;

                                        //�J��
                                        stateNumber = (int)states.Back;
                                    }
                                }
                            }
                            else
                            {
                                //�Ⴆ�Α���̃��C�t�Ǝ����̃��C�t���r

                                //���񂪂񂢂����� = 7
                                //�ӂ� = 5
                                //���̂��������� = 3

                                int random = 3;

                                //�����_���i30�`70%�j
                                if (Random.Range(1, 11) <= random)
                                {
                                    //�N���A�[
                                    timerCounter = 0.0f;

                                    //�J��
                                    stateNumber = (int)states.Approach;
                                }
                                else
                                {
                                    //�N���A�[
                                    timerCounter = 0.0f;

                                    //�J��
                                    stateNumber = (int)states.Back;
                                }
                            }
                        }
                    }
                    else 
                    {
                        //n�b�{�[�g���Ă�
                        if (length < 2.0f)
                        {
                            //���肪�U���J�n�H
                            if (isBeAttacked && Random.Range(0, 100) < guardRate)
                            {
                                //�K�[�h�̊J�n
                                myAnimator.SetTrigger("Guard");

                                //�N���A�[
                                timerCounter = 0.0f;

                                //�J��
                                stateNumber = (int)states.Guard;
                            }
                            else
                            {
                                Debug.Log("�K�[�h�L�����Z��");
                            }
                        }
                    }

                }
                break;

            case (int)states.Forward:
                {
                    myAnimator.SetInteger("Run", 1);
                    this.myRigidbody.velocity = new Vector2(VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);

                    //����܂�5���[�g���ȉ��H
                    if (length < 5.0f)
                    {
                        //�N���A�[
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(0.0f, 0.5f);

                        //�J��
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Back:
                {
                    myAnimator.SetInteger("Run", -1);
                    this.myRigidbody.velocity = new Vector2(-VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);

                    //����܂�7���[�g���ȏ�H
                    if (length > 7.0f)
                    {
                        //�N���A�[
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(0.0f, 0.5f);

                        //�J��
                        stateNumber = (int)states.Thinking;
                    }
                    //��Com
                    if(assign == -1)
                    {
                        //���[
                        if(this.transform.position.x < -7.8)
                        {
                            //�N���A
                            //timerCounter = 0.0f;
                            timerCounter = Random.Range(0.0f, 0.5f);

                            //�J��
                            stateNumber = (int)states.Thinking;
                        }

                    }
                    //�ECom
                    if(assign == -2)
                    {
                        //�E�[
                        if (this.transform.position.x > 7.8)
                        {
                            //�N���A
                            //timerCounter = 0.0f;
                            timerCounter = Random.Range(0.0f, 0.5f);

                            //�J��
                            stateNumber = (int)states.Thinking;
                        }
                    }

                }
                break;

            case (int)states.Attack:
                {
                    //�A�j���[�V�����I���܂ł̑ҋ@
                    if (timerCounter > 1.0f)
                    {
                        //�N���A�[
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(-0.5f, 0.0f);

                        //�J��
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Guard:
                {
                    //�A�j���[�V�����I���܂ł̑ҋ@
                    if (timerCounter > 1.0f)
                    {
                        //�N���A�[
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(0.0f, 0.5f);

                        //�J��
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Damage:
                {
                    //�A�j���[�V�����I���܂ł̑ҋ@
                    if (timerCounter > 0.1f)
                    {
                        //�N���A�[
                        timerCounter = 0.4f;
                        //timerCounter = Random.Range(-1.0f, 0.0f);

                        //�J��
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Approach:
                {
                    myAnimator.SetInteger("Run", 1);
                    this.myRigidbody.velocity = new Vector2(VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);

                    //����܂�2���[�g���ȉ��H
                    if (length < 2.0f)
                    {
                        //�N���A�[
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(0.0f, 0.5f);

                        //�J��
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Die:
                {
                    //�������Ȃ�
                }
                break;

        }

        //�f�o�b�O
        switch (stateNumber)
        {
            case 0: stateText.GetComponent<Text>().text = "Thinking " + length.ToString("F3"); break;
            case 1: stateText.GetComponent<Text>().text = "Forward " + length.ToString("F3"); break;
            case 2: stateText.GetComponent<Text>().text = "Back " + length.ToString("F3"); break;
            case 3: stateText.GetComponent<Text>().text = "Attack " + length.ToString("F3"); break;
            case 4: stateText.GetComponent<Text>().text = "Guard " + length.ToString("F3") + "" + isGuard; break;
            case 5: stateText.GetComponent<Text>().text = "Damage " + length.ToString("F3"); break;
            case 6: stateText.GetComponent<Text>().text = "Approach " + length.ToString("F3"); break;
            case 7: stateText.GetComponent<Text>().text = "Die " + length.ToString("F3"); break;
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
        if (assign == 1 || assign == 2)
        {
            //�v���C���[
            Player();
        }
        else if (assign == -2 || assign == -1)
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
        //�U���J�n��m�点��i�ǉ��j
        otherPlayer.GetComponent<PlayerController>().isBeAttacked = true;

        //Debug.Log("PreAttackStart");
    }

    public void AttackStart()
    {
        //
        if (gameObject.name == "Player1DeathBrowPrefab(Clone)")
        {
            //�U���𐶐�(3�{���ɑ傫�����Ă���j
            deathbrowObject = Instantiate(deathbrowPrefab, this.transform.position + new Vector3(5f * transform.localScale.x, 1.44f, 0f), Quaternion.identity);

            //Debug.Log("AttackStart");
        }
        else
        {
            //�U���𐶐�
            attackObject = Instantiate(attackPrefab, this.transform.position + new Vector3(1.29f * transform.localScale.x, 1.44f, 0f), Quaternion.identity);

            //Debug.Log("AttackStart");
        }
     
    }

    public void AttackEnd()
    {
        //�U���I����m�点��i�ǉ��j
        otherPlayer.GetComponent<PlayerController>().isBeAttacked = false;

        if(gameObject.name == "Player1DeathBrowPrefab(Clone)")
        {
            Destroy(deathbrowObject.gameObject);
        }
        else
        {
            //�U����j������
            Destroy(attackObject.gameObject);
        }
        //������悤�ɂȂ�
        isRun = true;

        //Debug.Log("AttackEnd");
    }

    public void GuardStart()
    {
        //�K�[�h�J�n
        isGuard = true;

        //Debug.Log("GuardStart");
    }

    public void GuardEnd()
    {
        //�K�[�h�I��
        isGuard = false;

        //������悤�ɂȂ�
        isRun = true;

        //Debug.Log("GuardEnd");
    }

    public void DamageEnd()
    {
        //������悤�ɂȂ�
        isRun = true;

        //Debug.Log("DamageEnd");
    }

    public void Footsteps()
    {
        GetComponent<AudioSource>().PlayOneShot(SE3Footsteps);
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
            

            if (assign == 1)
            {
                //������Ȃ��Ȃ�
                isRun = false;

                if (isGuard == false)
                {
                    //�_���[�W�̃A�j���[�V����
                    myAnimator.SetTrigger("Damage");

                    //�_���[�W���ʉ�
                    GetComponent<AudioSource>().PlayOneShot(SE1Slashed); 

                    //�_���[�W�𔽉f
                    hpManager.GetComponent<HPManager>().HPLeft -= TitleController.assign2Attack;

                    //LifeCheck����
                    if (hpManager.GetComponent<HPManager>().HPLeft <= 0)
                    {
                        //���S�A�j���[�����
                        myAnimator.SetBool("Die", true);
                        //�I��
                        isEnd = true;
                    }
                }
                else
                {
                    //Debug.Log("�K�[�h����");
                    //�K�[�h���ʉ�
                    //�K�[�h���������̂Ń_���[�W���󂯂Ȃ�
                    GetComponent<AudioSource>().PlayOneShot(SE2SoundOfSword);
                }

                //�v���C���[���_���[�W���󂯂�
                //�������
                //myRigidbody.AddForce(new Vector3(-5000f * transform.localScale.x, 0f, 0f));
                myRigidbody.AddForce(new Vector3(-5f * transform.localScale.x, 0f, 0f), ForceMode2D.Impulse);
            }
            else if (assign == -2)
            {
                //�N���A�[
                timerCounter = 0.0f;
                
                              
                if (isGuard == false)
                {
                    //�_���[�W�̃A�j���[�V����
                    myAnimator.SetTrigger("Damage");
                    //�_���[�W���ʉ�
                    GetComponent<AudioSource>().PlayOneShot(SE1Slashed);
                    //�J��
                    stateNumber = (int)states.Damage;
                    //�v���C���[�iCOM)���_���[�W���󂯂�
                    hpManager.GetComponent<HPManager>().HPRight -= TitleController.assign1Attack;
                    
                    //LifeCheck����
                    if (hpManager.GetComponent<HPManager>().HPRight <= 0)
                    {
                        //���S�A�j���[�V����
                        myAnimator.SetBool("Die", true);
                        //�I��
                        stateNumber = (int)states.Die;
                    }

                }
                else
                {
                    //�K�[�h���ʉ�
                    GetComponent<AudioSource>().PlayOneShot(SE2SoundOfSword);
                }

                //�������
                //myRigidbody.AddForce(new Vector3(-5000f * transform.localScale.x, 0f, 0f));
                myRigidbody.AddForce(new Vector3(-5f * transform.localScale.x, 0f, 0f), ForceMode2D.Impulse);
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