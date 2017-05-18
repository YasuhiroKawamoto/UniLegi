using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour {

    //Tapコンポーネント
    Tap tap;
    //Statesコンポーネント
    States states;
    //Bulletのプレハブ
    public GameObject bullet;
    //Delay
    private float m_Delay;
    //カウント
    private int m_Cnt = 0;
    //反転
    private bool m_Flag;
    //弾薬
    [SerializeField]
    private int m_Ammo = 0;
    //薬莢
    private int m_Cartridge = 0;
    //リロードフラグ
    private bool m_Reload = false;
    //装填時間
    private float m_LoadTime = 0.0f;
    //装填終わり
    private int m_LoadFinish = 0;
    private int m_Attack;


    //Use this for initialization
   void Start () {

        //Statesコンポーネントの取得
        states = GetComponent<States>();
        m_Delay = states.getrate();
        m_LoadFinish = states.GetCoolTime();
        m_Attack = states.getAttack();

    }

    // Update is called once per frame
    void Update()
    {
       
        tap = GetComponent<Tap>();

        m_Cnt ++;

        m_Flag = tap.getInverd();

        Transform Children = bullet.GetComponentInChildren<Transform>();

        foreach (Transform ob in Children)
        {
            ob.GetComponentInChildren<Bullet>().setInverdFlag(m_Flag);
            ob.GetComponentInChildren<Bullet>().getAttack(m_Attack);
        }


        if (tap.getShot() == true && m_Reload == false)
        {
            if (m_Delay < m_Cnt)
            {
                if(Time.timeScale != 0)
                {

                //弾をプレイヤーと同じ位置に設定
                Instantiate(bullet, transform.position, transform.rotation);
                //弾を数える
                m_Cartridge++;
                //リセット
                m_Cnt = 0;
                }
            } 
        }

        //リロード
        if(m_Cartridge >= m_Ammo)
        {
            m_Reload = true;
        }

        //リロードに入ったらカウントを数える
        if(m_Reload == true)
        {
            m_LoadTime += Time.deltaTime;
        }
        //リロードが終わったらフラグをfalseにする
        if(m_LoadTime > m_LoadFinish)
        {
            m_Reload = false;
            m_Cartridge = 0;
            m_LoadTime = 0;
        }


    }

    public void SetBullet(GameObject bullet)
    {
        this.bullet = bullet;
    }
  
}
