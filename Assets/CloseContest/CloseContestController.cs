using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseContestController : MonoBehaviour
{
    //Rigidbody2D変数
    //Rigidbody2D rbody;
    //移動させるコンポーネントを入れる
    private Rigidbody2D myRigidbody;
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //着地できるレイヤー
    //public LayerMask groundLayer;
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

    //addforce用の変数
    public float moveForce = 10f;　//これをつど0にしたり10にしたりする
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    //SoundEffect
    public AudioClip SE1Slashed; //ザシュッっとした音
    public AudioClip SE2SoundOfSword; //ガードした時の剣戟音
    public AudioClip SE3Footsteps; //足音

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
    //===================================================================
    //Addforce用=========================================================
        // 左ボタンが押されたら
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isMovingLeft = true;
        }

        // 右ボタンが押されたら
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isMovingRight = true;
        }

        // ボタンが離されたら
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isMovingLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isMovingRight = false;
        }
        //==================================================
        // 左ボタンが押されていれば左方向に力を加える
        if (isMovingLeft)
        {
            myRigidbody.AddForce(Vector3.left * moveForce, ForceMode2D.Force);
        }

        // 右ボタンが押されていれば右方向に力を加える
        if (isMovingRight)
        {
            myRigidbody.AddForce(Vector3.right * moveForce, ForceMode2D.Force);
        }
        //===================================================
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
    public void Slashed()
    {
        //ダメージ効果音
        GetComponent<AudioSource>().PlayOneShot(SE1Slashed);
    }
    public void SoundOfSword()
    {
        //ガード効果音
        GetComponent<AudioSource>().PlayOneShot(SE2SoundOfSword);
    }
    public void Footsteps()
    {
        GetComponent<AudioSource>().PlayOneShot(SE3Footsteps);
    }
    //実況（上の左右に何をやってるされているを代入するメソッドをつくる）
}
