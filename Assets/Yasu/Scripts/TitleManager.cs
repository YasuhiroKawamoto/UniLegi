using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

    [SerializeField]
    GameObject hand1;

    [SerializeField]
    GameObject hand2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount>0)
        {
            if (Input.touchCount == 2)
            {
                Vector2 pos1 = hand1.transform.position;
                pos1.x = Input.touches[0].position.x;
                hand1.transform.position = Camera.main.ScreenToWorldPoint(pos1);

                Vector2 pos2 = hand2.transform.position;
                pos2.x = Input.touches[1].position.x;
                hand2.transform.position = Camera.main.ScreenToWorldPoint(pos2);
            }
        }
	}
}
