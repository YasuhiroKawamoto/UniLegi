using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation : MonoBehaviour {

    //カウント
    [SerializeField]
    private float m_cnt;
    //切り替えフラグ
    private bool m_flag = true;
    //保存
    [SerializeField]
    private float m_save;
    private float m_save2;
    //角度　
    [SerializeField]
    private float m_angle;

    private float m_time = 0.0f;

    private Vector2 m_vec=new Vector2(0,0);


	// Use this for initializatio
	void Start () {

        m_vec = new Vector2(0, 0);

	}
	
	// Update is called once per frame
	void Update () {

        
            
       

    }
}
