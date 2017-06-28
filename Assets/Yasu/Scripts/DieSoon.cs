using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSoon : MonoBehaviour {
    PlayerControl player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();

    }
	
	// Update is called once per frame
	void Update () {
        if(player.GetUnionCoolTime() >= 80)
        {
            GetComponent<Flash>().enabled = true;
        }
        else
        {
            GetComponent<Flash>().enabled = false;

        }

    }
}
