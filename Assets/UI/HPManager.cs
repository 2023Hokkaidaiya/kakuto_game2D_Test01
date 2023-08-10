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

    //�]���p�̃|�C���g�i�ꖇ�G�̕���ȂǂɎg���j
    private GameObject leftEvP;
    private GameObject rightEvP;
    private GameObject evP;
    //�]���|�C���g�v�Z�p
    public int EvPLeft; //1
    public int EvPRight; //2
    //�}�C�i�X��1���L���@2���L��
    public int EvP = 0; //���z�P�|�Q

    // Start is called before the first frame update
    void Start()
    {
        //�t���[�������ɂ���i�R���s���[�^�[�̑��x�Ɉˑ����Ȃ��j
        Application.targetFrameRate = 120;
        //�V�[������HPText�I�u�W�F�N�g���擾
        this.hpleftText = GameObject.Find("HP_player1");
        this.hprightText = GameObject.Find("HP_player2");
        //�V�[������EvP�I�u�W�F�N�g�擾
        this.leftEvP = GameObject.Find("LeftEvP");
        this.rightEvP = GameObject.Find("RightEvP");
        this.evP = GameObject.Find("EvP");
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

        //EvP���X�V����
        this.leftEvP.GetComponent<Text>().text = "EvP:" + EvPLeft.ToString();
        this.rightEvP.GetComponent<Text>().text = "EvP:" + EvPRight.ToString();
        this.evP.GetComponent<Text>().text = EvP.ToString();

    }
}
