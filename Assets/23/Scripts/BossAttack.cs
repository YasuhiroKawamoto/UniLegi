using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

    //Statesコンポーネント
    States states;

    Collider2D C;

    Vector2 pos;
    private List<GameObject> target;
       

   
    [SerializeField]
    GameObject effect;

    private float rate;

    private float cnt;

    private int PlayerCnt;

    private bool AttackFlag;

    // Use this for initialization
    void Start()
    {
        states = this.gameObject.transform.parent.GetComponent<States>();

        this.gameObject.transform.position = new Vector3(this.gameObject.transform.parent.position.x,
            (this.gameObject.transform.parent.position.y - 0.2f), this.gameObject.transform.parent.position.z);

        C = this.gameObject.GetComponent<Collider2D>();

        C.transform.localScale = new Vector3(1.0f, states.getRenge(), 0.0f);

        rate = states.getrate();

        cnt = 0;


        PlayerCnt = 0;

        AttackFlag = false;

        target = new List<GameObject>();


    }

    // Update is called once per frame
    void Update()
    {

        if (states.getDead() ==false)
        {

            if (AttackFlag)//攻撃フラグがONであれば
            {
                cnt += Time.deltaTime;

                if (rate <= cnt)//攻撃間隔にカウントが到達
                {
                    foreach (GameObject obj in target)//範囲内ユニットに対して
                    {

                        if (obj.gameObject.tag == "Player")//接触オブジェクトタグがPlayer
                        {
                            obj.GetComponent<States>().setDamege(states.getAttack());//ダメージ判定

                            effect.transform.position = obj.transform.position;//エフェクトの位置を設定
                            if (effect != null)//エフェクトスロットに設定してある場合
                            {
                                Instantiate(effect);//エフェクト生成
                            }

                        }
                        else if (obj.gameObject.tag == "DangerZone")//接触オブジェクトタグがDangerZone
                        {
                            obj.GetComponent<DangerZone>().SetHp(obj.GetComponent<DangerZone>().GetHp() - states.getAttack());//ダメージ判定

                            effect.transform.position = new Vector3(this.gameObject.transform.position.x, obj.transform.position.y);//エフェクト位置設定

                            if (effect != null)//エフェクトスロットに設定してある場合
                            {
                                Instantiate(effect);//エフェクト生成
                            }
                        }

                    }

                    Debug.Log("攻撃");
                    cnt = 0;//カウントリセット
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {

            if (col.gameObject.tag == "Player")//接触オブジェクトタグがPlayer
            {
            target.Add(col.gameObject);

            AttackFlag = true;//攻撃フラグON

            col.gameObject.GetComponent<States>().SetLockOn(true);
                Debug.Log("接敵");

            }

            if (col.gameObject.tag == "DangerZone")//接触オブジェクトがDangerZone
            {
            target.Add(col.gameObject);
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
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "HavingPlayer")
        {
            target.Clear();
            col.gameObject.GetComponent<States>().SetLockOn(true);
            AttackFlag = false;//攻撃フラグOFF
            this.transform.parent.GetComponent<Mover>().setMoveFlag(true);//移動を開始
            Debug.Log("離脱");
        }



        if (col.gameObject.tag == "DangerZone")
        {
            target.Clear();
            col.gameObject.GetComponent<States>().SetLockOn(true);
            AttackFlag = false;//攻撃フラグOFF
            this.transform.parent.GetComponent<Mover>().setMoveFlag(true);//移動を開始
            Debug.Log("離脱");

        }
    }
}
