using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{

    //Statesコンポーネント
    States states;

    //挟撃を受けているか？
    private bool IsPincer;

    //正面被弾
    private bool flontHit;
    //背面被弾
    private bool backHit;

    private float flontCnt;//正面被弾判定待機時間

    private float backCnt;//背面被弾判定待機時間

    //挟撃ボーナスダメージ
    private int pincerBonusDamage = 2;

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

        IsPincer = false;

        flontHit = false;

        flontCnt = 2.0f;

        backHit = false;

        backCnt = 2.0f;


    }

    // Update is called once per frame
    void Update()
    {

        if (flontHit && backHit)
        {
            IsPincer = true;
        }
        else
        {
            IsPincer = false;
        }


        if (flontHit)
        {
            flontCnt -= Time.deltaTime;
            if (flontCnt <= 0)
            {
                flontHit = false;
                Debug.Log("正面リセット");
            }
        }


        if (backHit)
        {
            backCnt -= Time.deltaTime;
            if (backCnt <= 0)
            {
                backHit = false;
                Debug.Log("背面リセット");
            }
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
                    if (Singleton<SoundManager>.instance.getIsMute() == false)
                    {
                        //ガード音再生
                        Singleton<SoundManager>.instance.playSE("se009");
                    }
                    if (guardEffect != null)//エフェクトスロットに設定してある場合
                    {
                        if (col.gameObject.GetComponent<Bullet>().getFlag() == false)
                        {
                            guardEffect.transform.position = col.transform.position;//エフェクト位置設定
                        }
                        else {
                            guardEffect.transform.position = this.transform.position;//エフェクト位置設定

                        }
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

            }
            else
            {


                if (IsPincer == false)//非挟撃時
                {

                    if (col.GetComponent<Bullet>().getInverdFlag() == false)//被弾した弾の向きが反転していなければ
                    {
                        flontHit = true;
                        Debug.Log("正面被弾");
                        flontCnt = 2.0f;
                    }

                    if (col.GetComponent<Bullet>().getInverdFlag() == true)//被弾した弾の向きが反転していれば
                    {
                        backHit = true;
                        Debug.Log("背面被弾");
                        backCnt = 2.0f;

                    }

                    if (hitEffect != null)//エフェクトスロットに設定してある場合
                    {
                        hitEffect.transform.position = col.transform.position;//エフェクト位置設定
                        Instantiate(hitEffect);//エフェクト生成
                    }

                    Singleton<SoundManager>.instance.playSE("se001");//サウンド

                    states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());//ダメージ判定

                }
                else if (IsPincer == true)//挟撃成立時
                {
                    if (col.GetComponent<Bullet>().getInverdFlag() == false)//被弾した弾の向きが反転していなければ
                    {

                        flontCnt = 2.0f;
                    }

                    if (col.GetComponent<Bullet>().getInverdFlag() == true)//被弾した弾の向きが反転していれば
                    {

                        backCnt = 2.0f;

                    }


                    Debug.Log("挟撃成功");

                    if (pincerEffect != null)//エフェクトスロットに設定してある場合
                    {
                        pincerEffect.transform.position = col.transform.position;//エフェクト位置設定
                        Instantiate(pincerEffect);//エフェクト生成

                    }

                    Singleton<SoundManager>.instance.playSE("se001");//挟撃サウンド

                    states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage() + pincerBonusDamage);//ダメージ判定(挟撃ボーナス込み)

                }



            }
            if (col.gameObject.GetComponent<Bullet>().getFlag() == false)
            {
                Destroy(col);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        float cnt = 0;
        if (col.gameObject.tag == "Bullet")
        {
            cnt = Time.deltaTime;

            if (cnt > 1.0f)
            {
                states.setDamege(col.gameObject.GetComponent<Bullet>().getBulletDamage());
                if (hitEffect != null)//エフェクトスロットに設定してある場合
                {
                    Debug.Log("継続ダメージ");
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

    void OnCollisionExit2D(Collision2D col)
    {




    }



}
