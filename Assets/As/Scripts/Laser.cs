using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {


    //消えるまでの時間
    [SerializeField]
    private float lifeTime;
    //測るタイマー
    private float m_time = 0.0f;
    private float m_range = 0.0f;
    private Vector3 vec;

    
	// Use this for initialization
	void Start () {

        m_time = 0.0f;
        vec = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

        m_time += Time.deltaTime;

        if(m_time < 1.0f)
        {
            vec.x += 0.1f;
            transform.localScale = vec;
        }
        else if(m_time > 3.0f && m_time< 4.0f)
        {
            vec.x -= 0.1f;
            transform.localScale = vec;
            if(vec.x<0.0f)
            {
                Destroy(gameObject);
            }
        }



	}
}
