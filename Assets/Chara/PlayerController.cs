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
    private float JUMPPOWER = 4.0f;

    //地面
    private bool isGround = false;
    //着地できるレイヤー
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        //アニメータコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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
                myAnimator.SetBool("Run", true);

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
                myAnimator.SetBool("Run", true);

                //右移動
                this.myRigidbody.velocity = new Vector2(VELOCITY, this.myRigidbody.velocity.y);
            }
        }
        else
        {
            myAnimator.SetBool("Run", false);

            //ジャンプ
            if (Input.GetKey(KeyCode.Space) && isGround)
            {
                //垂直ジャンプ
                this.myRigidbody.velocity = new Vector2(0.0f, JUMPPOWER);
            }
        }

        //Debug.Log("velocity: " + this.myRigidbody.velocity.y);
        //Debug.Log("Ground: " + isGround); 

        this.myAnimator.SetFloat("Jump", this.myRigidbody.velocity.y);
        this.myAnimator.SetBool("Ground", isGround);

        //コライダーのOFF/ONのサンプル
        //this.GetComponent<BoxCollider2D>().enabled = false;
        //this.GetComponent<BoxCollider2D>().enabled = true;
    }

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
}
