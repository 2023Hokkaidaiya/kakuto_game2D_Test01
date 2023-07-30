using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseContestController : MonoBehaviour
{
    //Rigidbody2D�ϐ�
    Rigidbody2D rbody;
    //�ړ�������R���|�[�l���g������
    private Rigidbody2D myRigidbody;
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;
    //���n�ł��郌�C���[
    public LayerMask groundLayer;
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
    //�����i��̍��E�ɉ�������Ă邳��Ă���������郁�\�b�h������j
}
