using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPManager : MonoBehaviour
{
 //================================================
    //�����HP�e�L�X�g
    private GameObject hpleftText;
    //�E���HP�e�L�X�g
    private GameObject hprightText;
    //HP player1 player2�v�Z�p�ϐ�
    public int HPLeft;  //1
    public int HPRight; //2
//=================================================
    //�]���p�̃|�C���g�i�ꖇ�G�̕���ȂǂɎg���j
    //private GameObject leftEvP;
    //private GameObject rightEvP;
    private GameObject evPText;

    //�]���|�C���g�v�Z�p�ϐ�
    //public int EvPLeft; //1
    //public int EvPRight; //2
    //�}�C�i�X��1���L���@2���L��
    public int EvP = 0; //���z�P�|�Q


    //CL�e�L�X�g
    private GameObject closeText;
    public int Close = 0;
    //=================================================
    //�����Stand�e�L�X�g
    private GameObject stleftText;
    //�E���Stand�e�L�X�g
    private GameObject strightText;
    //Stand�v�Z�p�ϐ�
    public int STLeft;
    public int STRight;
//=================================================
    // Start is called before the first frame update
    void Start()
    {
        //�t���[�������ɂ���i�R���s���[�^�[�̑��x�Ɉˑ����Ȃ��j
        Application.targetFrameRate = 120;
        //�V�[������HPText�I�u�W�F�N�g���擾
        this.hpleftText = GameObject.Find("HP_player1");
        this.hprightText = GameObject.Find("HP_player2");
        //�V�[������Close�I�u�W�F�N�g�擾
        this.closeText = GameObject.Find("Close");
        //�V�[������EvP�I�u�W�F�N�g�擾
        //this.leftEvP = GameObject.Find("LeftEvP");
        //this.rightEvP = GameObject.Find("RightEvP");
        this.evPText = GameObject.Find("EvP");
        //�V�[������STText�I�u�W�F�N�g���擾
        this.stleftText = GameObject.Find("ST_player1");
        this.strightText = GameObject.Find("ST_player2");
    }

    // Update is called once per frame
    void Update()
    {
        //HP���X�V����=====================================================================
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

        //EvP���X�V����(�ϐ���EvP�j========================================================
        //this.leftEvP.GetComponent<Text>().text = "EvP:" + EvPLeft.ToString();
        //this.rightEvP.GetComponent<Text>().text = "EvP:" + EvPRight.ToString();
        this.evPText.GetComponent<Text>().text = EvP.ToString();

        //Close���X�V����==================================================================
        this.closeText.GetComponent<Text>().text = Close.ToString();
        //Stand���X�V����(�ϐ���STLeft STRight)============================================
        this.stleftText.GetComponent<Text>().text = STLeft.ToString();
        this.strightText.GetComponent<Text>().text = STRight.ToString();
        //=================================================================================
    }
}
