using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    //限界活動時間
    [SerializeField]
    private float m_limitTime = 0;
    //
    private float m_cnt = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        m_cnt += 1*0.01f;
		
        if(m_limitTime < m_cnt)
        {
            Debug.Log("破壊");
            Destroy(this.gameObject);
        }

	}
}
