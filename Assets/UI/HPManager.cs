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

    // Start is called before the first frame update
    void Start()
    {
        //フレームを一定にする（コンピューターの速度に依存しない）
        Application.targetFrameRate = 120;
        //シーン中のHPTextオブジェクトを取得
        this.hpleftText = GameObject.Find("HP_player1");
        this.hprightText = GameObject.Find("HP_player2");
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


        
    }
}
