using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random : MonoBehaviour
{

    //∞モード用スクリプトに仕様変更
    
    //敵プレハブを変数に代入
    [SerializeField]
    private GameObject Enemy;//読み込み用敵
    private GameObject SetEnemy1;//生成用敵
    [SerializeField]
    private GameObject Enemy2;//読み込み用敵
    private GameObject SetEnemy2;//生成用敵
    [SerializeField]
    private GameObject Enemy3;//読み込み用敵
    private GameObject SetEnemy3;//生成用敵
    [SerializeField]
    private GameObject Enemy4;//読み込み用敵
    private GameObject SetEnemy4;//生成用敵
    [SerializeField]
    private GameObject Enemy5;//読み込み用敵
    private GameObject SetEnemy5;//生成用敵


    [SerializeField]
    private GameObject BOSS1;
    private GameObject SetBOSS1;

    [SerializeField]
    private GameObject BOSS2;
    private GameObject SetBOSS2;

   

    //出現時間の為のカウント
    [SerializeField]
    private float cnt = 0;

    //敵の出現時間
    [SerializeField]
    private float SetTime = 2;
    //敵の種類
    [SerializeField]
    private int enemyNum = 0;

    private int enemyType = 0;

    private int BossCnt = 0;

    // Use this for initialization
    void Start()
    {
        




    }

    void Update()
    {
        cnt += Time.deltaTime;

        //カウントが設定時間に到達したら
        if (cnt >= SetTime)
        {
            //オブジェクトの座標
            float x = Random.Range(-3.5f, 3.5f);
            float y = gameObject.transform.position.y;
            float z = 1.0f;

            if ((enemyNum+1) % 10 != 0)//10の倍数以外では雑魚出現
            {
                enemyType = Random.Range(0, 5);
                //敵オブジェクトを生産（1から順に）
                switch (enemyType)
                {
                    case 0:
                        SetEnemy1 = Instantiate(Enemy, new Vector3(x, y, z), Quaternion.identity);//召喚
                        SetEnemy1.GetComponent<States>().StatesUpEnemy(Enemy,enemyNum);//能力向上
                        break;
                    case 1:
                        SetEnemy2 = Instantiate(Enemy2, new Vector3(x, y, z), Quaternion.identity);//召喚
                        SetEnemy2.GetComponent<States>().StatesUpEnemy(Enemy2,enemyNum);//能力向上
                        break;
                    case 2:
                        SetEnemy3 = Instantiate(Enemy3, new Vector3(x, y, z), Quaternion.identity);//召喚
                        SetEnemy3.GetComponent<States>().StatesUpEnemy(Enemy3,enemyNum);//能力向上
                        break;
                    case 3:
                        SetEnemy4 = Instantiate(Enemy4, new Vector3(x, y, z), Quaternion.identity);//召喚
                        SetEnemy4.GetComponent<States>().StatesUpEnemy(Enemy4,enemyNum);//能力向上
                        break;
                    case 4:
                        SetEnemy5 = Instantiate(Enemy5, new Vector3(x, y, z), Quaternion.identity);//召喚
                        SetEnemy5.GetComponent<States>().StatesUpEnemy(Enemy5,enemyNum);//能力向上
                        break;
                }
            }
            else
            {
                switch (BossCnt)
                {
                    case 0:
                        SetBOSS1 = Instantiate(BOSS1, new Vector3(0, y, z), Quaternion.identity);//召喚
                        SetBOSS1.GetComponent<States>().StatesUpBoss(BOSS1,enemyNum);//能力向上
                        BossCnt++;
                        break;
                    case 1:
                        SetBOSS2 = Instantiate(BOSS2, new Vector3(0, y, z), Quaternion.identity);//召喚
                        SetBOSS2.GetComponent<States>().StatesUpBoss(BOSS2,enemyNum);//能力向上
                        BossCnt++;
                        BossCnt++;
                        break;
                }
                if (BossCnt < 1)
                {
                    BossCnt = 0;
                }

            }      
            cnt = 0;                  
            enemyNum++;           
        }


        
       
       
       
      


      
       


    }
}