using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    //左上のHPテキスト
    public GameObject hpleftText;
    //右上のテキスト
    public GameObject hprightText;
    //HP player1 player2計算用変数
    public int HPLeft;  //1
    public int HPRight; //2

    // Start is called before the first frame update
    void Start()
    {
        //HP player1 player2を0で初期化
        HPLeft = 0;
        HPRight = 0;
        //シーン中のHPTextオブジェクトを取得
        this.hpleftText = GameObject.Find("HP_player1");
        this.hprightText = GameObject.Find("HP_player2");
    }

    // Update is called once per frame
    void Update()
    {
        //HPを更新する
        this.hpleftText.GetComponent<Text>().text = "HP:" + HPLeft.ToString();
        this.hprightText.GetComponent<Text>().text = "HP:" + HPRight.ToString();
    }
}
