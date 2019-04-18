using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//製作者(高橋)
public class Result : MonoBehaviour
{
    [SerializeField] private List<Scorenum> _numStorage2 = new List<Scorenum>();
   private ScoreController scoreController;    

    void Start ()
    {        
        int _resultScore = ScoreController.getScore();
        Debug.Log(_resultScore);
        int num = 0;
        int cc = 1000;
        for (int i = 0; i < 4; i++)
        {
            num = _resultScore / cc;
            _numStorage2[i].Setnumber(num);
            _resultScore %= cc;
            cc /= 10;
        }       
    }	
}
