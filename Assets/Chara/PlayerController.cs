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
    private float VELOCITY = 6.5f;

    //ジャンプ量（定数）
    private float JUMPPOWER = 6.0f;

    //地面
    private bool isGround = false;

    //着地できるレイヤー
    public LayerMask groundLayer;

    //移動可能
    private bool isRun = true;

    //攻撃オブジェクト(必要に応じて追加）
    private GameObject attackObject;
    //攻撃オブジェクト２（DeathBrow）
    private GameObject deathbrowObject;

    //プレイヤーか敵か　左：「１」　右：２　、COM　左：-1、右：「－2」
    public int assign;

    //攻撃のPrefab
    public GameObject attackPrefab;
    public GameObject deathbrowPrefab;

    //HPマネージャー
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

    //SoundEffect
    public AudioClip SE1Slashed; //ザシュッっとした音
    public AudioClip SE2SoundOfSword; //ガードした時の剣戟音
    public AudioClip SE3Footsteps; //足音

    //isEnd
    private bool isEnd = false;

    //Comのガード率　１００で必ずガード　０でしない
    public int guardRate;

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
        //衝突判定だけをディスエーブル
        //this.attackObject.GetComponent<BoxCollider2D>().enabled = false;

        //アサイン１→Leftstate　アサイン２→Rightstate(通常１VS-2）
        if (assign == 1 || assign == -1)
            {
            //プレイヤー
            this.stateText = GameObject.Find("Leftstate");
        }
        else if (assign == 2 || assign == -2)
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
        //生きている
        if (isEnd == false)
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
    }

    //----------------------------------------------------
    //便利な関数
    //----------------------------------------------------

    //距離を求める
    float getLength(Vector2 current, Vector2 target)
    {
        return Mathf.Sqrt(((current.x - target.x) * (current.x - target.x)) + ((current.y - target.y) * (current.y - target.y)));
    }
    //方向を求める ※オイラー（-180～0～+180)
    float getDirection(Vector2 current, Vector2 target)
    {
        Vector3 value = target - current;
        return Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg; //ラジアン→オイラー
    }

    //----------------------------------------------------
    //プレイヤー（COM)
    //----------------------------------------------------

    // 状態(列挙)
    private enum states : int { Thinking = 0, Forward, Back, Attack, Guard, Damage, Approach, Die };

    //Idoling = 0, Advance, Wait, Approach, Back, Attack,  Guard ,Die};

    void PlayerComuter()
    {
        //相手の距離
        float length = getLength(this.transform.position, otherPlayer.transform.position);

        //状態
        switch (stateNumber)
        {
            case (int)states.Thinking:
                {
                    //停止
                    myAnimator.SetInteger("Run", 0);
                    this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);

                    //タイマー
                    if (timerCounter > 1.0f)
                    {
                        //次の行動を決める

                        //相手まで5メートル以上？
                        if (length > 5.0f)
                        {
                            //クリアー
                            timerCounter = 0.0f;

                            //遷移
                            stateNumber = (int)states.Forward;
                        }
                        else
                        {
                            //相手まで2メートル以下？
                            if (length < 2.0f)
                            {
                                //攻撃が当たる、または相手の攻撃が当たる

                                //相手が攻撃開始？
                                if (isBeAttacked && Random.Range(0,100) <guardRate)
                                {
                                    //ガードの開始
                                    myAnimator.SetTrigger("Guard");

                                    //クリアー
                                    timerCounter = 0.0f;

                                    //遷移
                                    stateNumber = (int)states.Guard;
                                }
                                else
                                {
                                    //例えば相手のライフと自分のライフを比較

                                    //がんがんいこうぜ = 7
                                    //ふつう = 5
                                    //いのちだいじに = 3

                                    int random = 3;

                                    //ランダム（30～70%）
                                    if (Random.Range(1, 11) <= random)
                                    {
                                        //攻撃の開始
                                        myAnimator.SetTrigger("PreAttack");

                                        //クリアー
                                        timerCounter = 0.0f;

                                        //遷移
                                        stateNumber = (int)states.Attack;
                                    }
                                    else
                                    {
                                        //クリアー
                                        timerCounter = 0.0f;

                                        //遷移
                                        stateNumber = (int)states.Back;
                                    }
                                }
                            }
                            else
                            {
                                //例えば相手のライフと自分のライフを比較

                                //がんがんいこうぜ = 7
                                //ふつう = 5
                                //いのちだいじに = 3

                                int random = 3;

                                //ランダム（30～70%）
                                if (Random.Range(1, 11) <= random)
                                {
                                    //クリアー
                                    timerCounter = 0.0f;

                                    //遷移
                                    stateNumber = (int)states.Approach;
                                }
                                else
                                {
                                    //クリアー
                                    timerCounter = 0.0f;

                                    //遷移
                                    stateNumber = (int)states.Back;
                                }
                            }
                        }
                    }
                    else 
                    {
                        //n秒ボートしてる
                        if (length < 2.0f)
                        {
                            //相手が攻撃開始？
                            if (isBeAttacked && Random.Range(0, 100) < guardRate)
                            {
                                //ガードの開始
                                myAnimator.SetTrigger("Guard");

                                //クリアー
                                timerCounter = 0.0f;

                                //遷移
                                stateNumber = (int)states.Guard;
                            }
                            else
                            {
                                Debug.Log("ガードキャンセル");
                            }
                        }
                    }

                }
                break;

            case (int)states.Forward:
                {
                    myAnimator.SetInteger("Run", 1);
                    this.myRigidbody.velocity = new Vector2(VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);

                    //相手まで5メートル以下？
                    if (length < 5.0f)
                    {
                        //クリアー
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(0.0f, 0.5f);

                        //遷移
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Back:
                {
                    myAnimator.SetInteger("Run", -1);
                    this.myRigidbody.velocity = new Vector2(-VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);

                    //相手まで7メートル以上？
                    if (length > 7.0f)
                    {
                        //クリアー
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(0.0f, 0.5f);

                        //遷移
                        stateNumber = (int)states.Thinking;
                    }
                    //左Com
                    if(assign == -1)
                    {
                        //左端
                        if(this.transform.position.x < -7.8)
                        {
                            //クリア
                            //timerCounter = 0.0f;
                            timerCounter = Random.Range(0.0f, 0.5f);

                            //遷移
                            stateNumber = (int)states.Thinking;
                        }

                    }
                    //右Com
                    if(assign == -2)
                    {
                        //右端
                        if (this.transform.position.x > 7.8)
                        {
                            //クリア
                            //timerCounter = 0.0f;
                            timerCounter = Random.Range(0.0f, 0.5f);

                            //遷移
                            stateNumber = (int)states.Thinking;
                        }
                    }

                }
                break;

            case (int)states.Attack:
                {
                    //アニメーション終了までの待機
                    if (timerCounter > 1.0f)
                    {
                        //クリアー
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(-0.5f, 0.0f);

                        //遷移
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Guard:
                {
                    //アニメーション終了までの待機
                    if (timerCounter > 1.0f)
                    {
                        //クリアー
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(0.0f, 0.5f);

                        //遷移
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Damage:
                {
                    //アニメーション終了までの待機
                    if (timerCounter > 0.1f)
                    {
                        //クリアー
                        timerCounter = 0.4f;
                        //timerCounter = Random.Range(-1.0f, 0.0f);

                        //遷移
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Approach:
                {
                    myAnimator.SetInteger("Run", 1);
                    this.myRigidbody.velocity = new Vector2(VELOCITY * transform.localScale.x, this.myRigidbody.velocity.y);

                    //相手まで2メートル以下？
                    if (length < 2.0f)
                    {
                        //クリアー
                        //timerCounter = 0.0f;
                        timerCounter = Random.Range(0.0f, 0.5f);

                        //遷移
                        stateNumber = (int)states.Thinking;
                    }
                }
                break;

            case (int)states.Die:
                {
                    //何もしない
                }
                break;

        }

        //デバッグ
        switch (stateNumber)
        {
            case 0: stateText.GetComponent<Text>().text = "Thinking " + length.ToString("F3"); break;
            case 1: stateText.GetComponent<Text>().text = "Forward " + length.ToString("F3"); break;
            case 2: stateText.GetComponent<Text>().text = "Back " + length.ToString("F3"); break;
            case 3: stateText.GetComponent<Text>().text = "Attack " + length.ToString("F3"); break;
            case 4: stateText.GetComponent<Text>().text = "Guard " + length.ToString("F3") + "" + isGuard; break;
            case 5: stateText.GetComponent<Text>().text = "Damage " + length.ToString("F3"); break;
            case 6: stateText.GetComponent<Text>().text = "Approach " + length.ToString("F3"); break;
            case 7: stateText.GetComponent<Text>().text = "Die " + length.ToString("F3"); break;
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
        if (assign == 1 || assign == 2)
        {
            //プレイヤー
            Player();
        }
        else if (assign == -2 || assign == -1)
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

        //Debug.Log("PreAttackStart");
    }

    public void AttackStart()
    {
        //
        if (gameObject.name == "Player1DeathBrowPrefab(Clone)")
        {
            //攻撃を生成(3倍横に大きくしている）
            deathbrowObject = Instantiate(deathbrowPrefab, this.transform.position + new Vector3(5f * transform.localScale.x, 1.44f, 0f), Quaternion.identity);

            //Debug.Log("AttackStart");
        }
        else
        {
            //攻撃を生成
            attackObject = Instantiate(attackPrefab, this.transform.position + new Vector3(1.29f * transform.localScale.x, 1.44f, 0f), Quaternion.identity);

            //Debug.Log("AttackStart");
        }
     
    }

    public void AttackEnd()
    {
        //攻撃終了を知らせる（追加）
        otherPlayer.GetComponent<PlayerController>().isBeAttacked = false;

        if(gameObject.name == "Player1DeathBrowPrefab(Clone)")
        {
            Destroy(deathbrowObject.gameObject);
        }
        else
        {
            //攻撃を破棄する
            Destroy(attackObject.gameObject);
        }
        //動けるようになる
        isRun = true;

        //Debug.Log("AttackEnd");
    }

    public void GuardStart()
    {
        //ガード開始
        isGuard = true;

        //Debug.Log("GuardStart");
    }

    public void GuardEnd()
    {
        //ガード終了
        isGuard = false;

        //動けるようになる
        isRun = true;

        //Debug.Log("GuardEnd");
    }

    public void DamageEnd()
    {
        //動けるようになる
        isRun = true;

        //Debug.Log("DamageEnd");
    }

    public void Footsteps()
    {
        GetComponent<AudioSource>().PlayOneShot(SE3Footsteps);
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
            

            if (assign == 1)
            {
                //動けるなくなる
                isRun = false;

                if (isGuard == false)
                {
                    //ダメージのアニメーション
                    myAnimator.SetTrigger("Damage");

                    //ダメージ効果音
                    GetComponent<AudioSource>().PlayOneShot(SE1Slashed); 

                    //ダメージを反映
                    hpManager.GetComponent<HPManager>().HPLeft -= TitleController.assign2Attack;

                    //LifeCheckして
                    if (hpManager.GetComponent<HPManager>().HPLeft <= 0)
                    {
                        //死亡アニメーしょん
                        myAnimator.SetBool("Die", true);
                        //終了
                        isEnd = true;
                    }
                }
                else
                {
                    //Debug.Log("ガード成功");
                    //ガード効果音
                    //ガード成功したのでダメージを受けない
                    GetComponent<AudioSource>().PlayOneShot(SE2SoundOfSword);
                }

                //プレイヤーがダメージを受けた
                //吹っ飛ぶ
                //myRigidbody.AddForce(new Vector3(-5000f * transform.localScale.x, 0f, 0f));
                myRigidbody.AddForce(new Vector3(-5f * transform.localScale.x, 0f, 0f), ForceMode2D.Impulse);
            }
            else if (assign == -2)
            {
                //クリアー
                timerCounter = 0.0f;
                
                              
                if (isGuard == false)
                {
                    //ダメージのアニメーション
                    myAnimator.SetTrigger("Damage");
                    //ダメージ効果音
                    GetComponent<AudioSource>().PlayOneShot(SE1Slashed);
                    //遷移
                    stateNumber = (int)states.Damage;
                    //プレイヤー（COM)がダメージを受けた
                    hpManager.GetComponent<HPManager>().HPRight -= TitleController.assign1Attack;
                    
                    //LifeCheckして
                    if (hpManager.GetComponent<HPManager>().HPRight <= 0)
                    {
                        //死亡アニメーション
                        myAnimator.SetBool("Die", true);
                        //終了
                        stateNumber = (int)states.Die;
                    }

                }
                else
                {
                    //ガード効果音
                    GetComponent<AudioSource>().PlayOneShot(SE2SoundOfSword);
                }

                //吹っ飛ぶ
                //myRigidbody.AddForce(new Vector3(-5000f * transform.localScale.x, 0f, 0f));
                myRigidbody.AddForce(new Vector3(-5f * transform.localScale.x, 0f, 0f), ForceMode2D.Impulse);
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