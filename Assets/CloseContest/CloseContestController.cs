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
    //private bool isRun = true;
    //HPマネージャー
    //private GameObject hpManager;
    private HPManager hpManager;
    //HPに与えるダメージ量
    private int CloseContestDamage = 1;
    //状態（ステートマシン）
    private int stateNumber = 0;
    //汎用タイマー
    private float timerCounter;
    //debagテキスト（実況テキスト）
    //public GameObject stateText;

    //addforce用の変数
    public float moveForce = 10f;　//これをつど0にしたり10にしたりする
    //private bool isMovingLeft = false;
    //private bool isMovingRight = false;

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
        hpManager = GameObject.Find("HPManager").GetComponent<HPManager>();

        //有利の条件（初期値を調整することもできる、とりあえずは０）
        hpManager.EvP = 0;

        //デバッグ(ゲームスピードを遅くする
        Time.timeScale = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        ////攻撃に使用するボタン（優先権があれば攻撃できるようにしたい）=======
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Attack1();
        //}

        ////Addforce用=========================================================
        //// 左ボタンが押されたら
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    isMovingLeft = true;
        //}

        //// 右ボタンが押されたら
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    isMovingRight = true;
        //}

        //// ボタンが離されたら
        //if (Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    isMovingLeft = false;
        //}

        //if (Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    isMovingRight = false;
        //}
        //==================================================
        // 左ボタンが押されていれば左方向に力を加える
        //if (isMovingLeft)
        //{
        //    //myRigidbody.AddForce(Vector3.left * moveForce, ForceMode2D.Force);
        //    myRigidbody.AddForce(Vector3.left * 30f, ForceMode2D.Impulse);
        //    isMovingLeft = false;
        //}

        //// 右ボタンが押されていれば右方向に力を加える
        //if (isMovingRight)
        //{
        //    //myRigidbody.AddForce(Vector3.right * moveForce, ForceMode2D.Force);
        //    myRigidbody.AddForce(Vector3.right * 30f, ForceMode2D.Impulse);
        //    isMovingRight = false;
        //}
        //===================================================


        timerCounter += Time.deltaTime;

        //状態
        switch (stateNumber)
        {
            //5050
            case 0:
                {
                    // 左ボタンが押されたら（30を大きくすると速く、小さくすると遅くなる）
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        myRigidbody.AddForce(Vector3.left * 30f, ForceMode2D.Impulse);
                    }
                    // 右ボタンが押されたら（30を大きくすると速く、小さくすると遅くなる）
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        myRigidbody.AddForce(Vector3.right * 30f, ForceMode2D.Impulse);
                    }

                    //0ならどちらが仕掛けても良い
                    if (hpManager.EvP == 0)
                    {
                        //Player1の処理(Z押下で攻撃する）
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            //クリアー
                            timerCounter = 0.0f;

                            //フラグクリア(一本とガードがバッティングすることはないので、仕掛ける直前には必ずクリアすること)
                            myAnimator.SetBool("Ippon", false);
                            myAnimator.SetBool("Guard", false);

                            //Player1が仕掛ける
                            myAnimator.SetTrigger("Attack1AS1");

                            //状態の遷移
                            stateNumber = 1;
                        }

                        //Player2の処理（1秒経過後、ランダムでPlayer2が動き始める、10％としているが大きくするとより攻撃的になります）
                        if (timerCounter > 1.0f) 
                        {
                            //クリアー
                            timerCounter = 0.0f;

                            //ランダム 仮に10%（10は変数候補）
                            if (Random.Range(0, 100) < 10)
                            {
                                //フラグクリア
                                myAnimator.SetBool("Ippon", false);
                                myAnimator.SetBool("Guard", false);

                                //Player2が仕掛ける
                                myAnimator.SetTrigger("Attack1AS2");

                                //状態の遷移
                                stateNumber = 2;
                            }
                        }
                    }
                    //Player1が有利ならPlayer1のみが仕掛けることができる
                    else if (hpManager.EvP < 0)
                    {
                        //Player1の処理
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            //クリアー
                            timerCounter = 0.0f;

                            //フラグクリア
                            myAnimator.SetBool("Ippon", false);
                            myAnimator.SetBool("Guard", false);

                            //Player1が仕掛ける
                            myAnimator.SetTrigger("Attack1AS1");

                            //状態の遷移
                            stateNumber = 1;
                        }
                    }
                    //Player2が有利ならPlayer2のみが仕掛けることができる
                    else if (hpManager.EvP > 0)
                    {
                        //Player2の処理
                        if (timerCounter > 1.0f)
                        {
                            //クリアー
                            timerCounter = 0.0f;

                            //ランダム 仮に10%（10は変数候補）
                            if (Random.Range(0, 100) < 10)
                            {
                                //フラグクリア
                                myAnimator.SetBool("Ippon", false);
                                myAnimator.SetBool("Guard", false);

                                //Player2が仕掛ける
                                myAnimator.SetTrigger("Attack1AS2");

                                //状態の遷移
                                stateNumber = 2;
                            }
                        }
                    }
                }
                break;

            //Player1が仕掛ける
            case 1:
                {
                    //Player2の処理
                    if (timerCounter > 0.8f)　//Player1が攻撃を仕掛けてから0.8秒以上（変数候補）が経過したときに成り立ちます。
                    {
                        //ガード失敗
                        myAnimator.SetBool("Ippon", true);
                        myAnimator.SetInteger("Assign", 1);

                        //クリアー
                        timerCounter = 0.0f;

                        //状態の遷移
                        stateNumber = -1;
                    }
                    else
                    {
                        //ランダム 仮に5%
                        if (Random.Range(0, 100) < 5)　//Player1が攻撃を仕掛けてから0.8秒（変数候補）が経過するまでガードの判定
                        {
                            myAnimator.SetBool("Guard", true);
                            myAnimator.SetInteger("Assign", 2);

                            //Player2が有利になる
                            hpManager.EvP++;

                            //クリアー
                            timerCounter = 0.0f;

                            //状態の遷移
                            stateNumber = 3;
                        }
                    }
                }
                break;

            //Player2が仕掛ける
            case 2:
                {
                    //Player1の処理
                    if (timerCounter > 0.8f)
                    {
                        //ガード失敗
                        myAnimator.SetBool("Ippon", true);
                        myAnimator.SetInteger("Assign", 2);

                        //クリアー
                        timerCounter = 0.0f;

                        //状態の遷移
                        stateNumber = -2;
                    }
                    else
                    {
                        //ガードキー
                        if (Input.GetKeyDown(KeyCode.X))
                        {
                            myAnimator.SetBool("Guard", true);
                            myAnimator.SetInteger("Assign", 1);

                            //Player1が有利になる
                            hpManager.EvP--;

                            //クリアー
                            timerCounter = 0.0f;

                            //状態の遷移
                            stateNumber = 3;
                        }
                    }
                }
                break;

            //一定時間経過後、状態０に戻る
            case 3:
                {
                    if (timerCounter > 1.0f)
                    {
                        //状態の遷移
                        stateNumber = 0;
                    }
                }
                break;

            //一本 Player1が勝った
            case -1:
                {
                    if (timerCounter > 1.0f)
                    {
                        //ダメージを反映
                        hpManager.HPRight -= CloseContestDamage * TitleController.assign1Attack;

                        //クリアー
                        timerCounter = 0.0f;

                        //状態の遷移
                        stateNumber = 9;
                    }
                }
                break;

            //一本 Player2が勝った
            case -2:
                {
                    if (timerCounter > 1.0f)
                    {
                        //ダメージを反映
                        hpManager.HPLeft -= CloseContestDamage * TitleController.assign2Attack;

                        //クリアー
                        timerCounter = 0.0f;

                        //状態の遷移
                        stateNumber = 9;
                    }
                }
                break;

            //戻る
            case 9:
                {
                    if (timerCounter > 0.5f)
                    {
                        GameObject.Find("PlayerGenerator").GetComponent<PlayerGenerator>().CheckoutCloseContest();
                    }
                }
                break;
        }
    }

    //以下はアニメーションイベント用の関数

    //アニメーションイベントはアタッチしたスクリプトしか呼べない
    //イベントを受け取り、間接的にPlayerGeneratorの関数を呼ぶ
    public void CheckOut()
    {
        GameObject.Find("PlayerGenerator").GetComponent<PlayerGenerator>().CheckoutCloseContest();
    }
    //public void LeftHPDown()
    //{
    //    //ダメージを反映
    //    hpManager.GetComponent<HPManager>().HPLeft -= CloseContestDamage * TitleController.assign2Attack;
    //    //0.1秒後にCloseContestDamageをリセット
    //}
    //public void RightHPDown()
    //{
    //    //ダメージを反映
    //    hpManager.GetComponent<HPManager>().HPRight -= CloseContestDamage * TitleController.assign2Attack;
    //    //0.1秒後にCloseContestDamageをリセット
    //}
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
    //public void Attack1()
    //{
    //  //  myAnimator.SetTrigger("Attack1");
    //}
    ////1を勝たせる
    //public void WinnerIsAssign1()
    //{
    //   // myAnimator.SetInteger("Assign", 1);
    //}
    ////1に対するガード

    ////2を勝たせる
    //public void WinnerIsAssign2()
    //{
    //   // myAnimator.SetInteger("Assign", 2);
    //}
    ////0に戻す
    //public void NoSide()
    //{
    //   // myAnimator.SetInteger("Assign", 0);
    //}
    //実況（上の左右に何をやってるされているを代入するメソッドをつくる）
}
