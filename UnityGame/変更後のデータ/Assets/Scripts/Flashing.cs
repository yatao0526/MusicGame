using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//製作者(高橋)
public class Flashing : MonoBehaviour
{
    /// <summary>
    /// Press to B buttonを点滅させる
    /// </summary>
    [SerializeField]
    Image fadeImage;    

    float fadeSpeed = 1.0f;
    float time;
    
    void Update()
    {
        fadeImage.color = GetAlphaColor(fadeImage.color);
    } 

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * fadeSpeed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;
        return color;
    }       
}
