using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseContestController : MonoBehaviour
{
    //Rigidbody2D�ϐ�
    //Rigidbody2D rbody;
    //�ړ�������R���|�[�l���g������
    private Rigidbody2D myRigidbody;
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;
    //���n�ł��郌�C���[
    //public LayerMask groundLayer;
    //�ړ��\
    //private bool isRun = true;
    //HP�}�l�[�W���[
    //private GameObject hpManager;
    private HPManager hpManager;
    //HP�ɗ^����_���[�W��
    private int CloseContestDamage = 1;
    //��ԁi�X�e�[�g�}�V���j
    private int stateNumber = 0;
    //�ėp�^�C�}�[
    private float timerCounter;
    //debag�e�L�X�g�i�����e�L�X�g�j
    //public GameObject stateText;

    //addforce�p�̕ϐ�
    public float moveForce = 10f;�@//�������0�ɂ�����10�ɂ����肷��
    //private bool isMovingLeft = false;
    //private bool isMovingRight = false;

    //SoundEffect
    public AudioClip SE1Slashed; //�U�V���b���Ƃ�����
    public AudioClip SE2SoundOfSword; //�K�[�h�������̌�����
    public AudioClip SE3Footsteps; //����

    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //Rigidbody�R���|�[�l���g���擾
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //HP�}�l�[�W���I�u�W�F�N�g
        hpManager = GameObject.Find("HPManager").GetComponent<HPManager>();

        //�L���̏����i�����l�𒲐����邱�Ƃ��ł���A�Ƃ肠�����͂O�j
        hpManager.EvP = 0;

        //�f�o�b�O(�Q�[���X�s�[�h��x������
        Time.timeScale = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        ////�U���Ɏg�p����{�^���i�D�挠������΍U���ł���悤�ɂ������j=======
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Attack1();
        //}

        ////Addforce�p=========================================================
        //// ���{�^���������ꂽ��
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    isMovingLeft = true;
        //}

        //// �E�{�^���������ꂽ��
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    isMovingRight = true;
        //}

        //// �{�^���������ꂽ��
        //if (Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    isMovingLeft = false;
        //}

        //if (Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    isMovingRight = false;
        //}
        //==================================================
        // ���{�^����������Ă���΍������ɗ͂�������
        //if (isMovingLeft)
        //{
        //    //myRigidbody.AddForce(Vector3.left * moveForce, ForceMode2D.Force);
        //    myRigidbody.AddForce(Vector3.left * 30f, ForceMode2D.Impulse);
        //    isMovingLeft = false;
        //}

        //// �E�{�^����������Ă���ΉE�����ɗ͂�������
        //if (isMovingRight)
        //{
        //    //myRigidbody.AddForce(Vector3.right * moveForce, ForceMode2D.Force);
        //    myRigidbody.AddForce(Vector3.right * 30f, ForceMode2D.Impulse);
        //    isMovingRight = false;
        //}
        //===================================================


        timerCounter += Time.deltaTime;

        //���
        switch (stateNumber)
        {
            //5050
            case 0:
                {
                    // ���{�^���������ꂽ��i30��傫������Ƒ����A����������ƒx���Ȃ�j
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        myRigidbody.AddForce(Vector3.left * 30f, ForceMode2D.Impulse);
                    }
                    // �E�{�^���������ꂽ��i30��傫������Ƒ����A����������ƒx���Ȃ�j
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        myRigidbody.AddForce(Vector3.right * 30f, ForceMode2D.Impulse);
                    }

                    //0�Ȃ�ǂ��炪�d�|���Ă��ǂ�
                    if (hpManager.EvP == 0)
                    {
                        //Player1�̏���(Z�����ōU������j
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            //�N���A�[
                            timerCounter = 0.0f;

                            //�t���O�N���A(��{�ƃK�[�h���o�b�e�B���O���邱�Ƃ͂Ȃ��̂ŁA�d�|���钼�O�ɂ͕K���N���A���邱��)
                            myAnimator.SetBool("Ippon", false);
                            myAnimator.SetBool("Guard", false);

                            //Player1���d�|����
                            myAnimator.SetTrigger("Attack1AS1");

                            //��Ԃ̑J��
                            stateNumber = 1;
                        }

                        //Player2�̏����i1�b�o�ߌ�A�����_����Player2�������n�߂�A10���Ƃ��Ă��邪�傫������Ƃ��U���I�ɂȂ�܂��j
                        if (timerCounter > 1.0f) 
                        {
                            //�N���A�[
                            timerCounter = 0.0f;

                            //�����_�� ����10%�i10�͕ϐ����j
                            if (Random.Range(0, 100) < 10)
                            {
                                //�t���O�N���A
                                myAnimator.SetBool("Ippon", false);
                                myAnimator.SetBool("Guard", false);

                                //Player2���d�|����
                                myAnimator.SetTrigger("Attack1AS2");

                                //��Ԃ̑J��
                                stateNumber = 2;
                            }
                        }
                    }
                    //Player1���L���Ȃ�Player1�݂̂��d�|���邱�Ƃ��ł���
                    else if (hpManager.EvP < 0)
                    {
                        //Player1�̏���
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            //�N���A�[
                            timerCounter = 0.0f;

                            //�t���O�N���A
                            myAnimator.SetBool("Ippon", false);
                            myAnimator.SetBool("Guard", false);

                            //Player1���d�|����
                            myAnimator.SetTrigger("Attack1AS1");

                            //��Ԃ̑J��
                            stateNumber = 1;
                        }
                    }
                    //Player2���L���Ȃ�Player2�݂̂��d�|���邱�Ƃ��ł���
                    else if (hpManager.EvP > 0)
                    {
                        //Player2�̏���
                        if (timerCounter > 1.0f)
                        {
                            //�N���A�[
                            timerCounter = 0.0f;

                            //�����_�� ����10%�i10�͕ϐ����j
                            if (Random.Range(0, 100) < 10)
                            {
                                //�t���O�N���A
                                myAnimator.SetBool("Ippon", false);
                                myAnimator.SetBool("Guard", false);

                                //Player2���d�|����
                                myAnimator.SetTrigger("Attack1AS2");

                                //��Ԃ̑J��
                                stateNumber = 2;
                            }
                        }
                    }
                }
                break;

            //Player1���d�|����
            case 1:
                {
                    //Player2�̏���
                    if (timerCounter > 0.8f)�@//Player1���U�����d�|���Ă���0.8�b�ȏ�i�ϐ����j���o�߂����Ƃ��ɐ��藧���܂��B
                    {
                        //�K�[�h���s
                        myAnimator.SetBool("Ippon", true);
                        myAnimator.SetInteger("Assign", 1);

                        //�N���A�[
                        timerCounter = 0.0f;

                        //��Ԃ̑J��
                        stateNumber = -1;
                    }
                    else
                    {
                        //�����_�� ����5%
                        if (Random.Range(0, 100) < 5)�@//Player1���U�����d�|���Ă���0.8�b�i�ϐ����j���o�߂���܂ŃK�[�h�̔���
                        {
                            myAnimator.SetBool("Guard", true);
                            myAnimator.SetInteger("Assign", 2);

                            //Player2���L���ɂȂ�
                            hpManager.EvP++;

                            //�N���A�[
                            timerCounter = 0.0f;

                            //��Ԃ̑J��
                            stateNumber = 3;
                        }
                    }
                }
                break;

            //Player2���d�|����
            case 2:
                {
                    //Player1�̏���
                    if (timerCounter > 0.8f)
                    {
                        //�K�[�h���s
                        myAnimator.SetBool("Ippon", true);
                        myAnimator.SetInteger("Assign", 2);

                        //�N���A�[
                        timerCounter = 0.0f;

                        //��Ԃ̑J��
                        stateNumber = -2;
                    }
                    else
                    {
                        //�K�[�h�L�[
                        if (Input.GetKeyDown(KeyCode.X))
                        {
                            myAnimator.SetBool("Guard", true);
                            myAnimator.SetInteger("Assign", 1);

                            //Player1���L���ɂȂ�
                            hpManager.EvP--;

                            //�N���A�[
                            timerCounter = 0.0f;

                            //��Ԃ̑J��
                            stateNumber = 3;
                        }
                    }
                }
                break;

            //��莞�Ԍo�ߌ�A��ԂO�ɖ߂�
            case 3:
                {
                    if (timerCounter > 1.0f)
                    {
                        //��Ԃ̑J��
                        stateNumber = 0;
                    }
                }
                break;

            //��{ Player1��������
            case -1:
                {
                    if (timerCounter > 1.0f)
                    {
                        //�_���[�W�𔽉f
                        hpManager.HPRight -= CloseContestDamage * TitleController.assign1Attack;

                        //�N���A�[
                        timerCounter = 0.0f;

                        //��Ԃ̑J��
                        stateNumber = 9;
                    }
                }
                break;

            //��{ Player2��������
            case -2:
                {
                    if (timerCounter > 1.0f)
                    {
                        //�_���[�W�𔽉f
                        hpManager.HPLeft -= CloseContestDamage * TitleController.assign2Attack;

                        //�N���A�[
                        timerCounter = 0.0f;

                        //��Ԃ̑J��
                        stateNumber = 9;
                    }
                }
                break;

            //�߂�
            case 9:
                {
                    if (timerCounter > 0.5f)
                    {
                        GameObject.Find("PlayerGenerator").GetComponent<PlayerGenerator>().CheckoutCloseContest();
                    }
                }
                break;
        }
    }

    //�ȉ��̓A�j���[�V�����C�x���g�p�̊֐�

    //�A�j���[�V�����C�x���g�̓A�^�b�`�����X�N���v�g�����ĂׂȂ�
    //�C�x���g���󂯎��A�ԐړI��PlayerGenerator�̊֐����Ă�
    public void CheckOut()
    {
        GameObject.Find("PlayerGenerator").GetComponent<PlayerGenerator>().CheckoutCloseContest();
    }
    //public void LeftHPDown()
    //{
    //    //�_���[�W�𔽉f
    //    hpManager.GetComponent<HPManager>().HPLeft -= CloseContestDamage * TitleController.assign2Attack;
    //    //0.1�b���CloseContestDamage�����Z�b�g
    //}
    //public void RightHPDown()
    //{
    //    //�_���[�W�𔽉f
    //    hpManager.GetComponent<HPManager>().HPRight -= CloseContestDamage * TitleController.assign2Attack;
    //    //0.1�b���CloseContestDamage�����Z�b�g
    //}
    public void Slashed()
    {
        //�_���[�W���ʉ�
        GetComponent<AudioSource>().PlayOneShot(SE1Slashed);
    }
    public void SoundOfSword()
    {
        //�K�[�h���ʉ�
        GetComponent<AudioSource>().PlayOneShot(SE2SoundOfSword);
    }
    public void Footsteps()
    {
        GetComponent<AudioSource>().PlayOneShot(SE3Footsteps);
    }
    //public void Attack1()
    //{
    //  //  myAnimator.SetTrigger("Attack1");
    //}
    ////1����������
    //public void WinnerIsAssign1()
    //{
    //   // myAnimator.SetInteger("Assign", 1);
    //}
    ////1�ɑ΂���K�[�h

    ////2����������
    //public void WinnerIsAssign2()
    //{
    //   // myAnimator.SetInteger("Assign", 2);
    //}
    ////0�ɖ߂�
    //public void NoSide()
    //{
    //   // myAnimator.SetInteger("Assign", 0);
    //}
    //�����i��̍��E�ɉ�������Ă邳��Ă���������郁�\�b�h������j
}
