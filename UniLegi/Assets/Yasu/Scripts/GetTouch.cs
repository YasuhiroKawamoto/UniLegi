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
    private bool isTrigger;

    [SerializeField]
    private GameObject Area;
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
                Debug.Log("hayakukaihatusitai");
                // キャンバスの子にする

                break;
            case TAP_STATE.DOUBLE:
                // ゲームオブジェクト「魔王の手」を動的に生成

                // キャンバスの子にする

                // 空のゲームオブジェクトを生成
                
                // コライダの大きさを設定
                Vector2 size = new Vector2(Mathf.Abs(touch_pos1.x - touch_pos2.x)/5.0f, 1.0f);
                Area.transform.localScale = size;


                // コライダの位置を設定
                float min_x = Mathf.Min(touch_pos1.x, touch_pos2.x);
                float min_y = Mathf.Min(touch_pos1.y, touch_pos2.y);

                Vector2 pos = new Vector2(min_x+ size.x*2.5f, min_y);

                Area.transform.position = pos;

                // コライダ生成
                if (isTrigger == false)
                {
                    Area.transform.parent = transform;
                    isTrigger = true;
                }
                break;
            case TAP_STATE.NONE:
                isTrigger = false;
                Area.transform.position = new Vector3(-300, -300, -300);
                
                break;
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
                if (tap_state != TAP_STATE.SINGLE && Input.touchCount == 1)
                {
                    touch_pos1 = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                    tap_state = TAP_STATE.SINGLE;
                }

                // 2点タップの時
                else if (tap_state != TAP_STATE.DOUBLE && Input.touchCount == 2)
                {
                    touch_pos1 = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                    touch_pos2 = Camera.main.ScreenToWorldPoint(Input.touches[1].position);
                    tap_state = TAP_STATE.DOUBLE;
                }

                // 3点以上タップされている
                else if (tap_state != TAP_STATE.MULTI && Input.touchCount >= 3)
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
}


