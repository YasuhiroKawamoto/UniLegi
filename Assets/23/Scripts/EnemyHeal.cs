using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : MonoBehaviour {

    //Statesコンポーネント
    States states;

    Collider2D C;

  
    private List<GameObject> target;



    [SerializeField]
    GameObject effect;

    private float rate;

    private float cnt;



    private bool HealFlag;

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


      

        HealFlag = false;

        target = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (states.getDead() == false)
        {
            if (HealFlag)//フラグがONであれば
            {
                cnt += Time.deltaTime;

                if (rate <= cnt)//起動間隔にカウントが到達
                {
                    if (target != null)
                    {
                        foreach (GameObject obj in target)//範囲内ユニットに対して
                        {
                            if (obj.gameObject.GetComponent<States>().GetLockOn() == true)
                            {
                                if (obj.gameObject.GetComponent<States>().getHp() < obj.gameObject.GetComponent<States>().getMaxHp())
                                {
                                    obj.GetComponent<States>().setDamege(-states.getAttack());//回復判定
                                    Debug.Log("回復");
                                    effect.transform.position = obj.transform.position;//エフェクトの位置を設定
                                    if (effect != null)//エフェクトスロットに設定してある場合
                                    {
                                        Instantiate(effect);//エフェクト生成
                                    }
                                }
                                obj.gameObject.GetComponent<States>().SetLockOn(false);
                            }

                        }
                    }
                  
                    cnt = 0;//カウントリセット
                    target.Clear();

                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enamy")//接触オブジェクトタグがPlayer
        {
            if (col.gameObject.GetComponent<States>().GetLockOn() == false)
            {
                if (gameObject.GetComponent<States>().getHp() < gameObject.GetComponent<States>().getMaxHp())
                {
                    target.Add(col.transform.gameObject);
                    col.gameObject.GetComponent<States>().SetLockOn(true);
                }
            }
            HealFlag = true;//攻撃フラグON
            Debug.Log("接敵");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Enemy")//接触オブジェクトタグがPlayer
        {
            
            if (col.gameObject.GetComponent<States>().GetLockOn() == false)
            {
                if (col.gameObject.GetComponent<States>().getHp() < col.gameObject.GetComponent<States>().getMaxHp())
                {
                    target.Add(col.transform.gameObject);
                    col.gameObject.GetComponent<States>().SetLockOn(true);
                }
            }
            HealFlag = true;//攻撃フラグON
            //Debug.Log("接敵");
            //this.transform.parent.GetComponent<Mover>().setMoveFlag(false);//移動を止める
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            target.Clear();
            col.gameObject.GetComponent<States>().SetLockOn(false);
            HealFlag = false;//攻撃フラグOFF
            //this.transform.parent.GetComponent<Mover>().setMoveFlag(true);//移動を開始
            //Debug.Log("離脱");
        }

    }
}


