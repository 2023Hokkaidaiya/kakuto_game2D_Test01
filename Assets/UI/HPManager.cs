using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPManager : MonoBehaviour
{
    //左上のHPテキスト
    private GameObject hpleftText;
    //右上のテキスト
    private GameObject hprightText;
    //HP player1 player2計算用変数
    public int HPLeft;  //1
    public int HPRight; //2

    //評価用のポイント（一枚絵の分岐などに使う）
    private GameObject leftEvP;
    private GameObject rightEvP;
    private GameObject evP;
    //評価ポイント計算用
    public int EvPLeft; //1
    public int EvPRight; //2
    //マイナスで1が有利　2が有利
    public int EvP = 0; //差額１－２

    // Start is called before the first frame update
    void Start()
    {
        //フレームを一定にする（コンピューターの速度に依存しない）
        Application.targetFrameRate = 120;
        //シーン中のHPTextオブジェクトを取得
        this.hpleftText = GameObject.Find("HP_player1");
        this.hprightText = GameObject.Find("HP_player2");
        //シーン中のEvPオブジェクト取得
        this.leftEvP = GameObject.Find("LeftEvP");
        this.rightEvP = GameObject.Find("RightEvP");
        this.evP = GameObject.Find("EvP");
    }

    // Update is called once per frame
    void Update()
    {
        //HPを更新する
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

        //EvPを更新する
        this.leftEvP.GetComponent<Text>().text = "EvP:" + EvPLeft.ToString();
        this.rightEvP.GetComponent<Text>().text = "EvP:" + EvPRight.ToString();
        this.evP.GetComponent<Text>().text = EvP.ToString();

    }
}
