using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single : MonoBehaviour {

    //弾のスピード
    public float speed=5.0f;

	// Use this for initialization
	void Start () {

        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(this.transform.position.y>5)
        {
            Destroy(this.gameObject);
        }

	}
}
