using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    //Rigidbody2D変数
    Rigidbody2D rbody;
    //入力
    float axisH = 0.0f;
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //移動させるコンポーネントを入れる
    private Rigidbody2D myRigidbody;

    //移動量（定数）
    private float VELOCITY = 2.5f;

    //ジャンプ量（定数）
    private float JUMPPOWER = 6.0f;

    //地面
    private bool isGround = false;

    //着地できるレイヤー
    public LayerMask groundLayer;

    //移動可能
    private bool isRun = true;

    //攻撃オブジェクト
    private GameObject attackObject;

    //プレイヤーか敵か　左：「１」　右：２　、COM　左：-1、右：「−2」
    public int assign;

    //攻撃のPrefab
    public GameObject attackPrefab;

    //HPmaga
    private GameObject hpManager;
    //相手(インスペクターでクロスさせてください）
    public GameObject otherPlayer;
    //状態（ステートマシン）
    private int stateNumber;
    //汎用タイマー
    private float timerCounter;
    //debagテキスト
    public GameObject stateText;

    //攻撃を受ける（相手から知らされる）
    public bool isBeAttacked = false;
    //ガードしている
    private bool isGuard = false;

    //---------------------------------------------------
    //スタート
    //---------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //アニメータコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //HPマネージャオブジェクト
        hpManager = GameObject.Find("HPManager");

        //子オブジェクトを取得
        //this.attackObject = transform.Find("Attack").gameObject;

        //衝突判定dakeをディスエーブル
        //this.attackObject.GetComponent<BoxCollider2D>().enabled = false;

        //アサイン１→Leftstate　アサイン２→Rightstate
        if (assign == 1)
        {
            //プレイヤー
            this.stateText = GameObject.Find("Leftstate");
        }
        else if (assign == -2)
        {
            //プレイヤー（COM)
            this.stateText = GameObject.Find("Rightstate");
        }

        //デバッグ(ゲームスピードを遅くする
        Time.timeScale = 1.0f;
    }
    //＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿
    //プレイヤー
    //＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿
    void Player()
    {
        //移動が可能
        if (isRun)
        {
            //移動
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //ジャンプ
                if (Input.GetKey(KeyCode.Space) && isGround)
                {
                    //左移動＋ジャンプ
                    this.myRigidbody.velocity = new Vector2(-VELOCITY, JUMPPOWER);
                }
                else
                {
                    myAnimator.SetInteger("Run", -1);

                    //左移動
                    this.myRigidbody.velocity = new Vector2(-VELOCITY, this.myRigidbody.velocity.y);
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                //ジャンプ
                if (Input.GetKey(KeyCode.Space) && isGround)
                {
                    //右移動＋ジャンプ
                    this.myRigidbody.velocity = new Vector2(VELOCITY, JUMPPOWER);
                }
                else
                {
                    myAnimator.SetInteger("Run", 1);

                    //右移動
                    this.myRigidbody.velocity = new Vector2(VELOCITY, this.myRigidbody.velocity.y);
                }
            }
            else
            {
                myAnimator.SetInteger("Run", 0);

                //停止
                this.myRigidbody.velocity = new Vector2(0.0f, this.myRigidbody.velocity.y);

                //ジャンプ
                if (Input.GetKey(KeyCode.Space) && isGround)
                {
                    //垂直ジャンプ
                    this.myRigidbody.velocity = new Vector2(0.0f, JUMPPOWER);
                }
            }
        }
        else
        {
            //停止
            this.myRigidbody.velocity = new Vector2(0.0f, this.myRigidbody.velocity.y);
        }

        //攻撃
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //動けなくする
            isRun = false;

            myAnimator.SetTrigger("PreAttack");
        }
        //ガード
        if (Input.GetKeyDown(KeyCode.X))
        {
            //動けなくする
            isRun = false;

            myAnimator.SetTrigger("Guard");
        }

    }

    //----------------------------------------------------
    //便利な関数
    //----------------------------------------------------

    //距離を求める
    float getLength(Vector2 current, Vector2 target)
    {
        return Mathf.Sqrt(((current.x - target.x) * (current.x - target.x)) + ((current.y - target.y) * (current.y - target.y)));
    }
    //方向を求める ※オイラー（-180〜0〜+180)
    float getDirection(Vector2 current, Vector2 target)
    {
        Vector3 value = target - current;
        return Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg; //ラジアン→オイラー
    }

    //----------------------------------------------------
    //プレイヤー（COM)
    //----------------------------------------------------

    // 状態(列挙)
    private enum states : int { Idoling = 0, Advance, Wait, Approach, Back, Attack,  Guard };

    void PlayerComuter()
    {
        //相手の距離
        float length = getLength(this.transform.position, otherPlayer.transform.position);
        //Debug.Log("length" + length);

        //状態
        switch (stateNumber)
        {
            //待機（スタート）
            case (int)states.Idoling:
                {
                    if (timerCounter > 3.0f)
                    {
                        //状態を遷移
                        stateNumber = (int)states.Advance;
                    }
                }
                break;

            //少し近づく
            case (int)states.Advance:
                {
                    //プレイヤーが遠いか
                    if (length > 5.0f)
                    {
                        myAnimator.SetInteger("Run", 1);
                        this.myRigidbody.velocity = new Vector2(VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);

                    }
                    else
                    {
                        //停止
                        myAnimator.SetInteger("Run", 0);
                        this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);

                        //リセット
                        timerCounter = 0.0f;
                        //状態を遷移
                        stateNumber = (int)states.Wait;
                    }
                }
                break;

            //待機(次の行動考える）
            case (int)states.Wait:
                {
                    //停止
                    myAnimator.SetInteger("Run", 0);
                    this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
                    //タイマー
                    if (timerCounter > 3.0f)
                    {
                        //向こうから近付いてきたか
                        if (length < 3.0f)
                        {
                            //リセット
                            timerCounter = 0.0f;
                            //状態を遷移
                            stateNumber = (int)states.Back;
                        }
                        else
                        {
                            //リセット
                            timerCounter = 0.0f;
                            //状態を遷移
                            stateNumber = (int)states.Approach;
                        }
                    }
                }
                break;

            //攻撃可能距離に近づく
            case (int)states.Approach:
                {
                    if (length > 2.0f)
                    {
                        myAnimator.SetInteger("Run", 1);
                        this.myRigidbody.velocity = new Vector2(VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);
                        //リセット
                        timerCounter = 0.0f;
                        //状態を遷移
                        stateNumber = (int)states.Attack;
                    }
                    else
                    {
                        //停止
                        myAnimator.SetInteger("Run", 0);
                        this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
                        //リセット
                        timerCounter = 0.0f;
                        //状態を遷移
                        stateNumber = (int)states.Back;
                    }
                }
                break;

            //少し離れる
            case (int)states.Back:
                {
                    //プレイヤーが近い
                    if (length < 5.0f)
                    {
                        myAnimator.SetInteger("Run", -1);
                        this.myRigidbody.velocity = new Vector2(-VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);
                        //リセット
                        timerCounter = 0.0f;
                        //状態を遷移
                        stateNumber = (int)states.Guard;
                    }
                    else
                    {
                        //停止
                        myAnimator.SetInteger("Run", 0);
                        this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
                        //リセット
                        timerCounter = 0.0f;
                        //状態を遷移
                        stateNumber = (int)states.Approach;
                    }
                }
                break;

            //Attack
            case (int)states.Attack:
                {
                    //停止
                    myAnimator.SetInteger("Run", 0);
                    this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
                    
                    myAnimator.SetTrigger("PreAttack");
                    
                    //リセット
                    timerCounter = 0.0f;
                    //状態を遷移
                    stateNumber = (int)states.Wait;
                }
                break;




            //Guard
            case (int)states.Guard:
                {
                    //停止
                    myAnimator.SetInteger("Run", 0);
                    this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
                    //ガード
                    myAnimator.SetTrigger("Guard");
                    
                    //リセット
                    timerCounter = 0.0f;
                    //状態を遷移
                    stateNumber = (int)states.Wait;
                }
                break;
        }

        //デバッグ
        switch (stateNumber)
        {
            case 0: stateText.GetComponent<Text>().text = "Idoling"; break;
            case 1: stateText.GetComponent<Text>().text = "Advance"; break;
            case 2: stateText.GetComponent<Text>().text = "Wait"; break;
            case 3: stateText.GetComponent<Text>().text = "Approach"; break;
            case 4: stateText.GetComponent<Text>().text = "Back"; break;
            case 5: stateText.GetComponent<Text>().text = "Attack"; break;
            case 6: stateText.GetComponent<Text>().text = "Guard"; break;
        }

        //タイマー
        timerCounter += Time.deltaTime;
    }

    // Update is called once per frame
    //＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿
    //アップデート
    //＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿
    void Update()
    {
        if (assign == 1)
        {
            //プレイヤー
            Player();
        }
        else if (assign == -2)
        {
            //プレイヤー（COM)
            PlayerComuter();
        }

        //ジャンプの遷移
        this.myAnimator.SetFloat("Jump", this.myRigidbody.velocity.y);
        this.myAnimator.SetBool("Ground", isGround);
    }

    public void PreAttackStart()
    {
        //攻撃開始を知らせる（追加）
        otherPlayer.GetComponent<PlayerController>().isBeAttacked = true;

        Debug.Log("PreAttackStart");
    }

    public void AttackStart()
    {
        //攻撃を生成
        attackObject = Instantiate(attackPrefab, this.transform.position + new Vector3(1.29f * transform.localScale.x, 1.44f, 0f), Quaternion.identity);

        Debug.Log("AttackStart");
    }

    public void AttackEnd()
    {
        //攻撃終了を知らせる（追加）
        otherPlayer.GetComponent<PlayerController>().isBeAttacked = false;

        //攻撃を破棄する
        Destroy(attackObject.gameObject);

        //動けるようになる
        isRun = true;

        Debug.Log("AttackEnd");
    }

    public void GuardStart()
    {
        //ガード開始
        isGuard = true;

        Debug.Log("GuardStart");
    }

    public void GuardEnd()
    {
        //ガード終了
        isGuard = false;

        //動けるようになる
        isRun = true;

        Debug.Log("GuardEnd");
    }

    /*
    private void FixedUpdate()
    {
        //地上判定
        isGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        if(isGround || axisH != 0)
        {
            //地面上OR速度が０ではない
            //速度を更新する
            rbody.velocity = new Vector2(VELOCITY * axisH,rbody.velocity.y);
        }
    }
    */


    //

    //
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack")
        {
            myAnimator.SetTrigger("Damage");

            if (assign == 1)
            {
                hpManager.GetComponent<HPManager>().HPLeft-= TitleController.assign2Attack;

                //プレイヤーがダメージを受けた
                //吹っ飛ぶ
                myRigidbody.AddForce(new Vector3(-5000f * transform.localScale.x, 0f, 0f));

            }
            else if (assign == -2)
            {
                //プレイヤー（COM)がダメージを受けた
                hpManager.GetComponent<HPManager>().HPRight -= TitleController.assign1Attack;
                //吹っ飛ぶ
                myRigidbody.AddForce(new Vector3(-5000f * transform.localScale.x, 0f, 0f));
            }

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }


    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
}