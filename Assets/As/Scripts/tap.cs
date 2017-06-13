using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{

    //弾が撃てる
    private bool m_canShot = true;
    //動いてるかどうか
    private bool m_moveFlag;
    //スケールの倍数
    const float rate = 1.5f;
    //範囲
    Vector3 zone;
    //元の大きさを保存
    private Vector3 m_saveScale;
    //反転してるかどうか
    private bool m_Invert = false;
    //タッチ
    Touch touch;
    //タッチ座標
    private Vector2 m_worldPoint;
    //オブジェクトを触っている時間
    private float m_Cnt;
    //魔王の手
    [SerializeField]
    GameObject Hand;

    [SerializeField]
    GameObject state;

    GameObject canvas;

    GameObject objCursor;
    GameObject objState;
    States unitStates;
    StateUI stateUI;


    // Use this for initialization
    void Start()
    {
        zone = GameObject.Find("SpriteDengerZone").transform.position;
        canvas = GameObject.Find("Canvas");
        unitStates = gameObject.GetComponent<States>();
        stateUI = state.GetComponent<StateUI>();

        //フラグは立たない
        m_moveFlag = false;
        //大きさを保存
        m_saveScale = this.transform.localScale;

        
    }

    // Update is called once per frame
    void Update()
    {
        //タッチされたら
        if (Input.touchCount > 0)
        {

            touch = Input.GetTouch(0);

            m_worldPoint = Camera.main.ScreenToWorldPoint(touch.position);

            //タッチ開始時
            if (touch.phase == TouchPhase.Began)
            {
                //タッチをした位置にオブジェクト判定
                RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                //オブジェクトにあたっていたら
                if (hit)
                {
                    if (Input.touchCount == 1)
                    {
                        if (hit.collider.gameObject == this.gameObject)
                        {
                            StateUI.HP = unitStates.getHp();
                            StateUI.ATK = unitStates.getAttack();

                            Singleton<SoundManager>.instance.playSE("se007");
                            //移動フラグをtrueにし弾を打てないようにする
                            PlayerControl.canUnion = false;

                            m_moveFlag = true;
                            this.gameObject.tag = "HavingPlayer";
                            //this.gameObject.layer = 12;
                            m_canShot = false;

                            objCursor = Instantiate(Hand, transform.position, transform.rotation);


                            objState = Instantiate(state, canvas.transform);

                        }
                    }
                }

            }
            //離したとき
            else if (touch.phase == TouchPhase.Ended && m_moveFlag)
            {

                //タッチをした位置にオブジェクト判定
                RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);
                if (hit.collider.gameObject == this.gameObject && hit.collider.gameObject.tag != "isPinched")
                {
                    if (hit)
                    {
                        PlayerControl.canUnion = true;
                        this.gameObject.tag = "Player";
                        //this.gameObject.layer = 0;
                        if (m_Cnt < 0.5f)

                            this.transform.localScale = m_saveScale;
                        m_moveFlag = false;

                        m_canShot = true;

                        m_Cnt = 0.0f;


                    }
                }
            }

            //オブジェクトが触っている時
            if (m_moveFlag == true)
            {
                if (m_worldPoint.y > zone.y + 0.2f)
                {
                    //タッチしている座標に追従する
                    transform.position = m_worldPoint;


                    Vector3 pos = gameObject.transform.position;
                    pos.y = pos.y + 1.0f;
                    objState.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);

                    //撃てない
                    m_canShot = false;

                    m_Cnt += 0.1f;

                }
                else
                {
                    this.transform.localScale = m_saveScale;
                    m_moveFlag = false;


                    m_canShot = true;


                    m_Cnt = 0.0f;

                    Destroy(objCursor);
                }

            }
        }
        else
        {
            Destroy(objState);
        }
    }

    public bool getShot()
    {
        return m_canShot;
    }

    //動いてるかどうかのフラグを渡す
    public bool getMove()
    {
        return m_moveFlag;
    }
    //反転してるかどうかのフラグ
    public bool getInverd()
    {
        return m_Invert;
    }

    public void setInverd(bool flag)
    {
        m_Invert = flag;
    }

}