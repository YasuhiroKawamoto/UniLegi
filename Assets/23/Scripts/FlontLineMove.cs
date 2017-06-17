using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlontLineMove : MonoBehaviour
{

    Rigidbody2D RB;//リジットボディ

    private Vector2 Speed = new Vector2(0.0f, -0.1f);//速度１

    private bool moveFlag = true;

    private bool backFlag = true;

    // Use this for initialization
    void Start()
    {
        RB = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (backFlag == true)
        {
            RB.velocity = new Vector2(0, 4);//上方に戻る
        }
        else
        {
            if (moveFlag == true)
            {
                RB.velocity = Speed;//加工
            }
            else
            {
                RB.velocity = new Vector2(0, 0);//停止
            }

        }


        

       
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            backFlag = false;
            moveFlag = false;
            Debug.Log("前線停止");
        }


      


    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            moveFlag = false;
            Debug.Log("前線停止");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            moveFlag = true;
            Debug.Log("可動");
        }


       

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (backFlag)
            {
                backFlag = false;
            }

        }

    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (backFlag)
            {
                backFlag = false;
            }

        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (backFlag == false)
            {
                backFlag = true;
            }
        }

    }


}