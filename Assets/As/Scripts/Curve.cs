﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour {

    //拡大
    [SerializeField]
    private Vector3 m_scale;
    //始点
    private Vector3 m_startPos;
    //終点
    private Vector3 m_endPos;

    //最大拡大値
    [SerializeField]
    private float m_limitScale = 0;

    //差分
    [SerializeField]
    private Vector3 m_pos = new Vector3(5.0f, 0, 0);
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale += m_scale;

        if(transform.localScale.x > m_limitScale)
        {
            Destroy(gameObject);
        }
    }

    //スタート地点を設定する
    public void setStartPos(Vector3 startPos)
    {
        m_startPos = startPos;
    }
    //終着点を設定する
    public void setEndPos(Vector3 endPos)
    {
        m_endPos = endPos + m_pos;
    }

}
