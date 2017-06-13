using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour {

    float delta;
	// Use this for initialization
	void Start () {
        delta = 0;

    }
	
	// Update is called once per frame
	void Update () {

        delta += 0.75f;
		if(gameObject.tag == "isPinched")
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Sin(delta) + 1);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,1);
        }
	}
}
