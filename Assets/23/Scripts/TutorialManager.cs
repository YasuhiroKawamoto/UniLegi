using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    private int StepFlag;//進行度合い

    GameObject TextBox;//文字表示オブジェクト

    Collider2D Zone;//指定ゾーン

    private bool IsClear;//条件を満たしているか？

    GameObject Manager;//ゲームマネージャ

    GameObject Player;//ゲームプレイヤー

    GameObject Enemy1;//通常の敵

    GameObject TestEnemy1;//通常の敵（実体）

    GameObject Enemy2;//正面無効の敵

    GameObject TestEnemy2;//正面無効の敵(実体)

    // Use this for initialization
    void Start () {
        //進行度の初期化
        StepFlag = 0;
        //状態の初期化
        IsClear = true;
        //ゲームマネージャの取得
        Manager = GameObject.Find("GameManager");
        Player = GameObject.Find("Player");
        //指定位置の初期化
        Zone = this.GetComponent<BoxCollider2D>();
        //Tutorial用のエネミーの登録
        Enemy1 = (GameObject)Resources.Load("Prefabs/TestEnemy1");
        Enemy2 = (GameObject)Resources.Load("Prefabs/TestEnemy2");
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (StepFlag != 4)
        {
            Player.GetComponent<PlayerControl>().SetUnionCoolTime(100);
        }

        

        ChangeStep();
  
	}


    void OnTriggerEnter2D(Collider2D col)
    {
        //ユニットを指定ゾーンに置いたら次のステップに
        if (StepFlag == 1 && col.tag == ("HavingPlayer"))
        {
            StepFlag = 2;
            IsClear = true;
            Zone.transform.position = new Vector3(0, 30, 0);
            //Destroy(Zone);
        }
    }

    void ChangeStep()
    {
        if (IsClear == true)
        {
            switch (StepFlag)
            {
                case 0:
                    Debug.Log("STEP0");
                    break;
                case 1:
                    Debug.Log("STEP1");
                    TestEnemy1 = Instantiate(Enemy1);
                    TestEnemy1.transform.position = new Vector3(0, 0, 0);
                    break;
                case 2:
                    Debug.Log("STEP2");
                    break;
                case 3:
                    Debug.Log("STEP3");
                    TestEnemy2 = Instantiate(Enemy2);
                    TestEnemy2.transform.position = new Vector3(0, 0, 0);
                    break;
                case 4:
                    Debug.Log("STEP4");
                    Player.GetComponent<PlayerControl>().SetUnionCoolTime(0);
                    break;
                case 5:
                    Debug.Log("Clear");
                    break;

            }

            IsClear = false;

        }

        //ユニットの召喚に成功したら次のステップへ
        if (StepFlag == 0 && Manager.GetComponent<GameManager>().GetUnitNum() != 0)
        {
            IsClear = true;
            StepFlag = 1;
        }

        if (TestEnemy1 != null)
        {
            if (StepFlag == 2 && TestEnemy1.GetComponent<States>().getDead() == true)
            {
                IsClear = true;
                StepFlag = 3;
            }
        }

        if (TestEnemy2 != null)
        {
            if (StepFlag == 3 && TestEnemy2.GetComponent<States>().getDead() == true)
            {
                IsClear = true;
                StepFlag = 4;
            }
        }

        if (StepFlag == 4&&Player.GetComponent<PlayerControl>().getIsCreated() == true)
        {
            IsClear = true;
            StepFlag = 5;
        }

    }

    public int getCurrentStep()
    {
        return StepFlag;
    }
  

    
}

