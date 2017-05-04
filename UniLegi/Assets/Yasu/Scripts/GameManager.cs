using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    private int stageNumber;

    [SerializeField]
    private GameObject dangerZone;

    [SerializeField]
    private GameObject text_hp;

    [SerializeField]
    private GameObject text_cost;

    private bool isPause;

    int unitNum;


    private int m_hp;
    private int m_cost;

    int m_maxCost = 1000;

    // Use this for initialization
    void Start()
    {
        unitNum = 0;
        m_hp = 1000;
        m_cost = 0;
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 敵が当たったらHP減少
        if (dangerZone.GetComponent<DangerZone>().isHit)
        {
            m_hp--;
        }

        // コストが最大値以下ならコスト回復
        if (m_cost < m_maxCost)
        {
            m_cost += 1;
        }

        // ユニット数を制御

        GameObject[] unions = GameObject.FindGameObjectsWithTag("Player");

        unitNum = 0;
        foreach (GameObject union in unions)
        {
            unitNum++;
        }

        // UIの更新
        text_hp.GetComponent<Text>().text = "HP:" + m_hp.ToString();

        text_cost.GetComponent<Text>().text = "COST:" + m_cost.ToString() + "/" + m_maxCost.ToString();


        // シーン遷移
        if (m_hp < 0)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}