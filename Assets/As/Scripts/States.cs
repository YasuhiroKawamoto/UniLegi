using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // HP0以下で消滅
        if (getHp() <= 0)
        {
            Destroy(gameObject);
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
    public int GetAbilityType()
    {
        return m_abilityType;
    }

    public int GetCoolTime()
    {
        return m_CoolTime;
    }

}



