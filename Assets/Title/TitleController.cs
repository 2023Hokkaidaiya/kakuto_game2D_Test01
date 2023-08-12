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

    static public int  guardRate;

    //CloseContest用の変数================================================
    //5050の時のAS2の攻撃頻度　高くすることでよりアグレッシブになる
    static public int aggression5050AS2;
    //Player1優勢の際のguard率（低くすることで攻撃が通りやすくなる→攻撃能力が向上）
    static public int guardRateAS2;
    //====================================================================

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
