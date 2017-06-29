using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSoon : MonoBehaviour {
    PlayerControl player;

	// Use this for initialization
	void Start () {
      

    }
	
	// Update is called once per frame
	void Update () {
        if(GetComponent<SelfDestroy>().GetLimitCnt() <= 3)
        {
            GetComponent<Flash>().enabled = true;
        }
        else
        {
            GetComponent<Flash>().enabled = false;

        }

    }
}
