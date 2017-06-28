using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GageFlash : MonoBehaviour {

    PlayerControl player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();

    }
	
	// Update is called once per frame
	void Update () {
        if((player.GetUnionCoolTime() <= 0 && player.GetOverload() == 0) || player.GetOverload() >= player.GetOverMAX())
        {
            gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
