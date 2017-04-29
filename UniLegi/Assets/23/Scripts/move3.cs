
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move3 : MonoBehaviour {

    //直進移動後少し後退を繰り返す


    //移動速度
    [SerializeField]
    private Vector2 Speed = new Vector2(0.0f, -0.01f);
    [SerializeField]
    private Vector2 Speed2 = new Vector2(-0.01f, 0.01f);
    
    private Vector2 reverseX = new Vector2(-1.0f, 1.0f);

    private Vector2 reverseY = new Vector2(1.0f, -1.0f);

    private Vector2 Positon;


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
        Positon = transform.position;

        if (MC <= 50)
        {
            //速度加算
            Positon += Speed;

        }
        else
        {
            if (moveFlag == 0)
            {
                Positon += Speed2;
            }
            else
            {
                Positon +=  (Vector2.Scale(Speed2,reverseX) );
            }
        }

        if (MC >= 80)
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


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "DangerZone")
        {
            Debug.Log("hit");

        }


        if (col.gameObject.tag == "OutZone")
        {
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
}
