using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{


    [SerializeField]
    private float farstMoveTime;

    [SerializeField]
    private float secondMoveTime;

    //移動速度
    [SerializeField]
    private Vector2 Speed = new Vector2(0.0f, -0.03f);
    [SerializeField]
    private Vector2 Speed2 = new Vector2(0.03f, 0.0f);

    private Vector2 reverseX = new Vector2(-1.0f, 1.0f);

    private bool ReversFlag = false;

    private bool moveFlag = true;

    private float MC = 0;

    Rigidbody2D RB;

    // Use this for initialization
    void Start()
    {

        RB = this.gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.GetComponent<States>().GetMoveType() ==  5)
        {
            this.gameObject.GetComponent<States>().SetMoveType(Random.Range(1, 4));
        }


        if (moveFlag == true)//フラグがtrueの時のみ動く
        {
            MC += Time.deltaTime;

            if (this.gameObject.GetComponent<States>().GetMoveType() == 1)
            {
                //速度加算
                RB.velocity = Speed;

            }
            else if (this.gameObject.GetComponent<States>().GetMoveType() == 2)
            {


                if (MC <= farstMoveTime)
                {
                    //速度加算
                    RB.velocity = Speed;
                }
                else
                {
                    if (ReversFlag == false)
                    {
                        RB.velocity = Speed2;
                    }
                    else
                    {
                        RB.velocity = (Vector2.Scale(Speed2, reverseX));
                    }
                }


                if (MC >= secondMoveTime)
                {
                    MC = 0;
                    switch (ReversFlag)
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
            else if (this.gameObject.GetComponent<States>().GetMoveType() == 3)
            {





                if (MC <= farstMoveTime)
                {
                    //速度加算
                    RB.velocity = Speed;

                }
                else
                {
                    if (ReversFlag == false)
                    {
                        RB.velocity = Speed2;
                    }
                    else
                    {
                        RB.velocity = (Vector2.Scale(Speed2, reverseX));
                    }
                }

                if (MC >= secondMoveTime)
                {
                    MC = 0;

                    switch (ReversFlag)
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
            else if (this.gameObject.GetComponent<States>().GetMoveType() == 4)
            {
                if (MC <= farstMoveTime)
                {
                    //速度加算
                    RB.velocity = Speed;

                }
                else
                {
                    //速度加算
                    RB.velocity = Speed2;
                }

                if (MC >= secondMoveTime)
                {
                    MC = 0;
                }
            }
            else
            {
                MC = 0.0f;
            }

        }
        else if(moveFlag == false)//フラグがfalseなら動かない
        {
            RB.velocity = new Vector3(0,0,0);
        }
       
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "DangerZone")
        {
            Debug.Log("hit");

        }

    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "DangerZone")
        {

            Debug.Log("stey");
            Speed = new Vector2(0, 0);
        }


    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "DangerZone")
        {
            Debug.Log("out");

            Speed = new Vector2(0, -0.01f);
        }
    }

    public void setMoveFlag(bool F)
    {
        moveFlag = F;
    }

}