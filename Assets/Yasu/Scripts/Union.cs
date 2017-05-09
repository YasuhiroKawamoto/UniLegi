using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Union : MonoBehaviour
{

    GameObject newUnit;

    // Use this for initialization
    void Start()
    {
        newUnit = GameObject.Find("Player").GetComponent<GetTouch>().newUnit;
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject[] unions = GameObject.FindGameObjectsWithTag("Player");
        States newState = newUnit.GetComponent<States>();

        List<int> typeIDs = new List<int>();

        foreach (GameObject union in unions)
        {
            States state = union.GetComponent<States>();
            typeIDs.Add(state.m_typeId);
        }

        newState.m_typeId = FindIndexMost(typeIDs);


        //newState.m_typeId = typeIDs.

        if (collision.gameObject.tag == "Collider")
        {
            this.gameObject.tag = "isPinched";
        }
    }

    int FindIndexMost(List<int> intList)
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
}