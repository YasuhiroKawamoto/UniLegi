using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDevil : MonoBehaviour {

    //タッチ座標
    private Vector2 m_worldPoint;
    //タッチ
    Touch touch;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        touch = Input.GetTouch(0);
        m_worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
        transform.position = m_worldPoint;

        if (touch.phase == TouchPhase.Ended)
        {
            Destroy(gameObject);
            
        }


    }

   
}
