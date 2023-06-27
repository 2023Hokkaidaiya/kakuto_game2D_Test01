using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour
{
    static public int assign1Attack = 0;　//UIで値を変更させる。
    static public int assign2Attack = 0;　//UIで値を変更させる。
    public string sceneName; //読み込むシーン名

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //スタートボタンでシーン遷移
    public void StartButton()
    {
        //シーンを読み込む
        SceneManager.LoadScene(sceneName);
    }
}
