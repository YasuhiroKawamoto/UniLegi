﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

    int stage;
	// Use this for initialization
	void Start () {

        stage = Singleton<SceneData>.instance.getStageNumber();

        switch (stage)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Back1");
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Back2");
                break;
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}