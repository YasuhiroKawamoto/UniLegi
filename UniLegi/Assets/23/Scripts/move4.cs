using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move4 : MonoBehaviour {

    //直進移動後少し停止を繰り返す


    //移動速度
    [SerializeField]
    private Vector2 Speed = new Vector2(0.0f, -0.01f);
    [SerializeField]
    private Vector2 Speed2 = new Vector2(0.0f, 0.0f);

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

            Positon += Speed2;
        }

        if (MC >= 80)
        {
            MC = 0;
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
