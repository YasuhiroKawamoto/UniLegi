using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum TAP_STATE
{
    NONE,
    SINGLE,
    DOUBLE,
    MULTI = 10,
}
public class GetTouch : MonoBehaviour
{

    [SerializeField]
    GameObject new_unit;

    [SerializeField]
    private GameObject Area;

    [SerializeField]
    private SpriteRenderer hand1;

    [SerializeField]
    private SpriteRenderer hand2;


    private Vector3 touch_pos1;
    private Vector3 touch_pos2;
    private Vector3 start_pos;
    private Vector2 start_size;
    private bool isTrigger;
    private bool canInstantiate;


    [SerializeField]
    private GameManager manager;


    private int pinch_num;


    // タップ状態
    private TAP_STATE tap_state;

    // Use this for initialization
    void Start()
    {

        start_size = new Vector2(0.0f, 0.0f);
        canInstantiate = true; 
        isTrigger = false;

        pinch_num = 0;

        tap_state = TAP_STATE.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        // タップ数などを判定
        TapSearch();

        GameObject[] unions = GameObject.FindGameObjectsWithTag("isPinched");


        switch (tap_state)
        {
           
            case TAP_STATE.DOUBLE:
                // 手　スプライト




                // コライダの大きさを設定
                Vector2 size = new Vector2(Mathf.Abs(touch_pos1.x - touch_pos2.x), 1.0f);

                if(start_size.x > 0.0f )
                {
                    size.x = Mathf.Clamp(size.x, 0.0f, start_size.x);
                }

                Area.transform.localScale = size;



                // コライダの位置を設定
                float min_x = Mathf.Min(touch_pos1.x, touch_pos2.x);
                float min_y = Mathf.Min(touch_pos1.y, touch_pos2.y);

                Vector2 pos = new Vector2(min_x + size.x / 2.0f, min_y);


                // コライダ生成
                if (isTrigger == false)
                {
                    if (size.x > 1)
                    {
                        hand1.transform.position = new Vector3(pos.x - size.x / 2.0f, pos.y, 0);
                        hand2.transform.position = new Vector3(pos.x + size.x / 2.0f, pos.y, 0);
                    }

                    start_size = size;
                    start_pos = pos;
                    Area.transform.position = pos;

                    isTrigger = true;
                }


                // 合体
                if (Area.gameObject.tag == "Pinched" && Area.transform.localScale.x < 1)
                {
                    Area.transform.position = new Vector3(-300, -300, -300);
                    // hand1.transform.position = new Vector3(-300, -300, -300);
                    // hand2.transform.position = new Vector3(-300, -300, -300);

                    if (canInstantiate)
                    {
                        // 2体以上はさんだ時
                        if (pinch_num >= 2)
                        {
                            // コストが足りているとき
                            if (manager.GetCost() >= new_unit.gameObject.GetComponent<States>().getCost())
                            {
                                new_unit.transform.position = new Vector3(pos.x + size.x / 2.0f, pos.y, 0.0f);
                                new_unit.transform.localScale = new Vector3(1, 1, 1);

                                // 新ユニットを生成
                                Instantiate(new_unit);

                                // コスト消費
                                manager.SpendCost(new_unit.gameObject.GetComponent<States>().getCost());

                                canInstantiate = false;
                            }
                        }
                    }
                }

                // ピンチイン判定
                if (size.x <= 1)
                {
                    Area.gameObject.tag = "Pinched";
                }

                break;
            case TAP_STATE.SINGLE:
                // ゲームオブジェクト「魔王の指」を動的に生成
            case TAP_STATE.NONE:
                Area.gameObject.tag = "Collider";
                Area.transform.position = new Vector3(-300, -300, -300);
                hand1.transform.position = new Vector3(-300, -300, -300);
                hand2.transform.position = new Vector3(-300, -300, -300);
                isTrigger = false;
                canInstantiate = true;
                unions = GameObject.FindGameObjectsWithTag("isPinched");

                foreach (GameObject union in unions)
                {
                    union.gameObject.tag = "Player";
                }
                pinch_num = 0;
                start_size = new Vector2(0.0f, 0.0f);
                break;
        }



        pinch_num = 0;
        // 判定エリア内のユニット数をカウント
        foreach (GameObject union in unions)
        {
            pinch_num++;
        }

        if (Area.gameObject.tag == "Pinched")
        {
            if(pinch_num>1 && canInstantiate == false)
            {
                foreach (GameObject union in unions)
                {
                    // 既存のユニットを破壊
                    Destroy(union);
                }
            }
            
        }

    }



    void TapSearch()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {

                touch_pos1.z = 0;
                touch_pos2.z = 0;



                //1点タップの時(トリガー)
                if (Input.touchCount == 1)
                {
                    touch_pos1 = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                    tap_state = TAP_STATE.SINGLE;
                }

                // 2点タップの時
                else if (Input.touchCount == 2)
                {
                    touch_pos1 = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    Debug.Log(touch_pos1);

                    touch_pos2 = Camera.main.ScreenToWorldPoint(Input.touches[1].position);
                    tap_state = TAP_STATE.DOUBLE;
                }

                // 3点以上タップされている
                else if (Input.touchCount >= 3)
                {
                    tap_state = TAP_STATE.MULTI;
                }

            }
        }
        else
        {
            tap_state = TAP_STATE.NONE;
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Pinched" && this.gameObject.tag == "isPinched")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
}

