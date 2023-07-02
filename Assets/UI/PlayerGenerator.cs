using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class PlayerGenerator : MonoBehaviour
{

    //PlayerPrefabを入れる
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject Player1DeathBrowPrefab;
    private GameObject Player1;
    private GameObject Player2;



    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlayer1toPlayerDeathBrow();
        }
    }
    //アサイン1の入れ替え(数値の１を押下するとPlayer1→Player1DeathBrow）
    public void ChangePlayer1toPlayerDeathBrow()
    {
        //入れ替え直前のポジションを取得
        Vector3 position = Player1.transform.position;
        //ポジションが取得できたの破棄
        Destroy(Player1.gameObject, 0.0f);
        //新しいプレイヤーを生成
        Player1 = Instantiate(Player1DeathBrowPrefab);
        //positionで取得した位置を設定
        Player1.transform.position = position;
        //プレイヤーは使用していないが、COM用に相手を設定
        Player1.GetComponent<PlayerController>().otherPlayer = Player2;
        //クロスする形で登録する
        Player2.GetComponent<PlayerController>().otherPlayer = Player1;
        //キーボードで動かす
        Player1.GetComponent<PlayerController>().assign = 1;
    }

    //アサイン1を戻す
    //アサイン2の入れ替え
    //アサイン2を戻す
}
