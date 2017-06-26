using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Union : MonoBehaviour
{

    GameObject newUnit;
    static public Sprite tmpSprite;

    bool isattached = false;


    GameObject newBullet;

    int UnitFlagBit = 0;        // 0b0000
    const int Evolved = 8;      // 0b1000

    const int GOHST = 1;        // 0b0001
    const int KERBEROS = 2;     // 0b0010
    const int SALAMANDER = 3;   // 0b0011
    const int GOLEM = 4;        // 0b0100
    const int SUCCUBUS = 5;     // 0b0101
    const int HARPY = 6;        // 0b0110
    const int DUMMY = 7;        // 0b0111

    const int GOHST2 = 9;       // 0b1001
    const int KERBEROS2 = 10;   // 0b1010
    const int SALAMANDER2 = 11; // 0b1011
    const int GOLEM2 = 12;      // 0b1100
    const int SUCCUBUS2 = 13;   // 0b1101
    const int HARPY2 = 14;      // 0b1110
    const int DUMMY2 = 15;        // 0b1111






    // Use this for initialization
    void Start()
    {
        UnitFlagBit = 0;
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
            tmpSprite = newUnit.GetComponent<SpriteRenderer>().sprite = GetSpr();

            // 弾の設定
            SelectBullet();
            newUnit.GetComponent<Firing>().SetBullet(newBullet);

        }
    }

    private int CalcId(GameObject[] unions)
    {
        List<int> typeIDs = new List<int>();
        int returnID = 0;

        UnitFlagBit = 0;
        foreach (GameObject union in unions)
        {
            States state = union.GetComponent<States>();

            UnitFlagBit = UnitFlagBit | state.GetTypePoint();

            typeIDs.Add(state.GetTypeId());
        }


        // 進化後であるかどうか
        if (((UnitFlagBit & Evolved) & Evolved) == 1)
        {
            // 進化後だった時の処理

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
            Angle cmp = newUnit.GetComponent<Angle>();
            cmp.enabled = false;
        }

        //赤スライム
        if(typeId == 2)
        {
            newBullet =Resources.Load<GameObject>("Prefabs/PlayerBullet");
            newUnit.GetComponent<States>().SetFireRate(4);
            newUnit.GetComponent<States>().SetCoolTime(3);
            newUnit.GetComponent<States>().SetAmmo(5);
            newUnit.GetComponent<States>().SetTypePoint(1);
            Angle cmp = newUnit.GetComponent<Angle>();
            cmp.enabled = false;
        }

        // 白ケル
        if (typeId == 3)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet2");
            Angle cmp = newUnit.GetComponent<Angle>();
            cmp.enabled = false;
        }

        // 紫ケル
        if (typeId == 4)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet3");
            newUnit.GetComponent<States>().SetFireRate(7);
            newUnit.GetComponent<States>().SetTypePoint(2);
            Angle cmp = newUnit.GetComponent<Angle>();
            cmp.enabled = false;

        }

        // ゴーレム
        if (typeId == 5)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet7");
            newUnit.GetComponent<States>().SetFireRate(7);
            Angle cmp = newUnit.GetComponent<Angle>();
            cmp.enabled = false;
        }

        // 氷ゴーレム
        if (typeId == 6)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet8");
            newUnit.GetComponent<States>().SetFireRate(15);
            newUnit.GetComponent<States>().SetCoolTime(1);
            newUnit.GetComponent<States>().SetAmmo(3);
            newUnit.GetComponent<States>().SetTypePoint(4);
            Angle cmp = newUnit.GetComponent<Angle>();
            cmp.enabled = false;
        }

        //ドラゴン
        if (typeId == 7)
        {
            newBullet = Resources.Load<GameObject>("Prefabs/PlayerBullet6");
            newUnit.GetComponent<States>().SetFireRate(1);
            newUnit.GetComponent<States>().SetCoolTime(5);
            newUnit.GetComponent<States>().SetAmmo(15);
            newUnit.GetComponent<States>().SetTypePoint(7);



            Angle cmp = newUnit.GetComponent<Angle>();
            cmp.enabled = true;
            cmp.SetDir(30.0f);
            cmp.SetAng(150.0f);
            cmp.SetSpd(3.0f);
            cmp.SetBullet(newBullet);
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