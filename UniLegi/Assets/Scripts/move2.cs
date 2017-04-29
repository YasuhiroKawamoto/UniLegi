using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2 : MonoBehaviour {

    //直進移動後左右に振るその後直進


    //移動速度
    private Vector2 Speed = new Vector2(0.0f, -0.01f);

    private Vector2 Speed2 = new Vector2(0.01f, 0.0f);

    private int moveFlag = 0;

    private int MC = 0;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        MC++;

        //現在地取得
        Vector2 Positon = transform.position;

        if (MC <= 50)
        {
            //速度加算
            Positon += Speed;

        }
        else
        {
            //速度加算
            if (moveFlag == 0)
            {
                Positon += Speed2;
            }
            else
            {
                Positon -= Speed2;
            }
        }

        if (MC >= 100)
        {
            MC = 0;
            switch (moveFlag)
            { 
                case 0:
                    moveFlag = 1;
                    break;
                case 1:
                    moveFlag = 0;
                    break;
            }
        }


      

        //現在位置に速度加算後位置を代入
        transform.position = Positon;



    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "DangerZone")
        {
            Debug.Log("hit");

        }

       



    }

    void OnCollisionStay(Collision col)
    {


        if (col.gameObject.tag == "DangerZone")
        {

            Debug.Log("stey");
            Speed = new Vector2(0, 0);
        }


    }

    void OnCollisionExit(Collision col)
    {


        if (col.gameObject.tag == "DangerZone")
        {
            Debug.Log("out");

            Speed = new Vector2(0, -0.01f);
        }


        


    }
}
