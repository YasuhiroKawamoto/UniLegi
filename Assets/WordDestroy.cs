using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordDestroy : MonoBehaviour {

    private float cnt = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        cnt = Time.deltaTime;
        if(cnt>1.0f)
        {
            Destroy(gameObject);
        }

	}
}
