using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

    //フィニッシュ
    [SerializeField]
    private float m_cnt = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(m_cnt>Time.deltaTime)
        {
            Destroy(this.gameObject);
        }

	}
}
