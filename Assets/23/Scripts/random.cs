using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random : MonoBehaviour
{

    //∞モード用スクリプトに仕様変更

    //敵プレハブを変数に代入
    [SerializeField]
    private GameObject Enemy;//読み込み用敵
    [SerializeField]
    private GameObject Enemy2;//読み込み用敵
    [SerializeField]
    private GameObject Enemy3;//読み込み用敵
    [SerializeField]
    private GameObject Enemy4;//読み込み用敵
    [SerializeField]
    private GameObject Enemy5;//読み込み用敵 
    [SerializeField]
    private GameObject Enemy6;//読み込み用敵


    //重複回避用リスト
    private List<int> list = new List<int>();
    private int EC = 0;

    //出現時間の為のカウント
    [SerializeField]
    private float cnt = 0;

    //敵の出現時間
    [SerializeField]
    private float SetTime = 2;
    //敵の種類
    [SerializeField]
    private int enemyNum = 0;

    private int Level = 0;//現在のレベル

    //範囲内に敵が存在するか？
    private bool IsExistence;




    //敵６体分の出現位置
    private Vector3[] EnemyPos = {  new Vector3(-2.5f,5, 1.0f),
                                    new Vector3(-1.5f, 5, 1.0f),
                                    new Vector3(-0.5f, 5, 1.0f),
                                    new Vector3(0.5f, 5, 1.0f),
                                    new Vector3(1.5f, 5, 1.0f),
                                    new Vector3(2.5f, 5, 1.0f)
                                   };



    


    // Use this for initialization
    void Start()
    {
        Level = 1;//レベルの初期化
        IsExistence = false;

    }

    void Update()
    {

        GetComponent<Rigidbody2D>().WakeUp();

        Level = Singleton<SceneData>.instance.getEnemyCnt() / 10;//敵10対撃破ごとにレベルアップ

        cnt += Time.deltaTime;

        //カウントが設定時間に到達したら
        if (cnt >= SetTime)
        {
            //オブジェクトの座標

            if (IsExistence == false)//エリア内に敵が存在しなければ
            {
              
                    if (Level < 5)//レベル５以下
                    {
                        SetEnemy(2);
                    }
                    else if (Level >= 5 && Level < 10)//レベル１０以下
                    {
                        SetEnemy(3);
                    }
                    else if (Level >= 10 && Level < 15)//レベル１5以下
                    {
                        SetEnemy(4);
                    }
                    else if (Level >= 15 && Level < 20)//レベル20以下
                    {
                        SetEnemy(5);
                    }
                    else
                    {
                        SetEnemy(6);
                    }

            
                

              
                enemyNum++;
            }
            cnt = 0;
            IsExistence = false;
        }





    }


    /// <summary>
    /// 指定数の敵を並列にセットする
    /// </summary>
    /// <param name="SetNum"></param>
    void SetEnemy(int SetNum)
    {


        while (true)
        {

            int enemyType = Random.Range(0, 6);//敵の種類をランダムに

            int random = Random.Range(0, 6);//位置をランダムに決定

            if (list.Contains(random))
            {
                Debug.Log("被った");
                continue;
            }

            list.Add(random);


            GameObject SetEnemy;
            switch (enemyType)
            {
                case 0:
                    SetEnemy = Instantiate(Enemy, EnemyPos[random], Quaternion.identity);
                    SetEnemy.GetComponent<States>().StatesUpEnemy(SetEnemy,Level);
                    break;
                case 1:
                    SetEnemy = Instantiate(Enemy2, EnemyPos[random], Quaternion.identity);
                    SetEnemy.GetComponent<States>().StatesUpEnemy(SetEnemy, Level);
                    break;
                case 2:
                    SetEnemy = Instantiate(Enemy3, EnemyPos[random], Quaternion.identity);
                    SetEnemy.GetComponent<States>().StatesUpEnemy(SetEnemy, Level);
                    break;
                case 3:
                    SetEnemy = Instantiate(Enemy4, EnemyPos[random], Quaternion.identity);
                    SetEnemy.GetComponent<States>().StatesUpEnemy(SetEnemy, Level);
                    break;
                case 4:
                    SetEnemy = Instantiate(Enemy5, EnemyPos[random], Quaternion.identity);
                    SetEnemy.GetComponent<States>().StatesUpEnemy(SetEnemy, Level);
                    break;
                case 5:
                    SetEnemy = Instantiate(Enemy6, EnemyPos[random], Quaternion.identity);
                    SetEnemy.GetComponent<States>().StatesUpEnemy(SetEnemy, Level);
                    break;
            }
            EC++;

            if (EC >= SetNum)
            {
                EC = 0;
                list.Clear();//リストの消去
                break;
            }

        }

    }
   


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IsExistence = true;
            //Debug.Log("敵存在");
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        IsExistence = false;
    //    }



    //}


}