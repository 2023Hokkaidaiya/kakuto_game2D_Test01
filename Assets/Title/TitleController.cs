using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour
{
    static public int assign1Attack = 0;�@//UI�Œl��ύX������B
    static public int assign2Attack = 0;�@//UI�Œl��ύX������B
    public string sceneName; //�ǂݍ��ރV�[����

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
        //�V�[����ǂݍ���
        SceneManager.LoadScene(sceneName);
    }
}
