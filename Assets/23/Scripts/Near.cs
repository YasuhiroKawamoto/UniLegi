using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Near : MonoBehaviour {
    //自オブジェクトと最も近いオブジェクトを取得し対応するスクリプト

    Collider2D CO;
    

    private GameObject nearObject;         //最も近いオブジェクト
    private float searchTime = 0;    //経過時間

    // Use this for initialization
    void Start()
    {
        //ユニットのリジッドボディを取得
        CO = this.GetComponent<Collider2D>();
        //最も近かったオブジェクトを取得
        nearObject = serchTag(gameObject, "Enemy");
    }

    // Update is called once per frame
    void Update()
    {

        //経過時間を取得
        searchTime += Time.deltaTime;

        if (searchTime >= 0.2f)
        {
            //最も近かったオブジェクトを取得
            nearObject = serchTag(gameObject, "Enemy");

            //経過時間を初期化
            searchTime = 0;
        }

        if (nearObject != null)//最も近いオブジェクトがnullじゃなかったら
        {
            //最も近いオブジェクト距離とのを取得して反転判定
            if (nearObject.transform.position.y < this.transform.position.y)
            {
                this.gameObject.GetComponent<Tap>().setInverd(true);
            }
            else
            {
                this.gameObject.GetComponent<Tap>().setInverd(false);
            }

        }
        else {
            //近くにオブジェクトが無ければ正面判定にする
            this.gameObject.GetComponent<Tap>().setInverd(false);
        }
    }

    //指定されたタグの中で最も近いものを取得
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
       
        GameObject target = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            if ((Mathf.Abs(obs.transform.position.x - nowObj.transform.position.x) < CO.bounds.size.x))
            {
                tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

                //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
                //一時変数に距離を格納
                if (nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;

                    target = obs;
                }
            }

        }
        //最も近かったオブジェクトを返す
      
        return target;
    }
}