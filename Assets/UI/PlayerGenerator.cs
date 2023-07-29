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
    public GameObject Player1ImpossiblePrefab;
    public GameObject Player2ImpossiblePrefab;
    
    private GameObject Player1;
    private GameObject Player2;
    

    //�ꖇ�GPrefab������
    public GameObject CloseContestPrefab;
    private GameObject CloseContest;
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
        //�G�̃K�[�h��
        Player2.GetComponent <PlayerController>().guardRate = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //1�ŕK�E�Z�ɕς���
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlayer1toPlayerDeathBrow();
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
        //����HP��0�ȉ��Ȃ�Impossible�Ɠ���ւ��郁�\�b�h���g��
        if (hpManager.GetComponent<HPManager>().HPLeft < 1)
        {
            ChangePlayer1toImpossible();
        }
    }





    //�A�T�C��1�̓���ւ�(���l�̂P�����������Player1��Player1DeathBrow�j
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

    //�A�T�C��1�ƃA�T�C���Q���ꖇ�G�ɓ���ւ���(���l�̂Q�����������Player1,2���N���[�X�R���e�X�g�j
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
        Instantiate(CloseContestPrefab, positionMiddle, Quaternion.identity);
    }
    public void CheckoutCloseContest()
    {
        //����ւ����O�̃|�W�V�������擾
        Vector3 positionMiddle = CloseContestPrefab(Clone).transform.position;
    }
    //�A�T�C��1��߂�
    //�A�T�C��2�̓���ւ�
    //�A�T�C��2��߂�

    //HP0�ɂȂ�����Impossible�ɓ���ւ���
    public void ChangePlayer1toImpossible()
    {
        //����ւ����O�̃|�W�V�������擾
        Vector3 positionLeft = Player1.transform.position;
        //�|�W�V�������擾�ł����̔j��
        Destroy(Player1.gameObject, 0.0f);
        //�V����imposible�𐶐�
        Player1 = Instantiate(Player1ImpossiblePrefab);
        //position�Ŏ擾�����ʒu��ݒ�
        Player1.transform.position = positionLeft;
        //�v���C���[�͎g�p���Ă��Ȃ����ACOM�p�ɑ����ݒ�
        //Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        //�N���X����`�œo�^����
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //�L�[�{�[�h�œ�����
        //Player1.GetComponent<PlayerController>().assign = 1;
    }
}
