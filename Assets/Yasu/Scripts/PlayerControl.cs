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
public class PlayerControl : MonoBehaviour
{

    [SerializeField]
    public GameObject newUnit;

    [SerializeField]
    public GameObject Prediction;
    GameObject predictionUnit;
    GameObject predictionOption;


    [SerializeField]
    GameObject effect;

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
    private int unionCost;
    private float unionCoolTime;
    const int COOL_TIME = 100;


    private float delay;
    private bool isWaiting;

    private bool isCreated;

    int tmpId = 0;

    static public bool canUnion;

    // タップ状態
    private TAP_STATE tap_state;


    Sprite deleteSpr;

    // Use this for initialization
    void Start()
    {

        start_size = new Vector2(0.0f, 0.0f);
        canInstantiate = true;
        isTrigger = false;
        isWaiting = false;
        isCreated = false;
        unionCost = 0;
        pinch_num = 0;
        unionCoolTime = COOL_TIME;

        canUnion = true;
        tap_state = TAP_STATE.NONE;

        predictionUnit = GameObject.Find("Prediction/Unit");
        predictionOption = GameObject.Find("Prediction/Option");

        deleteSpr = Resources.Load<Sprite>("delete");
    }

    // Update is called once per frame
    void Update()
    {
        // タップ数などを判定
        TapSearch();

        GameObject[] unions = GameObject.FindGameObjectsWithTag("isPinched");

        SpriteRenderer sprPre = predictionUnit.GetComponent<SpriteRenderer>();
        SpriteRenderer sprPreOp = predictionOption.GetComponent<SpriteRenderer>();

        bool canUnion_ = canUnion;



            // dangerzone 以下は出現しない
            if (touch_pos1.y <= manager.GetDangerZone().gameObject.transform.position.y || touch_pos2.y <= manager.GetDangerZone().gameObject.transform.position.y)
        {
            canUnion_ = false;
            hand1.transform.position = new Vector3(-300, -300, -300);
            hand2.transform.position = new Vector3(-300, -300, -300);
            Prediction.transform.position = new Vector3(-300, -300, -300);
        }
        else
        {
            canUnion_ = canUnion;
        }


        GameObject effect_ = GameObject.FindGameObjectWithTag("Effect");
        if (effect_ != null)
        {
            canUnion_ = false;
        }

        if (canUnion_)
        {
            isCreated = false;
            switch (tap_state)
            {

                case TAP_STATE.DOUBLE:

                    // コライダの大きさを設定
                    Vector2 size = new Vector2(Mathf.Abs(touch_pos1.x - touch_pos2.x), 1.0f);

                    if (start_size.x > 0.0f)
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
                            // 手が出現
                            hand1.transform.position = new Vector3(pos.x - size.x / 2.0f, pos.y, 0);
                            hand2.transform.position = new Vector3(pos.x + size.x / 2.0f, pos.y, 0);
                        }

                        // ピンチエリア移動
                        start_size = size;
                        start_pos = pos;
                        Area.transform.position = pos;

                        // 予測ユニット表示
                        Vector3 pos2 = pos + new Vector2(0, 1.5f);
                        Prediction.transform.position = pos2;

                        isTrigger = true;
                    }


                    sprPre.sprite = Union.tmpSprite;



                    if (pinch_num == 1)
                    {
                        sprPreOp.sprite = deleteSpr;
                    }

                    else
                    {
                        sprPreOp.sprite = null;

                    }
                    // 合体
                    if (Area.gameObject.tag == "Pinched" && Area.transform.localScale.x < 1.5f)
                    {
                        Area.transform.position = new Vector3(-300, -300, -300);
                        Prediction.transform.position = new Vector3(-300, -300, -300);
                        Union.tmpSprite = null;


                        if (canInstantiate)
                        {
                            // 2体以上はさんだ時
                            if (pinch_num >= 2)
                            {
                                tmpId = newUnit.GetComponent<States>().GetTypeId();
                                // コストが足りているとき
                                if (manager.GetCost() >= newUnit.gameObject.GetComponent<States>().getCost())
                                {
                                    // クールタイムが終了いているとき
                                    if (unionCoolTime <= 0)
                                    {
                                        // 合体ユニット設定
                                        newUnit.transform.position = new Vector3(start_pos.x + size.x / 2.0f, start_pos.y + size.y / 2.0f, 0.0f);
                                        newUnit.transform.localScale = new Vector3(1, 1, 1);
                                        //newUnit.tag = "isPinched";

                                        // エフェクト設定
                                        effect.transform.position = new Vector3(start_pos.x + size.x / 2.0f, start_pos.y + size.y / 2.0f, 0.0f);
                                        effect.transform.localScale = new Vector3(1, 1, 1);

                                        // エフェクト発生
                                        Instantiate(effect);
                                        Singleton<SoundManager>.instance.playSE("se002");
                                        unionCoolTime = COOL_TIME;

                                        delay = 80;

                                        // 手をどける

                                        isWaiting = true;


                                        // コスト消費
                                        manager.SpendCost(unionCost);

                                        canInstantiate = false;
                                    }
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

                // ゲームオブジェクト「魔王の指」を動的に生成
                case TAP_STATE.NONE:
                case TAP_STATE.SINGLE:

                case TAP_STATE.MULTI:
                    Area.gameObject.tag = "Collider";
                    Area.transform.position = new Vector3(-300, -300, -300);
                    hand1.transform.position = new Vector3(-300, -300, -300);
                    hand2.transform.position = new Vector3(-300, -300, -300);
                    Prediction.transform.position = new Vector3(-300, -300, -300);
                    Union.tmpSprite = null;


                    //newUnit.tag = "Player";
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
        }

        // ユニット生成
        if (isWaiting)
        {
            delay--;
            if (delay < 60)
            {
                hand1.transform.position = new Vector3(-300, -300, -300);
                hand2.transform.position = new Vector3(-300, -300, -300);
                
            }
        }
           
        if (delay < 0)
        {
            delay = 80;
            Instantiate(newUnit);
            //
            isCreated = true;
            isWaiting = false;

        }

        if(unionCoolTime > 0)
        {
            unionCoolTime -= Time.deltaTime*5;
            
        }

        pinch_num = 0;
        unionCost = 0;

        // 判定エリア内のユニット数をカウント
        foreach (GameObject union in unions)
        {
            pinch_num++;
            unionCost += union.GetComponent<States>().getCost(); 
        }

        if (Area.gameObject.tag == "Pinched")
        {

            if (pinch_num > 1 && canInstantiate == false)
            {
                foreach (GameObject union in unions)
                {
                    // 既存のユニットを破壊
                    Destroy(union);
                }
            }
            else
            {
               if( pinch_num == 1)
                {
                    foreach (GameObject union in unions)
                    {
                        // 既存のユニットを破壊
                        Destroy(union);
                    }
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

    public float GetUnionCoolTime()
    {
        return unionCoolTime;
    }

    public void SetUnionCoolTime(float num)
    {
        unionCoolTime = num;
    }

    public int GetCoolTime()
    {
        return COOL_TIME;
    }

    public bool getIsCreated()
    {
        return isCreated;
    }
}