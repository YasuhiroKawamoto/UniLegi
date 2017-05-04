﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

    [SerializeField]
    private float zone;

    //触ったフラグ
    private int m_flag;
    //タッチ
    Touch touch;
    //タッチ座標
    private Vector2 m_worldPoint;
    //生成するオブジェクト
    public GameObject AsPrefab;

    private float posx;
    private float posy;

    private Vector2 savePos;

    // Use this for initialization
    void Start () {

        m_flag = 0;
       

        savePos = (this.transform.position);

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            m_worldPoint = Camera.main.ScreenToWorldPoint(touch.position);

            //タッチ開始時
            if (touch.phase == TouchPhase.Began)
            {
                
                //タッチをした位置にオブジェクト判定
                RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                if (hit)
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        Debug.Log("さわった");
                        m_flag = 1;
                    }

                }

            }
            //離したとき
            else if (touch.phase == TouchPhase.Ended)
            {
                //タッチをした位置にオブジェクト判定
                RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                if (hit)
                {
                   
                    if (hit.collider.gameObject == this.gameObject)
                    {

                        if(m_worldPoint.y<zone)
                        {
                            //元いた位置に戻る
                            transform.position = savePos;

                            m_flag = 0;
                        }
                        else
                        {
                            AsPrefab.transform.position = m_worldPoint;

                            Instantiate(AsPrefab);

                            //元いた位置に戻る
                            transform.position = savePos;

                            m_flag = 0;
                        }
                       
                    }

                }
            }

        }
        else
        {
            //元いた位置に戻る
            transform.position = savePos;

            m_flag = 0;
        }     

        //1の時追従する
        if (m_flag==1)
        {
            //タッチしている座標に追従する
            transform.position = m_worldPoint;

        }

    }

}
