using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour {

    //tapコンポーネント
    tap tap;

    //Statesコンポーネント
    States states;
    //Bulletのプレハブ
    public GameObject bullet;
    //Delay
    private int m_Delay;
    //カウント
    private int m_Cnt = 0;


    //Use this for initialization
   void Start () {

        //Statesコンポーネントの取得
        states = GetComponent<States>();
        m_Delay = states.getrate();

    }

    // Update is called once per frame
    void Update()
    {
        tap = GetComponent<tap>();

        m_Cnt ++;

        if (tap.getShot() == true)
        {
            if (m_Delay < m_Cnt)
            {
                //弾をプレイヤーと同じ位置に設定
                Instantiate(bullet, transform.position, transform.rotation);
                //リセット
                m_Cnt = 0;

            }
            
        }


    }

    

}
