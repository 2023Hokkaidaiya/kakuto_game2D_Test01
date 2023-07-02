using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGenerator : MonoBehaviour
{

    //PlayerPrefab������
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject Player1DeathBrowPrefab;




    // Start is called before the first frame update
    void Start()
    {
        //Player1��2������̈ʒu�ɐ�������
        GameObject Player1 = Instantiate(Player1Prefab);
        GameObject Player2 = Instantiate(Player2Prefab);
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
        
    }
    //�A�T�C��1�̓���ւ�(���l�̂P�����������Player1��Player1DeathBrow�j
    public void ChangePlayer1toPlayerDeathBrow()
    {
        //����ւ����O�̃|�W�V�������擾(�N���[�����擾���������ł��Ă��Ȃ��j
        Vector3 position = Player1Prefab.transform.position;
        Destroy(this.gameObject, 0.0f);
        //position�Ŏ擾�����ʒu�ɐ����������������ł�
        GameObject Player1 = Instantiate(Player1DeathBrowPrefab);
    }

    //�A�T�C��1��߂�
    //�A�T�C��2�̓���ւ�
    //�A�T�C��2��߂�
}
