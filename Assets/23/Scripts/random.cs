using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random : MonoBehaviour
{

    //敵を順次ランダムなポジションで生成

    //敵プレハブを変数に代入
    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private GameObject Enemy2;
    [SerializeField]
    private GameObject Enemy3;
    [SerializeField]
    private GameObject Enemy4;

    //出現時間の為のカウント
    [SerializeField]
    private int cnt = 0;

    //敵の出現時間
    [SerializeField]
    private int SetTime;
    //敵の種類
    [SerializeField]
    private int enemyNum;


    // Use this for initialization
    void Start()
    {

        enemyNum = 0;

        SetTime = 200;


    }

    void Update()
    {
        cnt++;



        //カウントが設定時間に到達したら
        if (cnt >= SetTime)
        {

            //オブジェクトの座標
            float x = Random.Range(-3.5f, 3.5f);
            float y = gameObject.transform.position.y;
            float z = 1.0f;





            //敵オブジェクトを生産（1から順に）
            switch (enemyNum)
                {
                case 0:
                    Instantiate(Enemy, new Vector3(x, y, z), Quaternion.identity);
                    enemyNum++;
                    break;
                case 1:
                    Instantiate(Enemy2, new Vector3(x, y, z), Quaternion.identity);
                    enemyNum++;
                    break;
                case 2:
                    Instantiate(Enemy3, new Vector3(x, y, z), Quaternion.identity);
                    enemyNum++;
                    break;
                case 3:
                    Instantiate(Enemy4, new Vector3(x, y, z), Quaternion.identity);
                    enemyNum++;
                    break;

            }
            
            cnt = 0;
            

            if (enemyNum >= 4)
            {
                enemyNum = 0;
            }
        }





    }
}