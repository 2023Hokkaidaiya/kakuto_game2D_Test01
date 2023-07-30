using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseContestController : MonoBehaviour
{
    //Rigidbody2D変数
    Rigidbody2D rbody;
    //移動させるコンポーネントを入れる
    private Rigidbody2D myRigidbody;
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //着地できるレイヤー
    public LayerMask groundLayer;
    //移動可能
    private bool isRun = true;
    //HPマネージャー
    private GameObject hpManager;
    //HPに与えるダメージ量
    private int CloseContestDamage = 1;
    //状態（ステートマシン）
    private int stateNumber;
    //汎用タイマー
    private float timerCounter;
    //debagテキスト（実況テキスト）
    //public GameObject stateText;

    // Start is called before the first frame update
    void Start()
    {
        //アニメータコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //HPマネージャオブジェクト
        hpManager = GameObject.Find("HPManager");

        //デバッグ(ゲームスピードを遅くする
        Time.timeScale = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //以下はアニメーションイベント用の関数
    public void LeftHPDown()
    {
        //ダメージを反映
        hpManager.GetComponent<HPManager>().HPLeft -= CloseContestDamage * TitleController.assign2Attack;
        //0.1秒後にCloseContestDamageをリセット
    }
    public void RightHPDown()
    {
        //ダメージを反映
        hpManager.GetComponent<HPManager>().HPRight -= CloseContestDamage * TitleController.assign2Attack;
        //0.1秒後にCloseContestDamageをリセット
    }
    //実況（上の左右に何をやってるされているを代入するメソッドをつくる）
}
