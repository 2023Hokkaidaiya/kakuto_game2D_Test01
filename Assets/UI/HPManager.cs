using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPManager : MonoBehaviour
{
 //================================================
    //左上のHPテキスト
    private GameObject hpleftText;
    //右上のHPテキスト
    private GameObject hprightText;
    //HP player1 player2計算用変数
    public int HPLeft;  //1
    public int HPRight; //2
//=================================================
    //評価用のポイント（一枚絵の分岐などに使う）
    //private GameObject leftEvP;
    //private GameObject rightEvP;
    private GameObject evPText;

    //評価ポイント計算用変数
    //public int EvPLeft; //1
    //public int EvPRight; //2
    //マイナスで1が有利　2が有利
    public int EvP = 0; //差額１－２


    //CLテキスト
    private GameObject closeText;
    public int Close = 0;
    //=================================================
    //左上のStandテキスト
    private GameObject stleftText;
    //右上のStandテキスト
    private GameObject strightText;
    //Stand計算用変数
    public int STLeft;
    public int STRight;
//=================================================
    // Start is called before the first frame update
    void Start()
    {
        //フレームを一定にする（コンピューターの速度に依存しない）
        Application.targetFrameRate = 120;
        //シーン中のHPTextオブジェクトを取得
        this.hpleftText = GameObject.Find("HP_player1");
        this.hprightText = GameObject.Find("HP_player2");
        //シーン中のCloseオブジェクト取得
        this.closeText = GameObject.Find("Close");
        //シーン中のEvPオブジェクト取得
        //this.leftEvP = GameObject.Find("LeftEvP");
        //this.rightEvP = GameObject.Find("RightEvP");
        this.evPText = GameObject.Find("EvP");
        //シーン中のSTTextオブジェクトを取得
        this.stleftText = GameObject.Find("ST_player1");
        this.strightText = GameObject.Find("ST_player2");
    }

    // Update is called once per frame
    void Update()
    {
        //HPを更新する=====================================================================
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

        //EvPを更新する(変数はEvP）========================================================
        //this.leftEvP.GetComponent<Text>().text = "EvP:" + EvPLeft.ToString();
        //this.rightEvP.GetComponent<Text>().text = "EvP:" + EvPRight.ToString();
        this.evPText.GetComponent<Text>().text = EvP.ToString();

        //Closeを更新する==================================================================
        this.closeText.GetComponent<Text>().text = Close.ToString();
        //Standを更新する(変数はSTLeft STRight)============================================
        this.stleftText.GetComponent<Text>().text = STLeft.ToString();
        this.strightText.GetComponent<Text>().text = STRight.ToString();
        //=================================================================================
    }
}
