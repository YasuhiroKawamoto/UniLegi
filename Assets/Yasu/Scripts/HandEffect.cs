using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEffect : MonoBehaviour {

    PlayerControl player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
		
	}

    // Update is called once per frame
    void Update () {
        if( player.GetOverload() >= player.GetOverMAX())
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

        }

    }
}
