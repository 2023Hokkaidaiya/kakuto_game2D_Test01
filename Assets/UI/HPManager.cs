using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    //�����HP�e�L�X�g
    public GameObject hpleftText;
    //�E��̃e�L�X�g
    public GameObject hprightText;
    //HP player1 player2�v�Z�p�ϐ�
    public int HPLeft;  //1
    public int HPRight; //2

    // Start is called before the first frame update
    void Start()
    {
        //HP player1 player2��0�ŏ�����
        HPLeft = 0;
        HPRight = 0;
        //�V�[������HPText�I�u�W�F�N�g���擾
        this.hpleftText = GameObject.Find("HP_player1");
        this.hprightText = GameObject.Find("HP_player2");
    }

    // Update is called once per frame
    void Update()
    {
        //HP���X�V����
        this.hpleftText.GetComponent<Text>().text = "HP:" + HPLeft.ToString();
        this.hprightText.GetComponent<Text>().text = "HP:" + HPRight.ToString();
    }
}
