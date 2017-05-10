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
    private int m_FireRate = 5;

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
    public int getrate()
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
    public int GetTypeId()
    {
        return m_typeId;
    }
    public int GetTypePoint()
    {
        return m_typePoint;
    }
    public int GetAbilityType()
    {
        return m_abilityType;
    }
}



