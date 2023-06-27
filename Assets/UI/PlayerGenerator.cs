using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{

    //PlayerPrefabを入れる
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    // Start is called before the first frame update
    void Start()
    {
        //Player1と2を所定の位置に生成する
        GameObject Player1 = Instantiate(Player1Prefab);
        GameObject Player2 = Instantiate(Player2Prefab);
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
        
    }
}
