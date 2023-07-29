using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;


public class PlayerGenerator : MonoBehaviour
{

    //PlayerPrefabを入れる
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject Player1DeathBrowPrefab;
    public GameObject Player1ImpossiblePrefab;
    public GameObject Player2ImpossiblePrefab;
    
    private GameObject Player1;
    private GameObject Player2;
    

    //一枚絵Prefabを入れる
    public GameObject CloseContestPrefab;
    private GameObject CloseContest;
    //HPmanager
    private GameObject hpManager;


    // Start is called before the first frame update
    void Start()
    {
        //HPマネージャオブジェクト
        hpManager = GameObject.Find("HPManager");
        //Player1と2を所定の位置に生成する
        Player1 = Instantiate(Player1Prefab);
        Player2 = Instantiate(Player2Prefab);

        Player1.transform.position = new Vector2(-6f, -3.21048f);
        Player2.transform.position = new Vector2(6f, -3.21048f);
        //OtherPlayerへクロスさせて登録する。（追加）
        Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //assignの再設定も可能 ※左:1 右:2 / COM 左：-1 右：-2
        Player1.GetComponent<PlayerController>().assign = 1;
        Player2.GetComponent<PlayerController>().assign = -2;
        //敵のガード率
        Player2.GetComponent <PlayerController>().guardRate = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //1で必殺技に変える
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlayer1toPlayerDeathBrow();
        }
        //0で一枚絵に変える（デバッグ用でありのちに更新すること）
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CheckinCloseContest();
        }
        //9で一枚絵から戻す（デバッグ用でありのちに更新すること）
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CheckoutCloseContest();
        }
        //左のHPが0以下ならImpossibleと入れ替えるメソッドを使う
        if (hpManager.GetComponent<HPManager>().HPLeft < 1)
        {
            ChangePlayer1toImpossible();
        }
    }





    //アサイン1の入れ替え(数値の１を押下するとPlayer1→Player1DeathBrow）
    public void ChangePlayer1toPlayerDeathBrow()
    {
        //入れ替え直前のポジションを取得(Leftを入れ変える)
        Vector3 positionLeft = Player1.transform.position;
        //ポジションが取得できたの破棄
        Destroy(Player1.gameObject, 0.0f);
        //新しいプレイヤーを生成
        Player1 = Instantiate(Player1DeathBrowPrefab);
        //positionで取得した位置を設定
        Player1.transform.position = positionLeft;
        //プレイヤーは使用していないが、COM用に相手を設定
        Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        //クロスする形で登録する
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //キーボードで動かす
        Player1.GetComponent<PlayerController>().assign = 1;
    }

    //アサイン1とアサイン２を一枚絵に入れ替える(数値の２を押下するとPlayer1,2→クロースコンテスト）
    public void CheckinCloseContest()
    {
        //入れ替え直前のポジションを取得(LとR両方取得)
        Vector3 positionLeft = Player1.transform.position;
        Vector3 positionRight = Player2.transform.position;
        Vector3 positionMiddle = (positionLeft + positionRight) / 2f;
        //ポジションが取得できたの破棄
        Destroy(Player1.gameObject, 0.0f);
        Destroy(Player2.gameObject, 0.0f);
        //つばぜり合いを生成
        Instantiate(CloseContestPrefab, positionMiddle, Quaternion.identity);
    }
    public void CheckoutCloseContest()
    {
        //入れ替え直前のポジションを取得
        Vector3 positionMiddle = CloseContestPrefab(Clone).transform.position;
    }
    //アサイン1を戻す
    //アサイン2の入れ替え
    //アサイン2を戻す

    //HP0になったらImpossibleに入れ替える
    public void ChangePlayer1toImpossible()
    {
        //入れ替え直前のポジションを取得
        Vector3 positionLeft = Player1.transform.position;
        //ポジションが取得できたの破棄
        Destroy(Player1.gameObject, 0.0f);
        //新しいimposibleを生成
        Player1 = Instantiate(Player1ImpossiblePrefab);
        //positionで取得した位置を設定
        Player1.transform.position = positionLeft;
        //プレイヤーは使用していないが、COM用に相手を設定
        //Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        //クロスする形で登録する
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //キーボードで動かす
        //Player1.GetComponent<PlayerController>().assign = 1;
    }
}
