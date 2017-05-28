using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Union : MonoBehaviour
{

    GameObject newUnit;

    bool isattached = false;


    GameObject newBullet;

    int dragonFlagBit;
    const int DRAGON = 7;        // 0b0111


    // Use this for initialization
    void Start()
    {
        dragonFlagBit = 0;
        newUnit = GameObject.Find("Player").GetComponent<PlayerControl>().newUnit;
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

        dragonFlagBit = 0;
        foreach (GameObject union in unions)
        {
            States state = union.GetComponent<States>();

            dragonFlagBit = dragonFlagBit | state.GetTypePoint();

            typeIDs.Add(state.GetTypeId());
        }


        // ドラゴン判定成功
        if((dragonFlagBit & DRAGON) == DRAGON)
        {
            returnID = 7;
        }
        // 最頻値が0のとき(最頻値が複数ある時)
       else if (Mode(typeIDs) == 0)
        {
            // 異種混合合体→IDが最大値
            returnID = Max(typeIDs);
        }

        // 最頻値が確定された
        else
        {
            if (Mode(typeIDs) % 2 == 0)
            {
                // 合体後同士合体後のまま
                returnID = Mode(typeIDs);
            }
            else
            {
                // 合体前→合体させる
                returnID = Mode(typeIDs) + 1;
            }

        }



        return returnID;
    }
    private Sprite GetSpr()
    {
        string[] unionNames = { "GD_Slime(Green)", "GD_Slime(Red)", "GD_Kerberos(White)", "GD_Kerberos(Purple)", "GD_Golem", "GD_Golem(Ice)", "GD_Bahamut(Silver)" };

        Sprite sprite;

        sprite = Resources.Load<Sprite>(unionNames[newUnit.GetComponent<States>().GetTypeId() - 1]);
        return sprite;

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
        // 弾データの設定  
        int typeId = newUnit.GetComponent<States>().GetTypeId();

        // 緑スライム
        if (typeId == 1)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        }

        //赤スライム
        if(typeId == 2)
        {
            newBullet =Resources.Load<GameObject>("Prefabs/PlayerBullet");
            newUnit.GetComponent<States>().SetFireRate(4);
            newUnit.GetComponent<States>().SetCoolTime(3);
            newUnit.GetComponent<States>().SetAmmo(5);
            newUnit.GetComponent<States>().SetTypePoint(1);

        }

        // 白ケル
        if (typeId == 3)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet2");

        }

        // 紫ケル
        if (typeId == 4)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet3");
            newUnit.GetComponent<States>().SetFireRate(7);
            newUnit.GetComponent<States>().SetTypePoint(2);

        }

        // ゴーレム
        if (typeId == 5)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet7");
            newUnit.GetComponent<States>().SetFireRate(7);
        }

        // 氷ゴーレム
        if (typeId == 6)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet8");
            newUnit.GetComponent<States>().SetFireRate(15);
            newUnit.GetComponent<States>().SetCoolTime(1);
            newUnit.GetComponent<States>().SetAmmo(3);
            newUnit.GetComponent<States>().SetTypePoint(4);
        }

        //ドラゴン
        if (typeId == 7)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet6");
            newUnit.GetComponent<States>().SetFireRate(1);
            newUnit.GetComponent<States>().SetCoolTime(5);
            newUnit.GetComponent<States>().SetAmmo(15);
            newUnit.GetComponent<States>().SetTypePoint(7);

            newUnit.AddComponent<Angle>();
            Angle cmp = newUnit.GetComponent<Angle>();
            cmp.SetDir(30.0f);
            cmp.SetAng(150.0f);
            cmp.SetSpd(3.0f);




        }
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