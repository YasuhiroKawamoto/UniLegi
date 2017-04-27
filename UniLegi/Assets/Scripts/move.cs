using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    //直進移動のスクリプト

    //移動速度
    private Vector2 Speed = new Vector2(0.0f, -0.01f);

    private int moveFlag = 0;
  
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

      

        //現在地取得
        Vector2 Positon = transform.position;

        //速度加算
        Positon += Speed;

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