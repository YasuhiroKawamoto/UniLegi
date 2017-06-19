using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{

    //Statesコンポーネント
    States states;

   
   
   

    [SerializeField]
    GameObject hitEffect;//被弾時エフェクト

    [SerializeField]
    GameObject guardEffect;//攻撃防御時エフェクト

    [SerializeField]
    GameObject pincerEffect;//攻撃防御時エフェクト





    // Use this for initialization
    void Start()
    {

        //Statesコンポーネントの取得
        states = GetComponent<States>();

     

        pincerEffect = (GameObject)Resources.Load("Prefabs/PincherA");

    }

    // Update is called once per frame
    void Update()
    {



    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Stoper")
        {
            gameObject.GetComponent<Mover>().setMoveFlag(false);
            //Debug.Log("停止");
        }


        if (col.gameObject.tag == "Bullet")//弾との判定
        {

            if (this.gameObject.GetComponent<States>().GetAbilityType() == 1)//正面無効タイプの場合
            {
                //ガード音再生
                Singleton<SoundManager>.instance.playSE("se009");

                if (guardEffect != null)//エフェクトスロットに設定してある場合
                {
                    if (col.gameObject.GetComponent<Bullet>().getFlag() == false)
                    {
                        guardEffect.transform.position = col.transform.position;//エフェクト位置設定
                    }
                    else
                    {
                        guardEffect.transform.position = this.transform.position;//エフェクト位置設定
                    }
                    states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());//ダメージ判定
                    Instantiate(guardEffect);//ガードエフェクト生成
                }
            }
            else
            {
                if (hitEffect != null)//エフェクトスロットに設定してある場合
                {
                    hitEffect.transform.position = col.transform.position;//エフェクト位置設定

                    Instantiate(hitEffect);//エフェクト生成
                }

                Singleton<SoundManager>.instance.playSE("se001");//サウンド

                states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());//ダメージ判定

            }
            if (col.gameObject.GetComponent<Bullet>().getFlag() == false)
            {
                Destroy(col);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Stoper")
        {
            gameObject.GetComponent<Mover>().setMoveFlag(false);
        }

        float cnt = 0;
        if (col.gameObject.tag == "Bullet")
        {
            cnt = Time.deltaTime;

            if (cnt > 1.0f)
            {
                states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());
                if (hitEffect != null)//エフェクトスロットに設定してある場合
                {
                    //Debug.Log("継続ダメージ");
                    hitEffect.transform.position = transform.position;//エフェクト位置設定
                    Instantiate(hitEffect);//エフェクト生成
                }
                cnt = 0;
            }
            if (col.gameObject.GetComponent<Bullet>().getFlag() == false)
            {
                Destroy(col);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Stoper")
        {
            gameObject.GetComponent<Mover>().setMoveFlag(true);
            //Debug.Log("移動開始");
        }
    }

}
