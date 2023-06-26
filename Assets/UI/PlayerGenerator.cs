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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
