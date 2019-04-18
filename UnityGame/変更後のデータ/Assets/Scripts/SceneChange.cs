using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void Update ()
    {              
        //タイトルから難易度選択
        if (SceneManager.GetActiveScene().name == "Title" && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            SceneManager.LoadScene("GameStart");
        }
        //タイトルから操作説明へ
        if (SceneManager.GetActiveScene().name == "Title" && Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            SceneManager.LoadScene("OperationDescription");
        }
        //操作説明からタイトル
        if (SceneManager.GetActiveScene().name == "OperationDescription" && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            SceneManager.LoadScene("Title");
        }
        //Resultからexeファイルの終了
        if (SceneManager.GetActiveScene().name == "Result" && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Application.Quit();           
        }
    }
}