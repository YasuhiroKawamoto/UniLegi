using System.Collections;
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
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("map1");
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("map2");
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("map3");
                break;
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
