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
    private Vector3 touch_pos1;
    private Vector3 touch_pos2;
    private Vector3 start_pos;
    private Vector2 start_size;
    private bool isTrigger;

    [SerializeField]
    private GameObject Area;

    [SerializeField]
    private SpriteRenderer hand1;

    [SerializeField]
    private SpriteRenderer hand2;


    // タップ状態
    private TAP_STATE tap_state;
    LineRenderer line;

    private void Awake()
    {
        // ボックスコライダーをアタッチ
        //Area.AddComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start()
    {

        isTrigger = false;

        tap_state = TAP_STATE.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        // タップ数などを判定
        TapSearch();



        switch (tap_state)
        {
            case TAP_STATE.SINGLE:
                // ゲームオブジェクト「魔王の指」を動的に生成

                break;
            case TAP_STATE.DOUBLE:
                // 手　スプライト




                // コライダの大きさを設定
                Vector2 size = new Vector2(Mathf.Abs(touch_pos1.x - touch_pos2.x), 1.0f);
                Area.transform.localScale = size;

                Debug.Log(size);

                // コライダの位置を設定
                float min_x = Mathf.Min(touch_pos1.x, touch_pos2.x);
                float min_y = Mathf.Min(touch_pos1.y, touch_pos2.y);

                Vector2 pos = new Vector2(min_x + size.x / 2.0f, min_y);


                // コライダ生成
                if (isTrigger == false)
                {
                    hand1.transform.position = new Vector3(touch_pos1.x, pos.y, 0);
                    hand2.transform.position = new Vector3(touch_pos1.x + size.x, pos.y, 0);
                    start_size = size;
                    start_pos = pos;
                    Area.transform.position = pos;

                    isTrigger = true;
                }

                if (Area.gameObject.tag == "Pinched" && size.x < 1)
                {
                    Area.transform.position = new Vector3(-300, -300, -300);
                    hand1.transform.position = new Vector3(-300, -300, -300);
                    hand2.transform.position = new Vector3(-300, -300, -300);
                }

                // ピンチイン判定
                if (size.x <= 1)
                {
                    Area.gameObject.tag = "Pinched";

                }

                break;
            case TAP_STATE.NONE:
                Area.transform.position = new Vector3(-300, -300, -300);
                Area.gameObject.tag = "Untagged";
                hand1.transform.position = new Vector3(-300, -300, -300);
                hand2.transform.position = new Vector3(-300, -300, -300);
                isTrigger = false;

                GameObject[] unions = GameObject.FindGameObjectsWithTag("isPinched");

                foreach (GameObject union in unions)
                {
                    this.gameObject.tag = "Untagged";
                }


                break;
        }

        if (Area.gameObject.tag == "Pinched")
        {
            GameObject[] unions = GameObject.FindGameObjectsWithTag("isPinched");

            foreach(GameObject union in unions)
            {
                Destroy(union);

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

