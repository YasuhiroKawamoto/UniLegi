using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField,Range(1,20)]
    private int stageNumber;

    [SerializeField]
    private GameObject dangerZone;

    [SerializeField]
    private GameObject text_hp;

    [SerializeField]
    private GameObject text_cost;

    Component cmp;

    private int m_hp;
    private int m_cost;

    int m_maxCost = 1000;

    // Use this for initialization
    void Start ()
    {

        m_hp = 1000;
        m_cost = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
       if(dangerZone.GetComponent<DangerZone>().isHit)
        {
            m_hp--;
        }

       if(m_cost  < m_maxCost)
        {
            m_cost += 1;
        }
        else
        {
            Destroy(cmp);
        }

        text_hp.GetComponent<Text>().text = "HP:" + m_hp.ToString();

        text_cost.GetComponent<Text>().text = "COST:" + m_cost.ToString() + "/" + m_maxCost.ToString();

    }
}
