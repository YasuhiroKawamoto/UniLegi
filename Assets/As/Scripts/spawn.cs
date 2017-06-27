using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{

   

    [SerializeField]
    private GameManager manager;


    [SerializeField]
    GameObject effect;
    [SerializeField]
    GameObject arrow;
    GameObject arrowObj;
    //デンジャーゾーン
    GameObject DanjarZone;

    GameObject putEffect;

    ////矢印が出ているかのフラグ
    //private bool m_arrowFlag;
    //触ったフラグ
    private int m_flag;
    //タッチ
    Touch touch;
    //タッチ座標
    private Vector2 m_worldPoint;
    //生成するオブジェクト
    public GameObject AsPrefab;

    private float posx;
    private float posy;

    //[SerializeField]
    private int waitTime = 30;
    private bool IsWaiting;
    private bool IsSummons;//召喚中か？

    static bool IsPut;//触っているか？



    private Vector3 savePos;
    Vector3 tmpPos;

    //マス
    GameObject[] grids;
    Grid currentGrid;

    // Use this for initialization
    void Start()
    {

        m_flag = 0;

        //putEffect = Resources.Load<GameObject>("Prefabs/Put");

        tmpPos = (this.transform.position);
        tmpPos.z = -1;

        savePos = tmpPos;

        IsWaiting = false;

        IsSummons = false;
        //m_arrowFlag = false;
        IsPut = false;
        DanjarZone = GameObject.Find("SpriteDengerZone");

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale != 0)
        {

            if (CanInstantiate())
            {
                // 色を明るく
                SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

                Vector3 pos = transform.position;
                pos.y = pos.y + 1.0f;

                //if (m_arrowFlag == false)
                //{
                //    arrowObj = Instantiate(arrow);
                //    arrowObj.transform.position = pos;
                //    m_arrowFlag = true;
                //}
                sr.color = Color.white;
                if (Input.touchCount > 0)
                {
                 

                    touch = Input.GetTouch(0);

                    m_worldPoint = Camera.main.ScreenToWorldPoint(touch.position);


                    //タッチ開始時
                    if (touch.phase == TouchPhase.Began)
                    {
                        // マスオブジェクトを検出
                        grids = GameObject.FindGameObjectsWithTag("Grid");


                        //タッチをした位置にオブジェクト判定
                        RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                        if (hit)
                        {
                            //putEffect = Resources.Load<GameObject>("Prefabs/Put");
                            //putEffect.transform.position = new Vector3(-300, -300, -300);


                            if (hit.collider.gameObject == this.gameObject)
                            {
                                PlayerControl.canUnion = false;
                                m_flag = 1;
                            }

                        }

                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        // マスオブジェクトを検出
                        grids = GameObject.FindGameObjectsWithTag("Grid");


                        //タッチをした位置にオブジェクト判定
                        RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                        if (hit)
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                IsPut = true;
                                foreach (GameObject grid in grids)
                                {
                                    Vector3 gridPos = grid.transform.position;
                                    Vector3 gridScl = grid.transform.localScale;

                                    // タッチ座標がマスの中
                                    if (m_worldPoint.x > gridPos.x - gridScl.x / 2 && m_worldPoint.y > gridPos.y - gridScl.y / 2 &&
                                        m_worldPoint.x < gridPos.x + gridScl.x / 2 && m_worldPoint.y < gridPos.y + gridScl.y / 2)
                                    {
                                        // エフェクト
                                        //putEffect.transform.position = gridPos;
                                    }
                                }
                            }

                        }

                    }
                    //離したとき
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        //putEffect.transform.position = new Vector3(-300, -300, -300);
                        IsPut = false;
                        //タッチをした位置にオブジェクト判定
                        RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);
                        if (hit)
                        {
                            if (hit.collider.gameObject == this.gameObject)
                            {
                                
                                if (m_worldPoint.y < DanjarZone.transform.position.y)
                                {
                                    //元いた位置に戻る
                                    transform.position = savePos;
                                    PlayerControl.canUnion = true;

                                    m_flag = 0;
                                    //if (m_arrowFlag == true)
                                    //{
                                    //    Destroy(arrowObj.gameObject);
                                    //}
                                    //m_arrowFlag = false;
                                }
                                else
                                {
                                    bool inGrid = false;
                                    Vector3 appearPos = Vector3.zero;
                                    bool isExisting = false;

                                    foreach (GameObject grid in grids)
                                    {
                                        Vector3 gridPos = grid.transform.position;
                                        Vector3 gridScl = grid.transform.localScale;
                                        isExisting = grid.GetComponent<Grid>().GetIsExisting();

                                        // タッチ座標がマスの中
                                        if (m_worldPoint.x > gridPos.x - gridScl.x / 2 && m_worldPoint.y > gridPos.y - gridScl.y / 2 &&
                                            m_worldPoint.x < gridPos.x + gridScl.x / 2 && m_worldPoint.y < gridPos.y + gridScl.y / 2)
                                        {
                                            if (isExisting == false)
                                            {
                                                Debug.Log("マスの中");
                                                inGrid = true;
                                                appearPos = gridPos;
                                                currentGrid = grid.GetComponent<Grid>();
                                                int row = grid.GetComponent<Grid>().GetRow();
                                            }
                                        }
                                    }


                                    if (IsSummons == false && inGrid)//召喚中ッで無ければ　かつ　マスの中
                                    {
                                        AsPrefab.transform.position = appearPos;

                                        effect.transform.position = AsPrefab.transform.position;//エフェクト位置設定



                                        if (effect != null)//エフェクトスロットに設定してある場合
                                        {
                                            Singleton<SoundManager>.instance.playSE("se002");
                                            Instantiate(effect);//エフェクト生成


                                        }
                                        IsSummons = true;//召喚状態にする
                                        IsWaiting = true;//エフェクト待機状態にする


                                        // コスト消費
                                        manager.SpendCost(AsPrefab.gameObject.GetComponent<States>().getCost());
                                    }
                                    //元いた位置に戻る
                                    transform.position = savePos;
                                    PlayerControl.canUnion = true;

                                    m_flag = 0;

                                    //m_arrowFlag = false;

                                }
                            }
                        }
                    }
                }
                else
                {
                    m_flag = 0;
                    transform.position = savePos;
                }
            }
            else
            {
                //元いた位置に戻る
                transform.position = savePos;

                m_flag = 0;

                // 色を暗く
                SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
                //if (m_arrowFlag == true)
                //{
                //    Destroy(arrowObj.gameObject);
                //}
                sr.color = Color.gray;
                //フラグを折る
                //m_arrowFlag = false;
            }

            //1の時追従する
            if (m_flag == 1)
            {
                //タッチしている座標に追従する
                transform.position = m_worldPoint;

                tmpPos = transform.position;
                tmpPos.z = -1;
                transform.position = tmpPos;

            }

            //デンジャーゾーンよりY軸が大きくなったら矢印を消す
            if (this.transform.position.y > DanjarZone.transform.position.y)
            { 
                        //    Destroy(arrowObj.gameObject);
            }



            // ユニット生成
            if (IsWaiting)
            {
                waitTime--;

            }
            if (waitTime < 0)
            {
                Instantiate(AsPrefab);//ユニット生成


                IsWaiting = false;//エフェクト待機解除
                waitTime = 30;//待機カウントリセット
                IsSummons = false;//召喚待機状態に設定
            }
        }


    }




    bool CanInstantiate()
    {
        //// ユニット数が5以上の時生成不可
        //if (manager.GetNum() >= 5)
        //{
        //    return false;
        //}

        // コストが足りていないとき生成不可
        if (manager.GetCost() < AsPrefab.gameObject.GetComponent<States>().getCost())
        {
            return false;
        }

        return true;
    }



    public bool getIsPut()
    {
        return IsPut;
    }
}


