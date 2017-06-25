using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideRangeAttack : MonoBehaviour
{
    /// <summary>
    /// 味方ユニット広範囲攻撃スクリプト
    /// </summary>
    States states;

    BoxCollider2D BC;

    GameObject Line;

    [SerializeField]
    GameObject effect;

    private float attackRate;

    private float attackCnt;

    private bool AttackFlag;

    private List<GameObject> TargetEnemy;


    // Use this for initialization
    void Start()
    {
        states = GetComponentInParent<States>();

        Line = GameObject.Find("FrontLine");

        BC = this.GetComponent<BoxCollider2D>();
        attackCnt = 0;

        attackRate = states.getrate();

        AttackFlag = false;

        TargetEnemy = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        BC.transform.position = new Vector3(0, Line.transform.position.y + 0.5f, 0);


        if (states.getDead() == false)
        {

            if (AttackFlag)//攻撃フラグがONであれば
            {
                attackCnt += Time.deltaTime;

                if (attackRate <= attackCnt)//攻撃間隔にカウントが到達
                {
                    foreach (GameObject obj in TargetEnemy)//範囲内ユニットに対して
                    {


                        obj.GetComponent<States>().setDamege(states.getAttack());//ダメージ判定

                       
                        if (effect != null)//エフェクトスロットに設定してある場合
                        {
                            switch (states.GetAbilityType())//スーパーユニットの攻撃タイプは浴びタイプで判定
                            {
                                case 1://ぱららん
                                    effect.transform.position = BC.transform.position;//エフェクトの位置を設定
                                    Instantiate(effect);//エフェクト生成
                                    break;

                                case 2://倉健
                                    effect.transform.position = obj.transform.position;//エフェクトの位置を設定
                                    Instantiate(effect);//エフェクト生成
                                    break;

                            }
                          
                        }

                    }

                    Debug.Log("攻撃発動");
                    attackCnt = 0;//カウントリセット
                }

               
              
            }
        }

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")//接触オブジェクトタグがPlayer
        {
            TargetEnemy.Add(col.gameObject);

            AttackFlag = true;//攻撃フラグON

            col.gameObject.GetComponent<States>().SetLockOn(true);
            Debug.Log("接敵");

        }


    }

    void OnTriggerStay2D(Collider2D col)
    {

    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            TargetEnemy.Clear();
            col.gameObject.GetComponent<States>().SetLockOn(false);
            AttackFlag = false;//攻撃フラグOFF

            Debug.Log("離脱");
        }

    }

}



