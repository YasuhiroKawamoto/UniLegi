using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

  
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        // 子オブジェクトがなければ消滅
        if (gameObject.transform.childCount == 0)
        {
            Destroy(gameObject);
        }

    }


}
