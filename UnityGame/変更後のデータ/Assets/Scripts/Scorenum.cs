using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//製作者(高橋)
public class Scorenum : MonoBehaviour {

    /// <summary>
    /// UIの数字を格納する
    /// </summary>
    [SerializeField]　private List<Sprite> _numStorage = new List<Sprite>();

    public void Setnumber(int num)
    {
        GetComponent<Image>().sprite = _numStorage[num];
    }
}
