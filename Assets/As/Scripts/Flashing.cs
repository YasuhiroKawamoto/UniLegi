using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour {

    static float delta;
    PlayerControl player;
    // Use this for initialization
    void Start()
    {
        delta = 0;
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
    }
	
	// Update is called once per frame
	void Update () {

        if (player.GetPinchNum() != 0)
            delta += Time.deltaTime * 5.0f / player.GetPinchNum();

        if (gameObject.tag == "isPinched")
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Sin(delta) + 1);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,1);
        }
	}

}
