using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum TAP_STATE
{
    NONE,
    SINGLE,
    DOUBLE,
    MULTI = 10,
}
public class PlayerControl : MonoBehaviour
{

    public GameObject newUnit;

    private GameObject InstantiateUnit;

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

    private float overload;
    const float MAX_OVERLOAD = 3.0f;


    private float delay;
    private bool isWaiting;

    private bool isCreated;

    int tmpId = 0;

    static public bool canUnion;

    // タップ状態
    private TAP_STATE tap_state;

    bool superUnion;

    Sprite deleteSpr;

    //マス
    GameObject[] grids;
    Grid currentGrid;

    // スーパーユニット召喚中
    bool isSummon;
    //

   
    //暗くするための画像
    //画面が明るいか暗いか
    bool blackFlag = false;

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
        InstantiateUnit = newUnit;
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
        // dangerzone 以下 グリッド以上　 は出現しない
        if ((touch_pos1.y <= manager.GetDangerZone().gameObject.transform.position.y || touch_pos2.y <= manager.GetDangerZone().gameObject.transform.position.y)
             && touch_pos1.y >= 0.5 || touch_pos2.y >= 0.5f)
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
                    Vector2 size = new Vector2(Mathf.Abs(touch_pos1.x - touch_pos2.x), 0.4f);

                    if (start_size.x > 0.0f)
                    {
                        size.x = Mathf.Clamp(size.x, 0.0f, start_size.x);
                    }

                    Area.transform.localScale = size - new Vector2(0.6f, 0);



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
                            Vector3 hpos1 = new Vector3(pos.x - size.x / 2.0f, pos.y, 0);
                            Vector3 hpos2 = new Vector3(pos.x + size.x / 2.0f, pos.y, 0);

                            hpos1.x = Mathf.Clamp(hpos1.x, -2.7f, 2.7f);
                            hpos2.x = Mathf.Clamp(hpos2.x, -2.7f, 2.7f);

                            // マスオブジェクトを検出
                            grids = GameObject.FindGameObjectsWithTag("Grid");

