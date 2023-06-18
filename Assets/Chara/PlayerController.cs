using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //Rigidbody�R���|�[�l���g���擾
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //�q�I�u�W�F�N�g���擾
        this.attackObject = transform.Find("Attack").gameObject;

        //�Փ˂��I�t
        this.attackObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
                this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);

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
            this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
        }

        //�U��
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //�����Ȃ�����
            isRun = false;

            myAnimator.SetTrigger("PreAttack");
        }

        //Debug.Log("velocity: " + this.myRigidbody.velocity.y);
        //Debug.Log("Ground: " + isGround); 

        this.myAnimator.SetFloat("Jump", this.myRigidbody.velocity.y);
        this.myAnimator.SetBool("Ground", isGround);

        //�R���C�_�[��OFF/ON�̃T���v��
        //this.GetComponent<BoxCollider2D>().enabled = false;
        //this.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void PreAttackStart()
    {
        Debug.Log("PreAttackStart");
    }

    public void AttackStart()
    {
        //�Փ˂��I��
        this.attackObject.SetActive(true);
        Debug.Log("AttackStart");
    }

    public void AttackEnd()
    {
        //�Փ˂��I�t
        this.attackObject.SetActive(false);
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        if (other.gameObject.tag == "Attack")
        {
            myAnimator.SetTrigger("Damage");
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