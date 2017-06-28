using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

    //Statesコンポーネント
    States states;


   
    private List<GameObject> target;
       

   
    [SerializeField]
    GameObject effect;

    private float rate;

    private float cnt;

  

    private bool AttackFlag;

    // Use this for initialization
    void Start()
    {
        states = this.gameObject.transform.parent.GetComponent<States>();

        this.gameObject.transform.position = new Vector3(this.gameObject.transform.parent.position.x,
            (this.gameObject.transform.parent.position.y - 0.2f), this.gameObject.transform.parent.position.z);

       

        rate = states.getrate();

        cnt = 0;



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
                    if(target != null)
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

            

           
       
    }

    void OnTriggerStay2D(Collider2D col)
    {

       

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "HavingPlayer")
        {
            target.Clear();
            col.gameObject.GetComponent<States>().SetLockOn(false);
            AttackFlag = false;//攻撃フラグOFF
            this.transform.parent.GetComponent<Mover>().setMoveFlag(true);//移動を開始
            Debug.Log("離脱");
        }



        
    }
}
