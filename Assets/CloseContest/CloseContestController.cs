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
    private bool isRun = true;
    //HP�}�l�[�W���[
    private GameObject hpManager;
    //HP�ɗ^����_���[�W��
    private int CloseContestDamage = 1;
    //��ԁi�X�e�[�g�}�V���j
    private int stateNumber;
    //�ėp�^�C�}�[
    private float timerCounter;
    //debag�e�L�X�g�i�����e�L�X�g�j
    //public GameObject stateText;

    //addforce�p�̕ϐ�
    public float moveForce = 10f;�@//�������0�ɂ�����10�ɂ����肷��
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

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
        hpManager = GameObject.Find("HPManager");

        //�f�o�b�O(�Q�[���X�s�[�h��x������
        Time.timeScale = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
    //===================================================================
    //Addforce�p=========================================================
        // ���{�^���������ꂽ��
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isMovingLeft = true;
        }

        // �E�{�^���������ꂽ��
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isMovingRight = true;
        }

        // �{�^���������ꂽ��
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isMovingLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isMovingRight = false;
        }
        //==================================================
        // ���{�^����������Ă���΍������ɗ͂�������
        if (isMovingLeft)
        {
            myRigidbody.AddForce(Vector3.left * moveForce, ForceMode2D.Force);
        }

        // �E�{�^����������Ă���ΉE�����ɗ͂�������
        if (isMovingRight)
        {
            myRigidbody.AddForce(Vector3.right * moveForce, ForceMode2D.Force);
        }
        //===================================================
    }

    //�ȉ��̓A�j���[�V�����C�x���g�p�̊֐�
    public void LeftHPDown()
    {
        //�_���[�W�𔽉f
        hpManager.GetComponent<HPManager>().HPLeft -= CloseContestDamage * TitleController.assign2Attack;
        //0.1�b���CloseContestDamage�����Z�b�g
    }
    public void RightHPDown()
    {
        //�_���[�W�𔽉f
        hpManager.GetComponent<HPManager>().HPRight -= CloseContestDamage * TitleController.assign2Attack;
        //0.1�b���CloseContestDamage�����Z�b�g
    }
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
    //�����i��̍��E�ɉ�������Ă邳��Ă���������郁�\�b�h������j
}
