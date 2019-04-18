using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//製作者(高橋)
public class ScoreController : MonoBehaviour
{    
    [SerializeField] private List<Scorenum> _numStorage2 = new List<Scorenum>();
    [SerializeField] private List<Scorenum> _numStorage3 = new List<Scorenum>();
    [SerializeField] private List<Scorenum> _numStorage4 = new List<Scorenum>();
    [SerializeField] private Image combo;

    private SongReproduction songReproduction;  //SongReproductionスクリプトをここに入れる           

    private int score = 0;  //スコアの初期値  
    private int Combo = 0;  //コンボの初期値  
    private int hp = 300;   //体力の初期値      

    private static int ResultScore;

    private void Start()
    {
        _numStorage4[0].Setnumber(hp / 100);   // scoreの4桁目の数字を配列に入れる
        _numStorage4[1].Setnumber((hp % 100) / 10); // scoreの3桁目の数字を配列に入れる
        _numStorage4[2].Setnumber((hp % 10));  // scoreの2桁目の数字を配列に入れる
        songReproduction = GameObject.Find("TapSound").GetComponent<SongReproduction>(); //スクリプトを読み込む                         
    }
    //scoreの値を増やす
    public void AddScore(int t)
    {
        score += t;
        int niseScore = score;
        ResultScore += t;
        int num;
        int cc = 1000;
        for (int i = 0; i < 4; i++)
        {
            num = niseScore / cc;
            _numStorage2[i].Setnumber(num);
            niseScore %= cc;
            cc /= 10;            
        }                
        
        if (t == 10)
        {
            //タップ成功時のSE
            songReproduction.GoodAudio();         
        }
        else if(t == 50)
        {
            //タップ成功時のSE
            songReproduction.PerfectAudio();
        }
    }
    public static int getScore()
    {
        return ResultScore;             
    }
    //コンボの値を増やす
    public void AddCombo()
    {
        Combo++;
        combo.gameObject.SetActive(true);
        _numStorage3[2].gameObject.SetActive(true);
        _numStorage3[2].Setnumber((Combo % 10));  // scoreの2桁目の数字を配列に入れる                   
        if (Combo > 9)
        {
            _numStorage3[1].gameObject.SetActive(true);
            _numStorage3[1].Setnumber((Combo % 100) / 10); // scoreの3桁目の数字を配列に入れる
        }
        if(Combo > 99)
        {
            _numStorage3[0].gameObject.SetActive(true);
            _numStorage3[0].Setnumber(Combo / 100);   // scoreの4桁目の数字を配列に入れる
        }
    }
    //ミス判定と体力の値を増やす
    public void MissJudgment()
    {        
        Combo = 0;
        combo.gameObject.SetActive(false);
        _numStorage3[0].gameObject.SetActive(false);
        _numStorage3[1].gameObject.SetActive(false);
        _numStorage3[2].gameObject.SetActive(false);

        hp -= 10;
        int niseScore = hp;
        int num = 0;
        int cc = 100;        
        for (int i = 0; i < 3; i++)
        {
            num = niseScore / cc;
            _numStorage4[i].Setnumber(num);
            niseScore %= cc;
            cc /= 10;
        }
        if (hp == 0)
        {
            SceneManager.LoadScene("Result");
        }
    }
}
