using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontMove : MonoBehaviour {

    Vector3 Pos; 
	// Use this for initialization
	void Start () {
        Pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.position = Pos;

    }
}
