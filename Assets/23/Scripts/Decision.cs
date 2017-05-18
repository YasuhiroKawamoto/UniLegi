using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour {

    //Statesコンポーネント
    States states;

    [SerializeField]
    private GameManager manager;

    [SerializeField]
    GameObject hitEffect;

    [SerializeField]
    GameObject guardEffect;




    // Use this for initialization
    void Start ()
    {

        //Statesコンポーネントの取得
        states = GetComponent<States>();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();





    }
	
	// Update is called once per frame
	void Update ()
    {


        if (states.getHp() <= 0)
        {
            // コスト回復
            manager.RecoverCost(states.getCost());

            Destroy(this.gameObject);
        }

	}


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")//弾との判定
        {

            if (this.gameObject.GetComponent<States>().GetAbilityType() == 1)//正面無効タイプの場合
            {
                if (col.GetComponent<Bullet>().getInverdFlag() == false)//被弾した弾の向きが反転していなければ
                {
                    guardEffect.transform.position = col.transform.position;//エフェクト位置設定

                    if (guardEffect != null)//エフェクトスロットに設定してある場合
                    {
                        Instantiate(guardEffect);//エフェクト生成
                    }


                }
                else
                {
                    states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());//ダメージ判定

                    hitEffect.transform.position = col.transform.position;//エフェクト位置設定

                    if (hitEffect != null)//エフェクトスロットに設定してある場合
                    {
                        Instantiate(hitEffect);//エフェクト生成
                    }

                }

            }
            else
            {

                states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());//ダメージ判定

                hitEffect.transform.position = col.transform.position;//エフェクト位置設定


                if (hitEffect != null)//エフェクトスロットに設定してある場合
                {
                    Instantiate(hitEffect);//エフェクト生成
                }
            }
            Destroy(col);//弾の消滅
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Bullet")
        {


            Debug.Log("緋弾のアリアAA");

            states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());

            if (hitEffect != null)//エフェクトスロットに設定してある場合
            {
                Instantiate(hitEffect);//エフェクト生成
            }

            Destroy(col);

        }



    }

    void OnCollisionExit2D(Collision2D col)
    {


      

    }



}
