using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//製作者(高橋)
public class Effect : MonoBehaviour {
	
    /// <summary>
    /// タップ成功時にでるSEを消す
    /// </summary>
	void Start () {
        StartCoroutine(DestroyEffect());
	}

    IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
