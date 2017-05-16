using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour {

    //Tapコンポーネント
    Tap tap;

    //Shot shot;
    
    //Statesコンポーネント
    States states;
    //Bulletのプレハブ
    [SerializeField]
    private GameObject bullet;
    //Delay
    private float m_Delay;
    //カウント
    private int m_Cnt = 0;

    int hoge;

    //反転
    private bool m_Flag;


    //Use this for initialization
   void Start () {

        //Statesコンポーネントの取得
        states = GetComponent<States>();
        m_Delay = states.getrate();

    }

    // Update is called once per frame
    void Update()
    {
       

        tap = GetComponent<Tap>();

        m_Cnt ++;

        m_Flag = tap.getInverd();

        Transform Children = bullet.GetComponentInChildren<Transform>();

        //hoge = 0;
        foreach (Transform ob in Children)
        {
            ob.GetComponentInChildren<Bullet>().setInverdFlag(m_Flag);
            //hoge++;
            //Debug.Log("hoge" + hoge);
        }



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

  
    public void SetBullet(GameObject _bullet)
    {
        bullet = _bullet;
    }
}
