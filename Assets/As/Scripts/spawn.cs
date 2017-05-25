using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{

    [SerializeField]
    private float zone;

    [SerializeField]
    private GameManager manager;

    [SerializeField]
    GameObject effect;

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
    private Vector3 savePos;
    Vector3 tmpPos;

    // Use this for initialization
    void Start()
    {

        m_flag = 0;


        tmpPos = (this.transform.position);
        tmpPos.z = -1;

        savePos = tmpPos;

        IsWaiting = false;

        IsSummons = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (CanInstantiate())
        {
            // 色を明るく
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

            sr.color = Color.white;
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                m_worldPoint = Camera.main.ScreenToWorldPoint(touch.position);


                //タッチ開始時
                if (touch.phase == TouchPhase.Began)
                {

                    //タッチをした位置にオブジェクト判定
                    RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                    if (hit)
                    {
                        if (hit.collider.gameObject == this.gameObject)
                        {
                            m_flag = 1;
                        }

                    }

                }
                //離したとき
                else if (touch.phase == TouchPhase.Ended)
                {
                    //タッチをした位置にオブジェクト判定
                    RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                    if (hit)
                    {
                        if (hit.collider.gameObject == this.gameObject)
                        {
                            if (m_worldPoint.y < zone)
                            {
                                //元いた位置に戻る
                                transform.position = savePos;

                                m_flag = 0;
                            }
                            else
                            {



                                if (IsSummons == false)//召喚中ッで無ければ
                                {
                                    AsPrefab.transform.position = m_worldPoint;

                                    effect.transform.position = AsPrefab.transform.position;//エフェクト位置設定



                                    if (effect != null)//エフェクトスロットに設定してある場合
                                    {

                                        Instantiate(effect);//エフェクト生成


                                    }
                                    IsSummons = true;//召喚状態にする
                                    IsWaiting = true;//エフェクト待機状態にする


                                    // コスト消費
                                    manager.SpendCost(AsPrefab.gameObject.GetComponent<States>().getCost());
                                }
                                //元いた位置に戻る
                                transform.position = savePos;

                                m_flag = 0;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            //元いた位置に戻る
            transform.position = savePos;

            m_flag = 0;

            // 色を暗く
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

            sr.color = Color.gray;

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




    bool CanInstantiate()
    {
        // ユニット数が5以上の時生成不可
        if (manager.GetNum() >= 5)
        {
            return false;
        }

        // コストが足りていないとき生成不可
        if (manager.GetCost() < AsPrefab.gameObject.GetComponent<States>().getCost())
        {
            return false;
        }

        return true;
    }
}


