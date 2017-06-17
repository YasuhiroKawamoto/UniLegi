using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStoper : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.transform.position = (transform.parent.position + new Vector3(0,transform.parent.localScale.y/3,0));
        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
