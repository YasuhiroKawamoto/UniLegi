using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{


    [SerializeField]
    private float farstMoveTime;//第一指定時間

    [SerializeField]
    private float secondMoveTime;//第二指定時間


    [SerializeField]
    GameObject MoveEffect;//移動時エフェクト

    private float WaitEffectTime = 0;//エフェクト待機時間
    
    private Animator walk;

    

    //移動速度
    [SerializeField]
    private Vector2 Speed = new Vector2(0.0f, -0.03f);//速度１
    [SerializeField]
    private Vector2 Speed2 = new Vector2(0.03f, 0.0f);//速度２

    private Vector2 reverseX = new Vector2(-1.0f, 1.0f);//X反転ベクトル

    private bool ReversFlag = false;//左右反転フラグ

    private bool moveFlag = true;//移動フラグ

    private float MC = 0;//移動用カウント

    Rigidbody2D RB;//リジットボディ

    // Use this for initialization
    void Start()
    {

        RB = this.gameObject.GetComponent<Rigidbody2D>();
        this.walk = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<States>().getDead() == false)
        {
           
            if (this.gameObject.GetComponent<States>().GetMoveType() == 5)//ランダム指定タイプだった場合
            {
                this.gameObject.GetComponent<States>().SetMoveType(Random.Range(1, 4));//移動タイプをランダムにする
            }



            if (moveFlag == true)//フラグがtrueの時のみ動く
            {
                MC += Time.deltaTime;//経過時間取得

                WaitEffectTime += Time.deltaTime;//経過時間取得

                

                if (MoveEffect != null)//エフェクトスロットに設定してある場合
                {

                    if (WaitEffectTime >= 0.0f)
                    {
                        MoveEffect.transform.position = this.gameObject.transform.position;//エフェクト位置指定 
                        Instantiate(MoveEffect);//エフェクト生成

                        WaitEffectTime = 0;
                    }

                }


                if (this.gameObject.GetComponent<States>().GetMoveType() == 1)//移動タイプ１の場合
                {
                    //速度加算
                    RB.velocity = Speed;
                }
                else if (this.gameObject.GetComponent<States>().GetMoveType() == 2)//移動タイプ2の場合
                {
                   

                    if (MC <= farstMoveTime)//第一指定時間に到達する前
                    {

                        RB.velocity = Speed; //速度加算
                    }
                    else//第一指定時間に到達後、第二指定時間到達まで
                    {
                        if (ReversFlag == false)
                        {
                            RB.velocity = Speed2; //速度加算
                        }
                        else
                        {
                            RB.velocity = (Vector2.Scale(Speed2, reverseX)); //速度加算
                        }
                    }


                    if (MC >= secondMoveTime)//第2指定時間に到達
                    {

                        switch (ReversFlag)//反転フラグ反転
                        {
                            case true:
                                ReversFlag = false;
                                break;
                            case false:
                                ReversFlag = true;
                                break;
                        }

                        MC = 0.0f;//カウントリセット
                    }

                }
                else if (this.gameObject.GetComponent<States>().GetMoveType() == 3)//移動タイプ3の場合
                {

                    if (MC <= farstMoveTime)//第一指定時間に到達する前
                    {

                        RB.velocity = Speed; //速度加算

                    }
                    else//第一指定時間に到達後、第二指定時間到達まで
                    {

                        if (ReversFlag == false)
                        {
                            RB.velocity = Speed2; //速度加算
                        }
                        else
                        {
                            RB.velocity = (Vector2.Scale(Speed2, reverseX)); //速度加算
                        }
                    }

                    if (MC >= secondMoveTime)//第2指定時間に到達
                    {
                        MC = 0.0f;//カウントリセット

                        switch (ReversFlag)//反転フラグ反転
                        {
                            case true:
                                ReversFlag = false;
                                break;
                            case false:
                                ReversFlag = true;
                                break;
                        }
                    }


                }
                else if (this.gameObject.GetComponent<States>().GetMoveType() == 4)//移動タイプ4の場合
                {

                    if (MC <= farstMoveTime)//第一指定時間に到達する前
                    {

                        RB.velocity = Speed;//速度加算

                    }
                    else//第一指定時間に到達後、第二指定時間到達まで
                    {

                        RB.velocity = Speed2;//速度加算
                    }

                    if (MC >= secondMoveTime)//第2指定時間に到達
                    {
                        MC = 0.0f;//カウントリセット
                    }
                }
                else
                {
                    MC = 0.0f;//カウントリセット
                }

            }
            else if (moveFlag == false)//フラグがfalseなら動かない
            {
                RB.velocity = new Vector3(0, 0, 0);//停止
                walk.SetTrigger("Stop");
            }

        }
       
        if(moveFlag==true)
        {
            walk.SetTrigger("Walk");
        }

    }

    //移動フラグセット関数
    //引数（bool型　true or false）
    public void setMoveFlag(bool F)
    {
        moveFlag = F;
    }

}