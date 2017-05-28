using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    //Statesコンポーネント
    States states;

    Collider2D C;

    Vector2 pos;

    GameObject target;

    [SerializeField]
    GameObject effect;

   

   private float rate;

   private float cnt;

    private bool AttackFlag;

    // Use this for initialization
    void Start () {

       
 
        states = this.gameObject.transform.parent.GetComponent<States>();

      this.gameObject.transform.position =  new Vector3 (this.gameObject.transform.parent.position.x,
          (this.gameObject.transform.parent.position.y-0.2f), this.gameObject.transform.parent.position.z);

        C = this.gameObject.GetComponent<Collider2D>();

        C.transform.localScale = new Vector3(1.0f, states.getRenge(), 0.0f);

        rate = states.getrate();

        cnt = 0;

        AttackFlag = false;

       
    }

    // Update is called once per frame
    void Update () {

        if (this.gameObject.GetComponentInParent<States>().GetDead() == false)
        {


            if (AttackFlag)//攻撃フラグがONであれば

            states.SetCharge(cnt);

            if (rate <= cnt)//攻撃間隔にカウントが到達
            {
                cnt += Time.deltaTime;

                if (rate <= cnt)//攻撃間隔にカウントが到達
                {

                    if (target.gameObject.tag == "Player")//接触オブジェクトタグがPlayer
                    {
                        target.GetComponent<States>().setDamege(states.getAttack());//ダメージ判定

                        effect.transform.position = target.transform.position;//エフェクトの位置を設定
                    }
                    else if (target.gameObject.tag == "DangerZone")//接触オブジェクトタグがDangerZone
                    {
                        target.GetComponent<DangerZone>().SetHp(target.GetComponent<DangerZone>().GetHp() - states.getAttack());//ダメージ判定

                        effect.transform.position = new Vector3(this.gameObject.transform.position.x, target.transform.position.y);//エフェクト位置設定
                    }


                    if (effect != null)//エフェクトスロットに設定してある場合
                    {
                        if (Singleton<SoundManager>.instance.getIsMute() == false)
                        {
                            Singleton<SoundManager>.instance.playSE("se001");
                        }

                        Instantiate(effect);//エフェクト生成
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
            AttackFlag = true;//攻撃フラグON
            Debug.Log("接敵");
            target = col.gameObject;//接触オブジェクトを攻撃対象に指定
            this.transform.parent.GetComponent<Mover>().setMoveFlag(false);//移動を止める
        }

        if (col.gameObject.tag == "DangerZone")//接触オブジェクトがDangerZone
        {
            AttackFlag = true;//攻撃フラグON
            Debug.Log("拠点接敵");
            target = col.gameObject;//接触オブジェクトを攻撃対象に指定
            this.transform.parent.GetComponent<Mover>().setMoveFlag(false);//移動を止める
        }

        if (col.gameObject.tag == "HavingPlayer")//接触オブジェクトタグがHavingPlayer
        {
            this.transform.parent.GetComponent<Mover>().setMoveFlag(true);//そのまま通過
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "DangerZone")//接触オブジェクトタグがPlayer
        {
            AttackFlag = true;//攻撃フラグON
            Debug.Log("接敵");
            target = col.gameObject;//接触オブジェクトを攻撃対象に指定
            this.transform.parent.GetComponent<Mover>().setMoveFlag(false);//移動を止める
        }


    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "DangerZone" || col.gameObject.tag == "Player" || col.gameObject.tag == "HavingPlayer")
            {
            AttackFlag = false;//攻撃フラグOFF
            this.transform.parent.GetComponent<Mover>().setMoveFlag(true);//移動を開始
            Debug.Log("離脱");
        }
    }

}
