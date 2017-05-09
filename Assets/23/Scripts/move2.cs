using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2 : MonoBehaviour {

    //直進移動後左右に振るその後直進


    //移動速度
    [SerializeField]
    private Vector2 Speed = new Vector2(0.0f, -0.01f);
    [SerializeField]
    private Vector2 Speed2 = new Vector2(0.01f, 0.0f);

    private Vector2 reverseX = new Vector2(-1.0f, 1.0f);

    private bool ReversFlag = false;

    private bool moveFlag = true;

    private int MC = 0;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //現在地取得
        Vector2 Positon = transform.position;

        if (moveFlag == true)
        {
            MC++;

        


       

            if (MC <= 50)
            {
                //速度加算
                Positon += Speed;

            }
            else
            {
                //速度加算
                if (ReversFlag == false)
                {
                    Positon += Speed2;
                }
                else
                {
                    Positon += (Vector2.Scale(Speed2, reverseX));
                }
            }

            if (MC >= 100)
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




            //現在位置に速度加算後位置を代入
            transform.position = Positon;

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
