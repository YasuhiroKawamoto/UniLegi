using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{

    //攻撃値
    [SerializeField]
    private int m_Attack;
    //HP
    [SerializeField]
    private int m_Hp;
    //コスト
    [SerializeField]
    private int m_Cost;
    //反転状態
    private bool m_TrunOver = true;
    //攻撃範囲
    [SerializeField]
    private float m_Range;
    //攻撃間隔
    [SerializeField]
    private float m_FireRate = 5.0f;

    // 移動タイプ
    [SerializeField]
    private int moveType;

    //種族データ
    [SerializeField]
    private int m_typeId;

    //種族値
    [SerializeField]
    private int m_typePoint;

    // アビリティタイプ
    [SerializeField]
    private int m_abilityType;
    //クールタイプ
    [SerializeField]
    private int m_CoolTime = 0;
    //弾薬
    [SerializeField]
    private int m_ammo = 0;

    //ロックオンカーソル
    [SerializeField]
    GameObject LockOnCursor;
    GameObject Lock;

    [SerializeField]
    GameObject DeadAction;

    private float DeadCnt;
    //死んでいるか？
    private bool IsDead = false;

    //ねらわれているか
    private bool IsLockon = false;
    private bool IsLockonNow = false;
    private float m_currentCharge;

    [SerializeField]
    private GameManager manager;
   



    // Use this for initialization
    void Start()
    {
        DeadCnt = 0.5f;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DeadAction = (GameObject)Resources.Load("Prefabs/DeadA");
        LockOnCursor = (GameObject)Resources.Load("Prefabs/LockOnCursor");

    }

    // Update is called once per frame
    void Update()
    {

        // HP0以下で消滅
        if (getHp() <= 0)
        {
            if (DeadAction != null&&!IsDead)
            {
                IsDead = true;
                Debug.Log("死んだ");
                DeadAction.transform.position = this.gameObject.transform.position;
                // コスト回復
                manager.RecoverCost(getCost());


                //死亡時動作
                Instantiate<GameObject>(DeadAction);


            }
           
        }



        if (IsDead)
        {

            DeadCnt -= Time.deltaTime;

            if (DeadCnt < 0)
            {
                Destroy(this.gameObject);

            }

        }

        //lock-onされている場合
        if (IsLockon)
        {
            if (LockOnCursor != null && IsLockonNow == false)
            {
                IsLockonNow = true;

                Lock = Instantiate<GameObject>(LockOnCursor);

            }
            Lock.transform.position = this.gameObject.transform.position;
        }
        else
        {
            if (IsLockonNow)
            {
                Destroy(Lock);
                IsLockonNow = false;
            }
        }

    }

    //
    public float getrate()
    {
        return m_FireRate;
    }

    public int getHp()
    {
        return m_Hp;
    }


    public int getAttack()
    {
        return m_Attack;
    }


    public float getRenge()
    {
        return m_Range;
    }


    public int getCost()
    {
        return m_Cost;
    }


    public void setDamege(int Damege)
    {
        m_Hp -= Damege;
    }

    public int GetMoveType()
    {
        return moveType;
    }

    public void SetFireRate(float rate)
    {
        m_FireRate = rate;
    }

    public void SetMoveType(int type)
    {
        moveType = type;
    }
    public int GetTypeId()
    {
        return m_typeId;
    }
    public void SetTypeId(int id)
    {
        m_typeId = id;
    }
    public void SetHp(int hp)
    {
        m_Hp = hp;
    }
    public void SetAtk(int atk)
    {
        m_Attack = atk;
    }
    public int GetTypePoint()
    {
        return m_typePoint;
    }

    public void SetTypePoint(int typePoint)
    {
        m_typePoint = typePoint;
    }


    public int GetAbilityType()
    {
        return m_abilityType;
    }

    public int GetCoolTime()
    {
        return m_CoolTime;
    }

    public void SetCoolTime(int time)
    {
        m_CoolTime = time;
    }

    //弾薬所持数の私
    public int GetAmmo()
    {
        return m_ammo;
    }

    public void SetAmmo(int ammo)
    {
        m_ammo = ammo;
    }



    public float GetCharge()
    {
        return m_currentCharge;
    }

    public void SetCharge(float charge)
    {
        m_currentCharge = charge;
    }

    //ロックオン状態変更関数
    public void SetLockOn(bool f)
    {
        IsLockon = f;
    }

    //死んでいるかどうか？
    public bool getDead()
    {

        return IsDead;
    }


    void OnDestroy()
    {
        if (IsLockonNow)
        {
            Destroy(Lock);
            IsLockonNow = false;
        }

    }
}



