using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Union : MonoBehaviour
{

    GameObject newUnit;

    bool isattached = false;

    [SerializeField]
    GameObject BulletPattern1;
    [SerializeField]
    GameObject BulletPattern2;
    [SerializeField]
    GameObject BulletPattern3;
    [SerializeField]
    GameObject BulletPattern4;
    [SerializeField]
    GameObject BulletPattern5;

    GameObject newBullet;


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
        if (collision.gameObject.tag == "Collider")
        {

            this.gameObject.tag = "isPinched";

            GameObject[] unions = GameObject.FindGameObjectsWithTag("isPinched");
            States newState = newUnit.GetComponent<States>();


            // Idをset
            newState.SetTypeId(CalcId(unions));

            // Hpをset    
            newState.SetHp(CalcHp(unions));

            // Attakをset
            newState.SetAtk(CalcAtk(unions));

            // スプライトを設定
            newUnit.GetComponent<SpriteRenderer>().sprite = GetSpr();

            // 弾の設定
            SelectBullet();
            newUnit.GetComponent<Firing>().SetBullet(newBullet);

        }
    }

    private int CalcId(GameObject[] unions)
    {
        List<int> typeIDs = new List<int>();
        int returnID = 0;

        foreach (GameObject union in unions)
        {
            States state = union.GetComponent<States>();

            typeIDs.Add(state.GetTypeId());
        }

        // 最頻値が0のとき(最頻値が複数ある時)
        if(Mode(typeIDs) == 0)
        {
            returnID = Max(typeIDs) + 1;
        }
        else
        {
            returnID = Mode(typeIDs) + 1;
        }

        return returnID;
    }
    private Sprite GetSpr()
    { 
        Sprite sprite;
        if(newUnit.GetComponent<States>().GetTypeId() == 2)
        {
            sprite = Resources.Load<Sprite>("GD_Slime(Red)");
            return sprite;
        }

        if (newUnit.GetComponent<States>().GetTypeId() == 4)
        {
            sprite = Resources.Load<Sprite>("GD_Kerberos(Purple)");
            return sprite;
        }

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

        int sum = 0;

        foreach (GameObject union in unions)
        {
            States state = union.GetComponent<States>();

            atks.Add(state.getAttack());
        }
        foreach (int atk in atks)
        {
            sum += atk;
        }

        // hpリターン
        return sum;
    }

    void SelectBullet()
    {
        int typeId = newUnit.GetComponent<States>().GetTypeId();
        if (typeId == 1)
        {
            newBullet = newUnit.GetComponent<Union>().BulletPattern1;
        }
        if(typeId == 2)
        {
            newBullet = newUnit.GetComponent<Union>().BulletPattern1;
            newUnit.GetComponent<States>().SetFireRate(2);
        }

        if (typeId == 3)
        {
            newBullet = newUnit.GetComponent<Union>().BulletPattern2;
        }
        if (typeId == 4)
        {
            newBullet = newUnit.GetComponent<Union>().BulletPattern3;
            newUnit.GetComponent<States>().SetFireRate(7);


        }
        //if ()
        //{
        //    bulletPattern = BulletPattern4;
        //}
        //if ()
        //{
        //    bulletPattern = BulletPattern5;
        //}
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

    int Max(List<int> intList)
    {
        int max = 0;
        for (int i = 0; i < intList.Count; i++)
        {
            if (intList[i] > max)
            {
                max = intList[i];
            }
        }

        return max; 
    }

    public GameObject GetNewUnit()
    {
        return newUnit;
    } 
}