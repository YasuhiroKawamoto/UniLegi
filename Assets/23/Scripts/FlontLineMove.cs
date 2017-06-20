using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlontLineMove : MonoBehaviour
{
    GameObject Player;

    Rigidbody2D RB;//リジットボディ

    private Vector2 Speed = new Vector2(0.0f, -0.3f);//速度１

    private bool moveFlag = true;

    private bool backFlag = true;

    private float changeCnt;

    private bool IsHitPlayer = false;



    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        changeCnt = 0;
        RB = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().WakeUp();
        //if (Player.GetComponent<PlayerControl>().IsUnion() == false)
        //{

            if (backFlag == true)
            {
                changeCnt += Time.deltaTime;

                if (changeCnt > 0.5f)
                {
                    RB.velocity = new Vector2(0, 3);//上方に戻る
                }


                if (this.gameObject.transform.position.y >= 4.0f)
                {

                    moveFlag = false;
                    backFlag = false;

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
        //}
    
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "isPinched")
        {
            IsHitPlayer = true;//プレイヤー接触
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
            if (IsHitPlayer == false)
            {
                moveFlag = true;
            }
        }

        if (col.gameObject.tag == "Player"|| col.gameObject.tag == "isPinched")
        {
            this.gameObject.transform.position = new Vector3(0, col.gameObject.transform.position.y+0.5f, 1);
            IsHitPlayer = true;
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

        if (col.gameObject.tag == "Player" || col.gameObject.tag == "HavingPlayer" || col.gameObject.tag == "isPinched")
        {
            Debug.Log("停止");
            moveFlag = true;
            IsHitPlayer = false;
        }

    }


}