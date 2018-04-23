using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDevil : MonoBehaviour {

    //タッチ座標
    private Vector2 m_worldPoint;
    //タッチ
    TouchInfo touch;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        touch = AppUtil.GetTouch();
        m_worldPoint = Camera.main.ScreenToWorldPoint(AppUtil.GetTouchPosition());
        transform.position = m_worldPoint;

        if (touch == TouchInfo.Ended)
        {
            Destroy(gameObject);
            
        }


    }

   
}
