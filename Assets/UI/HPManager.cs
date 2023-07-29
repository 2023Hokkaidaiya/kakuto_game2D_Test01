using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPManager : MonoBehaviour
{
    //�����HP�e�L�X�g
    private GameObject hpleftText;
    //�E��̃e�L�X�g
    private GameObject hprightText;
    //HP player1 player2�v�Z�p�ϐ�
    public int HPLeft;  //1
    public int HPRight; //2

    // Start is called before the first frame update
    void Start()
    {
        //�t���[�������ɂ���i�R���s���[�^�[�̑��x�Ɉˑ����Ȃ��j
        Application.targetFrameRate = 120;
        //�V�[������HPText�I�u�W�F�N�g���擾
        this.hpleftText = GameObject.Find("HP_player1");
        this.hprightText = GameObject.Find("HP_player2");
    }

    // Update is called once per frame
    void Update()
    {
        //HP���X�V����
        if(HPLeft <= 0)
        {
            this.hpleftText.GetComponent<Text>().text = "HP:0";
        }
        else
        {
            this.hpleftText.GetComponent<Text>().text = "HP:" + HPLeft.ToString();
        }

        if (HPRight <= 0)
        {
            this.hprightText.GetComponent<Text>().text = "HP:0";
        }
        else
        {
            this.hprightText.GetComponent<Text>().text = "HP:" + HPRight.ToString();
        }


        
    }
}
