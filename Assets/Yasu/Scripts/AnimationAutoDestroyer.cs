using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestroyer : MonoBehaviour {

    Animator cmp;

	// Use this for initialization
	void Start () {
        cmp = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
        if(cmp.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            Destroy(this.gameObject);
        }
	}
}
