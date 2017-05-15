using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {


    //消えるまでの時間
    [SerializeField]
    private float lifeTime;
    //測るタイマー
    private float m_time = 0.0f;


	// Use this for initialization
	void Start () {

        m_time = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {

        m_time += Time.deltaTime;

        //消える時間になったら
        if(m_time>lifeTime)
        {
            Destroy(gameObject);
        }


	}
}
