﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour {

    //Statesコンポーネント
    States states;

    

    [SerializeField]
    GameObject hitEffect;//被弾時エフェクト

    [SerializeField]
    GameObject guardEffect;//攻撃防御時エフェクト

    



    // Use this for initialization
    void Start ()
    {

        //Statesコンポーネントの取得
        states = GetComponent<States>();


      





    }
	
	// Update is called once per frame
	void Update ()
    {



            

	}


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")//弾との判定
        {

            if (this.gameObject.GetComponent<States>().GetAbilityType() == 1)//正面無効タイプの場合
            {
                if (col.GetComponent<Bullet>().getInverdFlag() == false)//被弾した弾の向きが反転していなければ
                {


                    Singleton<SoundManager>.instance.playSE("se009");

                    if (guardEffect != null)//エフェクトスロットに設定してある場合
                    {
                        guardEffect.transform.position = col.transform.position;//エフェクト位置設定

                        Instantiate(guardEffect);//エフェクト生成
                    }

                   
                }
                else
                {

                    Singleton<SoundManager>.instance.playSE("se001");
                    states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());//ダメージ判定


                    if (hitEffect != null)//エフェクトスロットに設定してある場合
                    {
                        hitEffect.transform.position = col.transform.position;//エフェクト位置設定

                        Instantiate(hitEffect);//エフェクト生成
                    }

                }

            }
            else
            {


                


                if (hitEffect != null)//エフェクトスロットに設定してある場合
                {
                    hitEffect.transform.position = col.transform.position;//エフェクト位置設定

                    Instantiate(hitEffect);//エフェクト生成
                }

                Singleton<SoundManager>.instance.playSE("se001");
                states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());//ダメージ判定

               
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
