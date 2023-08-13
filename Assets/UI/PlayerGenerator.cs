using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;


public class PlayerGenerator : MonoBehaviour
{

    //PlayerPrefab������
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject Player1DeathBrowPrefab;
    public GameObject Player2DeathBrowPrefab;
    
    
    private GameObject Player1;
    private GameObject Player2;
    

    //�ꖇ�GPrefab������
    public GameObject CloseContestPrefab;
    private GameObject CloseContest;

    public GameObject CloseContest2Prefab;
    private GameObject CloseContest2;

    //HPmanager
    private GameObject hpManager;


    // Start is called before the first frame update
    void Start()
    {
        //HP�}�l�[�W���I�u�W�F�N�g
        hpManager = GameObject.Find("HPManager");
        //Player1��2������̈ʒu�ɐ�������
        Player1 = Instantiate(Player1Prefab);
        Player2 = Instantiate(Player2Prefab);

        Player1.transform.position = new Vector2(-6f, -3.21048f);
        Player2.transform.position = new Vector2(6f, -3.21048f);
        //OtherPlayer�փN���X�����ēo�^����B�i�ǉ��j
        Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //assign�̍Đݒ���\ ����:1 �E:2 / COM ���F-1 �E�F-2
        Player1.GetComponent<PlayerController>().assign = 1;
        Player2.GetComponent<PlayerController>().assign = -2;
        //�G�̃K�[�h���iTitleController�Ɉړ������̂Ŕp�~�j
        //Player1.GetComponent<PlayerController>().guardRate = 5;
        //Player2.GetComponent<PlayerController>().guardRate = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //�f�o�b�O�p�i�ŏI�I�ɂ̓R�����g�A�E�g)=========================================
        //1�ŕK�E�Z�ɕς���
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlayer1toPlayerDeathBrow();
        }
        //2�œG��K�E�Z�ɕς���
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePlayer2toPlayerDeathBrow();
        }
        //0�ňꖇ�G�ɕς���i�f�o�b�O�p�ł���̂��ɍX�V���邱�Ɓj
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CheckinCloseContest();
        }
        //9�ňꖇ�G����߂��i�f�o�b�O�p�ł���̂��ɍX�V���邱�Ɓj
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CheckoutCloseContest();
        }
        //8�ňꖇ�G�P���ꖇ�G�Q�ɕς���
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeCloseContest();
        }
        //============================================================================


        //����ST��1�ɂȂ�����K�E�Z�ɕς���
        //����Close��1�ɂȂ�����΂��荇��
        if (hpManager.GetComponent<HPManager>().Close == 1)
        {
            hpManager.GetComponent<HPManager>().Close = 0;

            CheckinCloseContest();
        }
     
    }





    //�A�T�C��1�̓���ւ�(���l�̂P�����������Player1��Player1DeathBrow�jPlayer1�ɑ���������̂�Player1DeathBrow�ɕύX�@�K�v�ɂȂ����瑫��
    public void ChangePlayer1toPlayerDeathBrow()
    {
        //����ւ����O�̃|�W�V�������擾(Left�����ς���)
        Vector3 positionLeft = Player1.transform.position;
        //�|�W�V�������擾�ł����̔j��
        Destroy(Player1.gameObject, 0.0f);
        //�V�����v���C���[�𐶐�
        Player1 = Instantiate(Player1DeathBrowPrefab);
        //position�Ŏ擾�����ʒu��ݒ�
        Player1.transform.position = positionLeft;
        //�v���C���[�͎g�p���Ă��Ȃ����ACOM�p�ɑ����ݒ�
        Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        //�N���X����`�œo�^����
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //�L�[�{�[�h�œ�����
        Player1.GetComponent<PlayerController>().assign = 1;
    }
    public void ChangePlayer2toPlayerDeathBrow()
    {
        //����ւ����O�̃|�W�V�������擾(Left�����ς���)
        Vector3 positionLeft = Player2.transform.position;
        //�|�W�V�������擾�ł����̔j��
        Destroy(Player2.gameObject, 0.0f);
        //�V�����v���C���[�𐶐�
        Player2 = Instantiate(Player2DeathBrowPrefab);
        //position�Ŏ擾�����ʒu��ݒ�
        Player2.transform.position = positionLeft;
        //�v���C���[�͎g�p���Ă��Ȃ����ACOM�p�ɑ����ݒ�
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //�N���X����`�œo�^����
        Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        //�X�e�[�g�}�V���œ�����
        Player2.GetComponent<PlayerController>().assign = -2;
    }

    //�A�T�C��1�ƃA�T�C���Q���ꖇ�G�ɓ���ւ���(���l��0�����������Player1,2���N���[�X�R���e�X�g�j
    public void CheckinCloseContest()
    {
        //����ւ����O�̃|�W�V�������擾(L��R�����擾)
        Vector3 positionLeft = Player1.transform.position;
        Vector3 positionRight = Player2.transform.position;
        Vector3 positionMiddle = (positionLeft + positionRight) / 2f;
        
        //�|�W�V�������擾�ł����̔j��
        Destroy(Player1.gameObject, 0.0f);
        Destroy(Player2.gameObject, 0.0f);
        //�΂��荇���𐶐�
        CloseContest = Instantiate(CloseContestPrefab, positionMiddle, Quaternion.identity);

    }

    public void CheckoutCloseContest()
    {
        //����ւ����O�̃|�W�V�������擾
        Vector3 positionMiddle = CloseContest.transform.position;
        //�|�W�V�������擾�ł����̔j��
        Destroy(CloseContest.gameObject, 0.0f);

        //Player1��2������̈ʒu�ɐ�������
        Player1 = Instantiate(Player1Prefab);
        Player2 = Instantiate(Player2Prefab);

        Player1.transform.position = new Vector2(positionMiddle.x -1.0f, -3.21048f);
        Player2.transform.position = new Vector2(positionMiddle.x +1.0f, -3.21048f);
        //OtherPlayer�փN���X�����ēo�^����B�i�ǉ��j
        Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //assign�̍Đݒ���\ ����:1 �E:2 / COM ���F-1 �E�F-2
        Player1.GetComponent<PlayerController>().assign = 1;
        Player2.GetComponent<PlayerController>().assign = -2;
        ////�G�̃K�[�h��
        //Player1.GetComponent<PlayerController>().guardRate = 5;
        //Player2.GetComponent<PlayerController>().guardRate = 5;
    }

    public void ChangeCloseContest() //�G����ꂪ��ɕύX
    {
        //����ւ����O�̃|�W�V�������擾
        Vector3 positionMiddle = CloseContest.transform.position;
        //�|�W�V�������擾�ł����̔j��
        Destroy(CloseContest.gameObject, 0.0f);
        //�΂��荇��2�𐶐�
        CloseContest = Instantiate(CloseContest2Prefab, positionMiddle, Quaternion.identity);
    }

        //�A�T�C��1��߂�
        //�A�T�C��2�̓���ւ�
        //�A�T�C��2��߂�
        //HP0�ɂȂ�����Impossible�ɓ���ւ���i�p�~�j

    }
