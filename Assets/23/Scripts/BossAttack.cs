using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

    //Statesコンポーネント
    States states;


   
    private List<GameObject> target;

    BoxCollider2D BC;
   
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

        BC = this.GetComponent<BoxCollider2D>();

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

							if (obj.gameObject.tag == "SPlayer"||obj.gameObject.tag == "Player")//接触オブジェクトタグがPlayer
                            {
                                obj.GetComponent<States>().setDamege(states.getAttack());//ダメージ判定

                                effect.transform.position = obj.transform.position;//エフェクトの位置を設定
                                if (effect != null)//エフェクトスロットに設定してある場合
                                {

                                    switch (states.GetAbilityType())//スーパーユニットの攻撃タイプは浴びタイプで判定
                                    {
                                        case 1://ぱららん
                                            effect.transform.position = BC.transform.position;//エフェクトの位置を設定
                                            Instantiate(effect);//エフェクト生成
                                                                //
                                            Singleton<SoundManager>.instance.playSE("se018");//パラ攻撃音
                                            break;

                                        case 2://倉健
                                            effect.transform.position = obj.transform.position;//エフェクトの位置を設定
                                            Instantiate(effect);//エフェクト生成
                                            Singleton<SoundManager>.instance.playSE("se019");//イカ攻撃音
                                            break;

                                    }
                                    Instantiate(effect);//エフェクト生成
                                }

                            }
                            obj.gameObject.GetComponent<States>().SetLockOn(false);

                        }
                    }

                    Debug.Log("攻撃");
                    target.Clear();
                    cnt = 0;//カウントリセット
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {

		if (col.gameObject.tag == "SPlayer"||col.gameObject.tag == "Player")//接触オブジェクトタグがPlayer
        {
            if (col.gameObject.GetComponent<States>().GetLockOn() == false)
            {
              
                    target.Add(col.transform.gameObject);
                    col.gameObject.GetComponent<States>().SetLockOn(true);
                
            }
            AttackFlag = true;
        }




        }

    void OnTriggerStay2D(Collider2D col)
    {
		if (col.gameObject.tag == "SPlayer"||col.gameObject.tag == "Player")//接触オブジェクトタグがPlayer
        {
            if (col.gameObject.GetComponent<States>().GetLockOn() == false)
            {

                target.Add(col.transform.gameObject);
                col.gameObject.GetComponent<States>().SetLockOn(true);

            }
            AttackFlag = true;
        }


    }

    void OnTriggerExit2D(Collider2D col)
    {
		if(col.gameObject.tag == "SPlayer"||col.gameObject.tag == "Player" || col.gameObject.tag == "HavingPlayer")
        {
            target.Clear();
            col.gameObject.GetComponent<States>().SetLockOn(false);
            AttackFlag = false;//攻撃フラグOFF
            this.transform.parent.GetComponent<Mover>().setMoveFlag(true);//移動を開始
            Debug.Log("離脱");
        }



        
    }
}
