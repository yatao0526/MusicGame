using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//制作者(高橋)
public class SongReproduction : MonoBehaviour {
            
    /// <summary>
    /// 使いたいSEを格納する
    /// </summary>
    [SerializeField] private AudioClip Perfect;
    [SerializeField] private AudioClip Good;
    [SerializeField] private AudioClip Choice;

    //パーフェクト判定時のSE
    public void PerfectAudio()
    {        
        GetComponent<AudioSource>().PlayOneShot(Perfect);
    }

    //グッド判定時のSE
    public void GoodAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(Good);
    }

    //難易度選択時のSE
    public void ChoiceAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(Choice);
    }
}
