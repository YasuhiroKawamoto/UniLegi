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





            if (moveFlag == true)//フラグがtrueの時のみ動く
            {

                walk.SetTrigger("Walk");

                MC += Time.deltaTime;//経過時間取得

                WaitEffectTime += Time.deltaTime;//経過時間取得


                if (this.gameObject.GetComponent<States>().GetMoveType() == 1)//移動タイプ１の場合
                {
                    //速度加算
                    RB.velocity = Speed;


                   
                }

                

            }
            else if (moveFlag == false)//フラグがfalseなら動かない
            {
                RB.velocity = new Vector3(0, 0, 0);//停止
                walk.SetTrigger("Stop");

            }

          
        }
    }
    //移動フラグセット関数
    //引数（bool型　true or false）
    public void setMoveFlag(bool F)
    {
        moveFlag = F;
    }

}