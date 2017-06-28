using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Union : MonoBehaviour
{

    GameObject newUnit;
    PlayerControl player;

    static public Sprite tmpSprite;

    bool isattached = false;


    GameObject newBullet;

    int UnitBit = 0;        // 0b0000
    int UnitBitOld = 0;     // 0b0000

    // 進化フラグ
    bool canEvolve = false;
    const int Evolved = 8;      // 0b1000

    const int GOHST = 1;        // 0b0001
    const int KERBEROS = 2;     // 0b0010
    const int SALAMANDER = 3;   // 0b0011
    const int GOLEM = 4;        // 0b0100
    const int SUCCUBUS = 5;     // 0b0101
    const int HARPY = 6;        // 0b0110
    const int MINIDRAGON = 7;    // 0b0111

    const int GOHST2 = 9;       // 0b1001
    const int KERBEROS2 = 10;   // 0b1010
    const int SALAMANDER2 = 11; // 0b1011
    const int GOLEM2 = 12;      // 0b1100
    const int SUCCUBUS2 = 13;   // 0b1101
    const int HARPY2 = 14;      // 0b1110
    const int MINIDRAGON2s = 15;  // 0b1111






    // Use this for initialization
    void Start()
    {
        UnitBit = 0;
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        newUnit = player.newUnit;
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
            newState.SetTypePoint(CalcPrefabId(unions));

            // Hpをset    
            newState.SetHp(CalcHp(unions));

            // Attakをset
            newState.SetAtk(CalcAtk(unions));

            // prefabを設定
            player.newUnit.GetComponent<SpriteRenderer>().sprite = GetPrefab().GetComponent<SpriteRenderer>().sprite;
            player.newUnit.GetComponent<Firing>().SetBullet(GetPrefab().GetComponent<Firing>().GetBullet());

            // 予測ユニット設定
            tmpSprite = GetPrefab().GetComponent<SpriteRenderer>().sprite;
        }
    }

    private int CalcPrefabId(GameObject[] unions)
    {
        List<int> typePoints = new List<int>();
        int returnID = 0;
        int cnt = 0;

        UnitBit = 0;
        UnitBitOld = 0;
        canEvolve = true;
        // 範囲内ユニットを探索
        foreach (GameObject union in unions)
        {
            States state = union.GetComponent<States>();

            // 一つ前のビット状態を持つ
            UnitBitOld = UnitBit;

            // ユニットのビット和を算出
            UnitBit = UnitBit | state.GetTypePoint();

            // 「全て同じユニット」　という条件を満たせなかったとき進化フラグをへし折る
            if(UnitBitOld != 0 && (UnitBitOld ^ UnitBit) != 0)
            {
                canEvolve = false;
            }


            typePoints.Add(state.GetTypePoint());
            cnt++;
        }
        // 1体のみの時　進化フラグをへし折る
        if (cnt == 1)
        {
            canEvolve = false;
        }


        // 進化前であるかどうか
        if (((UnitBit & Evolved) & Evolved) == 0)
        {
            // 進化フラグが立っているとき
            if (canEvolve)
            {
                UnitBit = UnitBit | Evolved;
            }

            returnID = UnitBit;

        }
        // 進化後ユニットを含んでいるとき
        else

       // 最頻値が0のとき(最頻値が複数ある時)
       if (Mode(typePoints) == 0)
        {
            // 異種混合合体→IDが最大値
            returnID = Max(typePoints);
        }
        // 最頻値が確定された
        else
        {
            // 合体後同士合体後のまま
            returnID = Mode(typePoints);
        }
        return returnID;
    }
    private GameObject GetPrefab()
    {
        string Units = "Prefabs\\Units\\Unit";
        string[] unionPrefabs = { "Ghost", "Kerberos", "Salamander", "Golem", "Succubus", "Harpy", "MiniDragon", "",
       "Ghost2", "Kerberos2", "Salamander2", "Golem2", "Succubus2", "Harpy2", "MiniDragon2" };

        GameObject prefab;

        // 文字列を合成
        string InstantiatePrefab = Units + unionPrefabs[newUnit.GetComponent<States>().GetTypePoint() - 1];

        // 合成した文字列からprefabを参照
        prefab = Resources.Load<GameObject>(InstantiatePrefab);
        return prefab;

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