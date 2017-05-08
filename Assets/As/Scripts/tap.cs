﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour {

    //弾が撃てる
    private bool m_canShot = true;
    //動いてるかどうか
    private bool m_moveFlag;

    //スケールの倍数
    const float rate = 3;
    //範囲
    Vector3 zone;

    //元の大きさを保存
    private Vector3 m_saveScale;

    //反転してるかどうか
    private bool m_Invert = false;

    //タッチ
    Touch touch;

    //タッチ座標
    private Vector2 m_worldPoint;

    private float m_Cnt;


    // Use this for initialization
    void Start()
    {

        zone = GameObject.Find("SpriteDengerZone").transform.position;

        //フラグは立たない
        m_moveFlag = false;
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
                    if (Input.touchCount == 1)
                    {

                        if (hit.collider.gameObject == this.gameObject)
                        {
                            Debug.Log("タッチ");
                            m_moveFlag = true;
                            this.gameObject.tag = "Untagged";
                            m_canShot = false;
                        }
                    }
                }

            }
            //離したとき
            else if (touch.phase == TouchPhase.Ended && m_moveFlag)
            {
                //タッチをした位置にオブジェクト判定
                RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                if (hit)
                {
                    if (hit.collider.gameObject == this.gameObject && hit.collider.gameObject.tag != "isPinched")
                    {
                        Debug.Log("離した");
                        this.gameObject.tag = "Player";
                        if (m_Cnt < 0.5f)
                        {
                            m_Invert = !m_Invert;

                            Debug.Log("反転");
                            transform.Rotate(new Vector3(180.0f, 0.0f, 0.0f));

                            m_Cnt = 0.0f;

                        }

                        this.transform.localScale = m_saveScale;
                        m_moveFlag = false;
                        m_canShot = true;

                        m_Cnt = 0.0f;

                    }

                }

            }

        }


        //オブジェクトが触っている時
        if (m_moveFlag == true)
        {
            if (m_worldPoint.y > zone.y + 0.2f)
            {
                //タッチしている座標に追従する
                transform.position = m_worldPoint;
                //オブジェクトを倍加させる
                this.transform.localScale = new Vector3(rate, rate, 1);
                //撃てない
                m_canShot = false;

                m_Cnt += 0.1f;

            }
            else
            {
                this.transform.localScale = m_saveScale;
                m_moveFlag = false;
                m_canShot = true;

                m_Cnt = 0.0f;
            }

        }

    }

    public bool getShot()
    {
        return m_canShot;
    }

    //動いてるかどうかのフラグを渡す
    public bool getMove()
    {
        return m_moveFlag;
    }
    //反転してるか同課のフラグ
    public bool getInverd()
    {
        return m_Invert;
    }

}