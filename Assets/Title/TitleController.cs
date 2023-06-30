using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour
{
    static public int assign1Attack = 1;　//UIで値を変更させる。
    static public int assign2Attack = 1;　//UIで値を変更させる。
    public string sceneName; //読み込むシーン名
    public Slider sliderObject1; // sliderObject1変数を定義
    public Slider sliderObject2; // sliderObject2変数を定義


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
        //攻撃力を設定
        assign1Attack = (int)sliderObject1.GetComponent<Slider>().value;
        assign2Attack = (int)sliderObject2.GetComponent<Slider>().value;
        //シーンを読み込む
        SceneManager.LoadScene(sceneName);
    }
}
