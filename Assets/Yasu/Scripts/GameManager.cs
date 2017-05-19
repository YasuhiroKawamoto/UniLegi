using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Range(1, 3)]
    private int stageNumber;

    [SerializeField]
    private DangerZone dangerZone;

    [SerializeField]
    private GameObject text_hp;

    [SerializeField]
    private GameObject text_cost;

    [SerializeField]
    private GameObject text_unit;

    int m_unitNum;

    private float m_gameSpeed;

    [SerializeField]
    private int m_hp;
    [SerializeField]
    private int m_cost;

    private float m_cnt;

    [SerializeField]
    int m_maxCost = 1000;

    // Use this for initialization
    void Start()
    {
        m_gameSpeed = 1;
        m_unitNum = 0;
        m_cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_hp =dangerZone.GetHp();


        // コストが最大値以下ならコスト回復
        m_cnt++;
        if (m_cnt > 10)
        {
            if (m_cost < m_maxCost)
            {
                m_cost += (int)Time.timeScale * 10;
            }
            m_cnt = 0;
        }

        // ユニット数を制御

        GameObject[] unions = GameObject.FindGameObjectsWithTag("Player");

        m_unitNum = 0;
        foreach (GameObject union in unions)
        {
            m_unitNum++;
        }

        // UIの更新
        text_hp.GetComponent<Text>().text = "HP:" + m_hp.ToString();

        text_cost.GetComponent<Text>().text = "COST:" + m_cost.ToString() + "/" + m_maxCost.ToString();

        text_unit.GetComponent<Text>().text = "UNIT:" + m_unitNum.ToString() + "/" + 5;

        // シーン遷移
        if (m_hp < 0)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
   



    public void SpendCost(int cost)
    {
        m_cost -= cost;
    }

    public int GetCost()
    {
        return m_cost;
    }

    public int GetMaxCost()
    {
        return m_maxCost;
    }

    public int GetHp()
    {
        return m_hp;
    }
    public int GetNum()
    {
        return m_unitNum;
    }

    public int GetStageNumber()
    {
        return stageNumber;
    }
    public void SetStageNumber(int stageNum)
    {
        stageNumber = stageNum;
    }

    public void RecoverCost(int cost)
    {
        m_cost += cost;
    }

    public float GetSpd()
    {
        return m_gameSpeed;
    }

    public void SetSpd(float spd)
    {
        m_gameSpeed = spd;
    }
}