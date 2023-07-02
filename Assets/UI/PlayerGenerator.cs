using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class PlayerGenerator : MonoBehaviour
{

    //PlayerPrefab������
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject Player1DeathBrowPrefab;
    private GameObject Player1;
    private GameObject Player2;



    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlayer1toPlayerDeathBrow();
        }
    }
    //�A�T�C��1�̓���ւ�(���l�̂P�����������Player1��Player1DeathBrow�j
    public void ChangePlayer1toPlayerDeathBrow()
    {
        //����ւ����O�̃|�W�V�������擾
        Vector3 position = Player1.transform.position;
        //�|�W�V�������擾�ł����̔j��
        Destroy(Player1.gameObject, 0.0f);
        //�V�����v���C���[�𐶐�
        Player1 = Instantiate(Player1DeathBrowPrefab);
        //position�Ŏ擾�����ʒu��ݒ�
        Player1.transform.position = position;
        //�v���C���[�͎g�p���Ă��Ȃ����ACOM�p�ɑ����ݒ�
        Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        //�N���X����`�œo�^����
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //�L�[�{�[�h�œ�����
        Player1.GetComponent<PlayerController>().assign = 1;
    }

    //�A�T�C��1��߂�
    //�A�T�C��2�̓���ւ�
    //�A�T�C��2��߂�
}
