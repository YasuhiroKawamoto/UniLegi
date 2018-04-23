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
    //照準マークのプレハブ
    public GameObject aim;
    //Delay
    private float m_Delay;
    //カウント
    private int m_Cnt;
    //反転
    private bool m_Flag;
    //弾薬
    private int m_Ammo;
    //薬莢
    private int m_Cartridge;
    //リロードフラグ
    private bool m_Reload;
    //装填時間
    private float m_LoadTime;
    //装填終わり
    private int m_LoadFinish;
    private int m_Attack;
    //範囲
    private Vector3 m_aim =new Vector3(0.0f,0.5f,0.0f);
    //Aimフラグ
    private bool m_AimFlag;

    [SerializeField]
    static GameObject TmpObj;

    private void Awake()
    {
        TmpObj = GameObject.Find("TmpBullet");
        // 初期化
        m_LoadTime = 0.0f;
        m_LoadFinish = 0;
        m_AimFlag = false;
        m_Reload = false;
        m_Cartridge = 0;
        m_Ammo = 0;
        m_Cnt = 0;
    }

    //Use this for initialization
    void Start () {

        //Statesコンポーネントの取得
        states = GetComponent<States>();
        m_Delay = states.getrate();
        m_LoadFinish = states.GetCoolTime();
        m_Attack = states.getAttack();
        m_Ammo = states.GetAmmo();
    }

    // Update is called once per frame
    void Update()
    {

        tap = GetComponent<Tap>();

        m_Cnt++;

        m_Flag = tap.getInverd();

        if (m_Flag == false)
        {
            m_AimFlag = false;
        }
        else if (m_Flag == true)
        {
            m_AimFlag = true;
        }

        //照準マークを作成
        if (m_AimFlag == false && m_Flag == false)
        {
            Instantiate(aim, transform.position + m_aim, transform.rotation, TmpObj.transform);
            m_AimFlag = true;
        }
        else if (m_AimFlag == true && m_Flag == true)
        {
            Instantiate(aim, transform.position - m_aim, transform.rotation, TmpObj.transform);
            m_AimFlag = false;
        }


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
                if (Time.timeScale != 0)
                {

                    //弾をプレイヤーと同じ位置に設定
                    Instantiate(bullet, transform.position, transform.rotation, TmpObj.transform);
                    //弾を数える
                    m_Cartridge++;
                    //リセット
                    m_Cnt = 0;
                }
            }
        }

        //リロード
        if (m_Cartridge >= m_Ammo)
        {
            m_Reload = true;
        }

        //リロードに入ったらカウントを数える
        if (m_Reload == true)
        {
			if (gameObject.tag == "Player"||gameObject.tag == "SPlayer")
            {

                m_LoadTime += Time.deltaTime;
                states.SetCharge(m_LoadTime);
            }
        }

        //リロードが終わったらフラグをfalseにする
        if (m_LoadTime > m_LoadFinish)
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
    public GameObject GetBullet()
    {
        return bullet;
    }

    public bool GetFlag()
    {
        return m_Reload;
    }
  
}
