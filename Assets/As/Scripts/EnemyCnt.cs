using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCnt : MonoBehaviour {

    States states;

    private bool flag = false;

	// Use this for initialization
	void Start () {

        states = GetComponent<States>();

	}
	
	// Update is called once per frame
	void Update () {
	
        if(states.getDead())
        {
            if (flag == false)
            {
                Singleton<SceneData>.instance.setEnemyCnt(1);
                flag = true;
            }
        }
        	
	}
}
