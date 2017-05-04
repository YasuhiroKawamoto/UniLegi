﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tap : MonoBehaviour {

    //弾が撃てる
    private bool m_canShot=true;

    private int m_flag;

    //スケールの倍数
    const float rate = 3;
    //範囲
    const int zone=-3;

    //元の大きさを保存
    private Vector3 m_saveScale;

  

    //反転してるかどうか
    private bool m_Invert = true;

    //タッチ
    Touch touch;

    //タッチ座標
    private　Vector2 m_worldPoint;

    //tap判定
    //[SerializeField]
    //private float=;

    //
    private float m_Cnt;


    // Use this for initialization
    void Start () {

        //フラグは立たない
        m_flag = 0;
        //大きさを保存
        m_saveScale = this.transform.localScale;

	}

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {

            touch = Input.GetTouch(0);

            m_worldPoint = Camera.main.ScreenToWorldPoint(touch.position);

            //タッチ開始時
            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("お");

                //タッチをした位置にオブジェクト判定
                RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                if (hit)
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        Debug.Log("タッチ");
                        m_flag = 1;
                        m_canShot = false;
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
                        Debug.Log("離した");

                        if(m_Cnt < 0.5f)
                        {
                            Debug.Log("反転");
                            transform.Rotate(new Vector3(180.0f, 0.0f, 0.0f));

                            m_Cnt = 0.0f;

                        }

                        this.transform.localScale = m_saveScale;
                        m_flag = 0;
                        m_canShot = true;

                        m_Cnt = 0.0f;

                    }

                }

            }

        }

        Debug.Log(m_Cnt);

        //オブジェクトが触っている時
        if (m_flag == 1)
        {
            if (m_worldPoint.y > zone)
            {
                //タッチしている座標に追従する
                transform.position = m_worldPoint;
                //オブジェクトを倍加させる
                this.transform.localScale = new Vector3(rate, rate,1);
                //撃てない
                m_canShot = false;

                m_Cnt+=0.1f;

            }

        }

    }

    public bool getShot()
    {
        return m_canShot;
    }

    
}
