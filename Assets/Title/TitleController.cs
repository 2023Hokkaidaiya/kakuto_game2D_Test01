using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour
{
    static public int assign1Attack = 1;�@//UI�Œl��ύX������B
    static public int assign2Attack = 1;�@//UI�Œl��ύX������B
    public string sceneName; //�ǂݍ��ރV�[����
    public Slider sliderObject1; // sliderObject1�ϐ����`
    public Slider sliderObject2; // sliderObject2�ϐ����`


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�X�^�[�g�{�^���ŃV�[���J��
    public void StartButton()
    {
        //�U���͂�ݒ�
        assign1Attack = (int)sliderObject1.GetComponent<Slider>().value;
        assign2Attack = (int)sliderObject2.GetComponent<Slider>().value;
        //�V�[����ǂݍ���
        SceneManager.LoadScene(sceneName);
    }
}
