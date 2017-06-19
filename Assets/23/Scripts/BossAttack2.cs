using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2 : MonoBehaviour {
    //Statesコンポーネント
    States states;

    [SerializeField]
    GameObject effect;//召喚エフェクト
    [SerializeField]
    GameObject enemy1;
    [SerializeField]
    GameObject enemy2;
    [SerializeField]
    GameObject enemy3;

    private Vector3 SammonPos1;//召喚位置1

    private Vector3 SammonPos2;//召喚位置2

    private Vector3 SammonPos3;//召喚位置3

    private float rate;

    private float timeCnt;

    private bool AttackFlag;

    // Use this for initialization
    void Start()
    {
        states = this.gameObject.transform.parent.GetComponent<States>();

        rate = states.getrate();

        timeCnt = 0;

        AttackFlag = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (states.getDead() == false)
        {

            if (AttackFlag)//攻撃フラグがONであれば
            {
                timeCnt += Time.deltaTime;

                if (rate <= timeCnt)//攻撃間隔にカウントが到達
                {
                    SammonPos1 = this.gameObject.transform.position + new Vector3(-2.0f,0,0);
                    SammonPos2 = this.gameObject.transform.position + new Vector3(2.0f, 0, 0);
                    SammonPos3 = this.gameObject.transform.position + new Vector3(0, -1.0f, 0);


                    

                    SammonEnemy(SammonPos1);

                    SammonEnemy(SammonPos2);

                    SammonEnemy(SammonPos3);


                    Debug.Log("仲間召喚");
                    timeCnt = 0;//カウントリセット
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")//接触オブジェクトタグがPlayer
        {
           

            AttackFlag = true;//攻撃フラグON
            Debug.Log("接敵");

        }

        if (col.gameObject.tag == "DangerZone")//接触オブジェクトがDangerZone
        {
          
            AttackFlag = true;//攻撃フラグON
            Debug.Log("拠点接敵");

            this.transform.parent.GetComponent<Mover>().setMoveFlag(false);//移動を止める
        }

        if (col.gameObject.tag == "HavingPlayer")//接触オブジェクトタグがHavingPlayer
        {
            this.transform.parent.GetComponent<Mover>().setMoveFlag(false);//移動を止める
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "DangerZone")//接触オブジェクトタグがPlayer
        {
            AttackFlag = true;//攻撃フラグON
            Debug.Log("接敵");
            this.transform.parent.GetComponent<Mover>().setMoveFlag(false);//移動を止める
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "HavingPlayer")
        {
           
            AttackFlag = false;//攻撃フラグOFF
            this.transform.parent.GetComponent<Mover>().setMoveFlag(true);//移動を開始
            Debug.Log("離脱");
        }



        if (col.gameObject.tag == "DangerZone")
        {
           
          
            AttackFlag = false;//攻撃フラグOFF
            this.transform.parent.GetComponent<Mover>().setMoveFlag(true);//移動を開始
            Debug.Log("離脱");

        }
    }

    void SammonEnemy(Vector3 pos)
    {
        GameObject obj;
        int SammonType = Random.Range(1, 4);

        switch (SammonType)
        {
            case 1:
                obj = enemy1;
                obj.transform.position = pos;
                if (effect)
                {
                    effect.transform.position = pos;
                    Instantiate(effect);
                }
                obj = Instantiate(enemy1);
                break;
            case 2:
                obj = enemy2;
                obj.transform.position = pos;
                if (effect)
                {
                    effect.transform.position = pos;
                    Instantiate(effect);
                }
                obj = Instantiate(enemy2);
                break;
            case 3:
                obj = enemy3;
                obj.transform.position = pos;
                if (effect)
                {
                    effect.transform.position = pos;
                    Instantiate(effect);
                }
                obj = Instantiate(enemy3);
                break;
        }
    }
     
    


}
