using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInfinity : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //インフィニティモードなら
        if (Singleton<SceneData>.instance.getStageNumber() == 3)
        {

            GetComponent<Emitter>().enabled = false;
            GetComponent<random>().enabled = true;

        }
        else
        {
            GetComponent<Emitter>().enabled = true;
            GetComponent<random>().enabled = false;
        }
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
