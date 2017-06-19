using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlontLineMove : MonoBehaviour
{

    Rigidbody2D RB;//リジットボディ

    private Vector2 Speed = new Vector2(0.0f, -0.5f);//速度１

    private bool moveFlag = true;

    private bool backFlag = true;

    private float changeCnt;

    // Use this for initialization
    void Start()
    {
        changeCnt = 0;
        RB = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (backFlag == true)
        {
            changeCnt += Time.deltaTime;

            if (changeCnt > 0.5f)
            {
                RB.velocity = new Vector2(0, 3);//上方に戻る
            }
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
            moveFlag = false;
            Debug.Log("前線停止");
        }



        if (col.gameObject.tag == "Enemy")
        {
            if (backFlag)
            {
                backFlag = false;
                moveFlag = true;
            }

        }

    }

    void OnTriggerStay2D(Collider2D col)
    {


        if (col.gameObject.tag == "Enemy")
        {
            backFlag = false;
        }

        if (col.gameObject.tag == "Player")
        {

            moveFlag = false;

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("上昇");
            changeCnt = 0;
            backFlag = true;
        }

        if (col.gameObject.tag == "Player")
        {
            Debug.Log("停止");
            moveFlag = true;
        }

    }


}