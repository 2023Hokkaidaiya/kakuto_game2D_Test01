using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{

    //PlayerPrefab‚ğ“ü‚ê‚é
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    // Start is called before the first frame update
    void Start()
    {
        //Player1‚Æ2‚ğŠ’è‚ÌˆÊ’u‚É¶¬‚·‚é
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
