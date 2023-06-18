using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        //アニメータコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //子オブジェクトを取得
        this.attackObject = transform.Find("Attack").gameObject;

        //衝突をオフ
        this.attackObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
                this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);

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
            this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
        }

        //攻撃
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //動けなくする
            isRun = false;

            myAnimator.SetTrigger("PreAttack");
        }

        //Debug.Log("velocity: " + this.myRigidbody.velocity.y);
        //Debug.Log("Ground: " + isGround); 

        this.myAnimator.SetFloat("Jump", this.myRigidbody.velocity.y);
        this.myAnimator.SetBool("Ground", isGround);

        //コライダーのOFF/ONのサンプル
        //this.GetComponent<BoxCollider2D>().enabled = false;
        //this.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void PreAttackStart()
    {
        Debug.Log("PreAttackStart");
    }

    public void AttackStart()
    {
        //衝突をオン
        this.attackObject.SetActive(true);
        Debug.Log("AttackStart");
    }

    public void AttackEnd()
    {
        //衝突をオフ
        this.attackObject.SetActive(false);
        isRun = true;
        Debug.Log("AttackEnd");
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        if (other.gameObject.tag == "Attack")
        {
            myAnimator.SetTrigger("Damage");
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