                            hand1.transform.position = hpos1;
                            hand2.transform.position = hpos2;
                            Prediction.transform.position = new Vector3(-300, -300, -300);

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
                    else if (pinch_num >= 2)
                    {
                        if (unionCoolTime <= 0)
                        {
                            // オバロゲージ増大
                            overload += 0.1f;

                            //　でっかい手を出す
                            Vector2 handScale = Lerp(hand1.transform.localScale, new Vector2(-1.5f, 2.0f), 1.5f, TimeStep);
                            Vector2 handScale2 = Lerp(hand2.transform.localScale, new Vector2(1.5f, 2.0f), 1.5f, TimeStep);
                            Vector2 handPos1 = Lerp(hand1.transform.position, new Vector2(-3.5f, -1.0f), 1.5f, TimeStep);
                            Vector2 handPos2 = Lerp(hand2.transform.position, new Vector2(3.5f, -1.0f), 1.5f, TimeStep);

                            hand1.transform.localScale = handScale;
                            hand2.transform.localScale = handScale2;
                            hand1.transform.position = handPos1;
                            hand2.transform.position = handPos2;

                            int totalATK = 0;
                            int totalHP = 0;
                            int diff = 0;
                            if (overload >= MAX_OVERLOAD)
                            {
                                foreach (GameObject union in unions)
                                {
                                    union.tag = "Player";
                                }
                                unions = GameObject.FindGameObjectsWithTag("Player");

                                foreach (GameObject union in unions)
                                {

                                    // 全ユニットの値を抽出
                                    totalATK += union.GetComponent<States>().getAttack();
                                    totalHP += union.GetComponent<States>().getHp();

                                    // 全ユニットをはさまれた状態に
                                    union.tag = "isPinched";
                                }
                                diff = totalHP / 7 - totalATK;
                            }

                            InstantiateUnit = newUnit;

                            if (overload >= MAX_OVERLOAD)
                            {
                                // 移動済み
                                Area.transform.position = new Vector3(0, 0, 0);

                                // 生成ユニットの差し替え
                                if (diff < -5)
                                {
                                    InstantiateUnit = Resources.Load<GameObject>("Prefabs/SuperDragon");
                                }
                                else if (diff >= -5 && diff <= -1)
                                {
                                    InstantiateUnit = Resources.Load<GameObject>("Prefabs/SuperLilith");
                                }
                                else if (diff > -1 && diff <= 1)
                                {
                                    InstantiateUnit = Resources.Load<GameObject>("Prefabs/SuperParadin");
                                }
                                else if (diff <= 5 && diff > 1)
                                {
                                    InstantiateUnit = Resources.Load<GameObject>("Prefabs/SuperLich");
                                }
                                else
                                {
                                    InstantiateUnit = Resources.Load<GameObject>("Prefabs/SuperKraken");
                                }



                                // エフェクトの差し替え
                            }
                            // 予測の差し替え
                            sprPre.sprite = InstantiateUnit.GetComponent<SpriteRenderer>().sprite;
                        }
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

                                if (overload >= MAX_OVERLOAD)
                                {
                                    // 真ん中に行く
                                    Area.transform.position = new Vector3(0, 0, 0);

                                    // すーぱーがったい
                                    superUnion = true;
                                    isSummon = true;


                                    tmpId = newUnit.GetComponent<States>().GetTypeId();

                                    // 合体ユニット設定
                                    InstantiateUnit.transform.position = new Vector3(0.0f, start_pos.y + size.y / 2.0f, 0.0f);
                                    InstantiateUnit.transform.localScale = new Vector3(1, 1, 1);
                                    //newUnit.tag = "isPinched";

                                    // エフェクト設定
                                    effect.transform.position = new Vector3(0.0f, start_pos.y + size.y / 2.0f, 0.0f);
                                    effect.transform.localScale = new Vector3(1, 1, 1);

                                    // エフェクト発生
                                    Instantiate(effect);
                                    Singleton<SoundManager>.instance.playSE("se002");

                                    delay = 50;

                                    // 手をどける
                                    isWaiting = true;


                                    canInstantiate = false;

                                    Vector3 appearPos = Vector3.zero;
                                    bool isExisting = false;

                                    foreach (GameObject grid in grids)
                                    {
                                        Vector3 gridPos = grid.transform.position + new Vector3(0.5f, 0.5f, 0.0f);
                                        Vector3 gridScl = grid.transform.localScale;
                                        isExisting = grid.GetComponent<Grid>().GetIsExisting();

                                        // ユニットがマスの中
                                        if (InstantiateUnit.transform.position.x > gridPos.x - gridScl.x / 2 && InstantiateUnit.transform.position.y > gridPos.y - gridScl.y / 2 &&
                                             InstantiateUnit.transform.position.x < gridPos.x + gridScl.x / 2 && InstantiateUnit.transform.position.y < gridPos.y + gridScl.y / 2)
                                        {
                                            if (isExisting == false)
                                            {
                                                Debug.Log("マスの中");
                                                InstantiateUnit.transform.position = gridPos;
                                                currentGrid = grid.GetComponent<Grid>();
                                                int row = grid.GetComponent<Grid>().GetRow();
                                            }
                                        }
                                    }
                                }

                                // 通常合体
                                else
                                {

                                    tmpId = InstantiateUnit.GetComponent<States>().GetTypeId();

                                    // 合体ユニット設定
                                    InstantiateUnit.transform.position = new Vector3(start_pos.x + size.x / 2.0f, start_pos.y + size.y / 2.0f, 0.0f);
                                    InstantiateUnit.transform.localScale = new Vector3(1, 1, 1);
                                    //newUnit.tag = "isPinched";

                                    // エフェクト設定
                                    effect.transform.position = new Vector3(start_pos.x + size.x / 2.0f, start_pos.y + size.y / 2.0f, 0.0f);
                                    effect.transform.localScale = new Vector3(1, 1, 1);

                                    // エフェクト発生
                                    Instantiate(effect);
                                    Singleton<SoundManager>.instance.playSE("se002");

                                    delay = 50;
                                    pinch_num = 0;

                                    // 手をどける
                                    isWaiting = true;

                                    canInstantiate = false;

                                    Vector3 appearPos = Vector3.zero;
                                    bool isExisting = false;

                                    foreach (GameObject grid in grids)
                                    {
                                        Vector3 gridPos = grid.transform.position;
                                        Vector3 gridScl = grid.transform.localScale;
                                        isExisting = grid.GetComponent<Grid>().GetIsExisting();

                                        // ユニットがマスの中
                                        if (InstantiateUnit.transform.position.x > gridPos.x - gridScl.x / 2 && InstantiateUnit.transform.position.y > gridPos.y - gridScl.y / 2 &&
                                             InstantiateUnit.transform.position.x < gridPos.x + gridScl.x / 2 && InstantiateUnit.transform.position.y < gridPos.y + gridScl.y / 2)
                                        {
                                            if (isExisting == false)
                                            {
                                                Debug.Log("マスの中");
                                                InstantiateUnit.transform.position = gridPos;
                                                currentGrid = grid.GetComponent<Grid>();
                                                int row = grid.GetComponent<Grid>().GetRow();
                                            }
                                        }
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

                    hand1.transform.localScale = new Vector2(-1, 1);
                    hand2.transform.localScale = new Vector2(1, 1);

                    overload = 0;
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
            canUnion_ = false; canUnion = false;   

             delay--;
            if (delay < 20)
            {
                hand1.transform.position = new Vector3(-300, -300, -300);
                hand2.transform.position = new Vector3(-300, -300, -300);
                Prediction.transform.position = new Vector3(-300, -300, -300);


            }
        }

        if (delay < 0)
        {
            delay = 50;
            // ユニット生成
            Instantiate(InstantiateUnit);
            InstantiateUnit.tag = "Player";
            //
            isCreated = true;
            isWaiting = false;
            blackFlag = true;
            canUnion_ = true;

        }

        if (unionCoolTime > 0 && !isSummon)
        {
            unionCoolTime -= Time.deltaTime * 2;

        }

        if (unionCoolTime <= COOL_TIME && isSummon)
        {
            unionCoolTime += Time.deltaTime * 15;
        }

        else if (unionCoolTime > COOL_TIME)
        {
            isSummon = false;
            unionCoolTime = COOL_TIME;
        }
        if (unionCoolTime <= 0)
        {
            unionCoolTime = 0;
            blackFlag = false;
        }

        pinch_num = 0;

        // 判定エリア内のユニット数をカウント
        foreach (GameObject union in unions)
        {
            pinch_num++;
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
                pinch_num = 0;
            }

            // スーパー合体
            if (superUnion)
            {
                foreach (GameObject union in unions)
                {
                    // 既存のユニットを全て破壊
                    Destroy(union);

                }

                pinch_num = 0;
                superUnion = false;
                overload = 0;

            }

            else if (pinch_num == 1)
            {
                foreach (GameObject union in unions)
                {
                    Vector3 pos = union.transform.position;
                    GameObject dead = Resources.Load<GameObject>("Prefabs/Destroy");
                    dead.transform.position = pos;
                    Instantiate(dead);


                    // 既存のユニットを破壊
                    Destroy(union);


                }
                pinch_num = 0;
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


    // 線形補間用関数
    static float Lerp(float startNum, float targetNum, float t, Func<float, float> v)
    {
        float retNum = 0.0f;


        retNum = (1 - v(t)) * startNum + v(t) * targetNum;

        return retNum;
    }

    static Vector2 Lerp(Vector2 startNum, Vector2 targetNum, float t, Func<float, float> v)
    {
        Vector2 retNum = Vector2.zero;


        retNum = (1 - v(t)) * startNum + v(t) * targetNum;

        return retNum;
    }


    static float TimeStep(float stepTime)
    {
        float m_currentTime = 0;
        if (m_currentTime < stepTime)
        {
            m_currentTime += 0.1f;
        }

        return m_currentTime;
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

    public float GetOverload()
    {
        return overload;
    }

    public float GetOverMAX()
    {
        return MAX_OVERLOAD;
    }
    public bool getIsCreated()
    {
        return isCreated;
    }
    public bool IsUnion()
    {
        return isWaiting;
    }

    public bool IsSummon()
    {
        return isSummon;
    }

    public bool getFlag()
    {
        return blackFlag;
    }

    public int GetPinchNum()
    {
        return pinch_num;
    }
}