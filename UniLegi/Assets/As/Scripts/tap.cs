using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tap : MonoBehaviour {

    //Bulletのプレハブ
    public GameObject bullet;
 
    private int m_flag;

    //スケールの倍数
    const float rate = 6;
    //範囲
    
    const int zone=-3;

    //元の大きさを保存
    private Vector3 saveScale;

    //タッチ
    Touch touch;

    //タッチ座標
    private　Vector2 m_worldPoint;

    // Use this for initialization
    IEnumerator Start () {

        //フラグは立たない
        m_flag = 0;
        //大きさを保存
        saveScale = this.transform.localScale;

        //弾の複製
        while(true)
        {
            //弾をプレイヤーと同じ位置に設定
            Instantiate(bullet, transform.position, transform.rotation);
            //何秒か待つ
            yield return new WaitForSeconds(0.3f);
   
        }



	}

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {

            touch = Input.GetTouch(0);

            m_worldPoint = Camera.main.ScreenToWorldPoint(touch.position);

            //タッチ開始時
            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("お");

                //タッチをした位置にオブジェクト判定
                RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                if (hit)
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        Debug.Log("タッチ");
                        m_flag = 1;
                    }

                }

            }
            //else if (touch.phase == TouchPhase.Moved)
            //{

            //    //タッチをした位置にオブジェクト判定
            //    RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

            //    if (m_flag == 1)
            //    {

            //        if (hit)
            //        {

            //            if (hit.collider.gameObject == this.gameObject)
            //            {
            //                Debug.Log("移動");
            //            }

            //        }
            //    }

            //}
            //離したとき
            else if (touch.phase == TouchPhase.Ended)
            {
                //タッチをした位置にオブジェクト判定
                RaycastHit2D hit = Physics2D.Raycast(m_worldPoint, Vector2.zero);

                if (hit)
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        Debug.Log("離した");
                        this.transform.localScale =saveScale;
                        m_flag = 0;
                    }

                }

            }

        }

        //オブジェクトが触っている時
        if (m_flag == 1)
        {
            if (m_worldPoint.y > zone)
            {
                //タッチしている座標に追従する
                transform.position = m_worldPoint;
                //オブジェクトを倍加させる
                this.transform.localScale = new Vector3(rate, rate,1);
            }
        }


    }

    
   
}
