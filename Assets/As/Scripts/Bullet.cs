﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

   
    //ダメージ量
    [SerializeField]
    private int m_BulletDamege = 1;
    //反転しているかのフラグ　最初は反転していないのでfalse
    [SerializeField]
    private bool m_flag=false;
    //弾のスピード
    [SerializeField]
    private float speed = 5.0f;
    //弾の発射角度
    [SerializeField]
    private float m_dir = 90;

    //貫通弾かどうか
    [SerializeField]
    private bool m_pierce=false;


    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        Vector2 v;
        //反転などしていた場合、弾の向きを変更
        //反転していないとき
        if (m_flag == false)
        {
            //sin cosの計算をし角度を求める
            v.x = Mathf.Cos(Mathf.Deg2Rad * m_dir) * speed;
            v.y = Mathf.Sin(Mathf.Deg2Rad * m_dir) * speed;
            GetComponent<Rigidbody2D>().velocity = v;
        }
        //反転しているとき
        else if (m_flag == true)
        {
            //sin cosの計算をし角度を求める
            v.x = Mathf.Cos(Mathf.Deg2Rad * m_dir) * speed;
            v.y = Mathf.Sin(Mathf.Deg2Rad * m_dir) * speed;
            GetComponent<Rigidbody2D>().velocity = -v;
        }

    }

    //弾のダメージ量を渡す
    public int getBulletDamage()
    {
        return m_BulletDamege;
    }
    //Firingから反転フラグをセットしてもらう
    public void setInverdFlag(bool flag)
    {
        m_flag = flag;
    }
    //反転しているかどうかのフラグを渡す
    public bool getInverdFlag()
    {
        return m_flag;
    }
    public void getAttack(int attack)
    {
        m_BulletDamege = attack;
    }
    public void setDir(float dir)
    {
        m_dir = dir;
    }
    //貫通するかどうかのフラグ
    public bool getFlag()
    {
        return m_pierce;
    }


}
