using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Black : MonoBehaviour {

    [SerializeField]
    float color = 0.0f;

    bool m_flag = true;

    SpriteRenderer black;

    GameObject player;

    PlayerControl playerControl;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");

        playerControl = player.GetComponent<PlayerControl>();

        black = GetComponent<SpriteRenderer>();

        black.color = new Color(1.0f, 1.0f, 1.0f, color);

    }
	
	// Update is called once per frame
	void Update () {

        m_flag = playerControl.getFlag();

        if (m_flag == true)
        {
            if (color >= 0.0f)
            {
                color -= Time.deltaTime;
            }
        }
        else if(m_flag==false)
        {
            if(color<1.0f)
            {
                color += Time.deltaTime;
            }
        }

        black.color = new Color(1.0f, 1.0f, 1.0f, color);

	}

 

}
