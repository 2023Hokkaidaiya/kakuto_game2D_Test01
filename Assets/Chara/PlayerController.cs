using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Rigidbody2D•Ï”
    Rigidbody2D rbody;
    //“ü—Í
    float axisH = 0.0f;
    //ƒAƒjƒ[ƒVƒ‡ƒ“‚·‚é‚½‚ß‚ÌƒRƒ“ƒ|[ƒlƒ“ƒg‚ğ“ü‚ê‚é
    private Animator myAnimator;

    //ˆÚ“®‚³‚¹‚éƒRƒ“ƒ|[ƒlƒ“ƒg‚ğ“ü‚ê‚é
    private Rigidbody2D myRigidbody;

    //ˆÚ“®—Êi’è”j
    private float VELOCITY = 2.5f;

    //ƒWƒƒƒ“ƒv—Êi’è”j
    private float JUMPPOWER = 6.0f;

    //’n–Ê
    private bool isGround = false;

    //’…’n‚Å‚«‚éƒŒƒCƒ„[
    public LayerMask groundLayer;

    //ˆÚ“®‰Â”\
    private bool isRun = true;

    //UŒ‚ƒIƒuƒWƒFƒNƒg
    private GameObject attackObject;

    //ƒvƒŒƒCƒ„[‚©“G‚©@¶Fu‚Pv@‰EF‚Q@ACOM@¶F-1A‰EFu|2v
    public int assign;

    //UŒ‚‚ÌPrefab
    public GameObject attackPrefab;

    //---------------------------------------------------
    //ƒXƒ^[ƒg
    //---------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //ƒAƒjƒ[ƒ^ƒRƒ“ƒ|[ƒlƒ“ƒg‚ğæ“¾
        this.myAnimator = GetComponent<Animator>();

        //RigidbodyƒRƒ“ƒ|[ƒlƒ“ƒg‚ğæ“¾
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //qƒIƒuƒWƒFƒNƒg‚ğæ“¾
        //this.attackObject = transform.Find("Attack").gameObject;

        //Õ“Ë”»’èdake‚ğƒfƒBƒXƒG[ƒuƒ‹
        //this.attackObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    //QQQQQQQQQQQQQQQQQQQQQQQQ
    //ƒvƒŒƒCƒ„[
    //QQQQQQQQQQQQQQQQQQQQQQQ
    void Player()
    {
        //ˆÚ“®‚ª‰Â”\
        if (isRun)
        {
            //ˆÚ“®
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //ƒWƒƒƒ“ƒv
                if (Input.GetKey(KeyCode.Space) && isGround)
                {
                    //¶ˆÚ“®{ƒWƒƒƒ“ƒv
                    this.myRigidbody.velocity = new Vector2(-VELOCITY, JUMPPOWER);
                }
                else
                {
                    myAnimator.SetInteger("Run", -1);

                    //¶ˆÚ“®
                    this.myRigidbody.velocity = new Vector2(-VELOCITY, this.myRigidbody.velocity.y);
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                //ƒWƒƒƒ“ƒv
                if (Input.GetKey(KeyCode.Space) && isGround)
                {
                    //‰EˆÚ“®{ƒWƒƒƒ“ƒv
                    this.myRigidbody.velocity = new Vector2(VELOCITY, JUMPPOWER);
                }
                else
                {
                    myAnimator.SetInteger("Run", 1);

                    //‰EˆÚ“®
                    this.myRigidbody.velocity = new Vector2(VELOCITY, this.myRigidbody.velocity.y);
                }
            }
            else
            {
                myAnimator.SetInteger("Run", 0);

                //’â~
                this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);

                //ƒWƒƒƒ“ƒv
                if (Input.GetKey(KeyCode.Space) && isGround)
                {
                    //‚’¼ƒWƒƒƒ“ƒv
                    this.myRigidbody.velocity = new Vector2(0.0f, JUMPPOWER);
                }
            }
        }
        else
        {
            //’â~
            this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
        }

        //UŒ‚
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //“®‚¯‚È‚­‚·‚é
            isRun = false;

            myAnimator.SetTrigger("PreAttack");
        }


    }

    //----------------------------------------------------
    //ƒvƒŒƒCƒ„[iCOM)
    //----------------------------------------------------
    void PlayerComuter()
    {
        if (isRun)
        {
            //“®‚¯‚È‚­‚·‚é
            isRun = false;
            myAnimator.SetTrigger("PreAttack");
        }
    }

    // Update is called once per frame
    //QQQQQQQQQQQQQQQQQQQQQQQQQ
    //ƒAƒbƒvƒf[ƒg
    //QQQQQQQQQQQQQQQQQQQQQQQQ
    void Update()
    {
        if (assign == 1)
        {
            //ƒvƒŒƒCƒ„[
            Player();
        }
        else if (assign == -2)
        {
            //ƒvƒŒƒCƒ„[iCOM)
            PlayerComuter();
        }

        //ƒWƒƒƒ“ƒv‚Ì‘JˆÚ
        this.myAnimator.SetFloat("Jump", this.myRigidbody.velocity.y);
        this.myAnimator.SetBool("Ground", isGround);
    }

    public void PreAttackStart()
    {
        Debug.Log("PreAttackStart");
    }

    public void AttackStart()
    {
        //Õ“Ë‚ğƒIƒ“
        //this.attackObject.SetActive(true);
        //this.attackObject.GetComponent<BoxCollider2D>().enabled = true;
        //UŒ‚‚ğ¶¬
        attackObject = Instantiate(attackPrefab, this.transform.position + new Vector3(1.29f *transform.localScale.x ,1.44f,0f), Quaternion.identity);
        Debug.Log("AttackStart");
    }

    public void AttackEnd()
    {
        //Õ“Ë‚ğƒIƒt
        //this.attackObject.SetActive(false);
        //this.attackObject.GetComponent<BoxCollider2D>().enabled = false;
        //UŒ‚‚ğ”jŠü‚·‚é
        Destroy(attackObject.gameObject);
        isRun = true;
        Debug.Log("AttackEnd");      
    }

    /*
    private void FixedUpdate()
    {
        //’nã”»’è
        isGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        if(isGround || axisH != 0)
        {
            //’n–ÊãOR‘¬“x‚ª‚O‚Å‚Í‚È‚¢
            //‘¬“x‚ğXV‚·‚é
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
            /*
            if (assign == 1)
            {
                HPManager.HPLeft.GetComponent;

                //ƒvƒŒƒCƒ„[‚ªƒ_ƒ[ƒW‚ğó‚¯‚½
                
            }
            else if (assign == -2)
            {
                //ƒvƒŒƒCƒ„[iCOM)‚ªƒ_ƒ[ƒW‚ğó‚¯‚½

            }
            */
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