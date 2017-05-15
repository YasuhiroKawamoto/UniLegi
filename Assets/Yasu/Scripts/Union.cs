using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Union : MonoBehaviour
{

    GameObject newUnit;

    bool isattached = false;

    // Use this for initialization
    void Start()
    {
        newUnit = GameObject.Find("Player").GetComponent<GetTouch>().newUnit;
    
    }

    // Update is called once per frame
    void Update()
    {
        //newUnit = GameObject.Find("isPinched").GetComponent<GetTouch>().newUnit;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject[] unions = GameObject.FindGameObjectsWithTag("isPinched");
        States newState = newUnit.GetComponent<States>();
        Sprite sprite = newUnit.GetComponent<SpriteRenderer>().sprite;


        // Idをset
        newState.SetTypeId(CalcId(unions));

        // Hpをset    
        newState.SetHp(10);

        // Attakをset
        newState.SetAtk(CalcAtk(unions));

        //List<int> typeIDs = new List<int>();

        // スプライトを設定
       // newUnit.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("GD_Slime(Red)");

        if (collision.gameObject.tag == "Collider")
        {
            this.gameObject.tag = "isPinched";
        }
    }

    private int CalcId(GameObject[] unions)
    {
        List<int> typeIDs = new List<int>();


        foreach (GameObject union in unions)
        {
            States state = union.GetComponent<States>();

            typeIDs.Add(state.GetTypeId());
        }

        // 最頻値をIDとしてリターン
        return Mode(typeIDs);
    }
    private Sprite GetSpr(GameObject[] unions)
    {

        Sprite sprite;
        if(newUnit.GetComponent<States>().GetTypeId() == 1)
        {
            sprite = Resources.Load<Sprite>("GD_Slime(Red)");
            return sprite;
        }

        if (newUnit.GetComponent<States>().GetTypeId() == 2)
        {
            sprite = Resources.Load<Sprite>("GD_Kerberos(Purple)");
            return sprite;
        }
        //foreach (GameObject union in unions)
        //{
        //    if(union.GetComponent<States>().GetTypeId() == newUnit.GetComponent<States>().GetTypeId())
        //    {
        //        sprite = union.GetComponent<SpriteRenderer>().sprite;
        //        return sprite;
        //    }
        //}
        return null;
    }
    private int CalcHp(GameObject[] unions)
    {
        List<int> hps = new List<int>();


        //newState.m_typeId = typeIDs.

        int sum = 0;

        foreach (GameObject union in unions)
        {
            States state = union.GetComponent<States>();


            hps.Add(state.getHp());
        }
        foreach (int hp in hps)
        {
            sum += hp;
        }

        // hpリターン
        return sum;
    }


    private int CalcAtk(GameObject[] unions)
    {
        List<int> atks = new List<int>();

        int atk = 0;

        foreach (GameObject union in unions)
        {
            States state = union.GetComponent<States>();

            atk += state.getAttack();
        }

        // 最頻値をIDとしてリターン
        return atk;
    }
    int Mode(List<int> intList)
    {
        int id = 0;
        int tempCnt = 0;
        int cnt = 0;

        for (int i = 0; i < intList.Count; i++)
        {
            for (int j = i + 1; j < intList.Count; j++)
            {
                if (intList[i] == intList[j])
                {
                    tempCnt++;
                }
            }
            if (tempCnt > cnt)
            {
                cnt = tempCnt;
                id = intList[i];
            }
        }
        return id;
    }

    public GameObject GetNewUnit()
    {
        return newUnit;
    } 
}