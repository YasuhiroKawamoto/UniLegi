using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideLine : MonoBehaviour {
    //合体可能時に隣接ユニットを繋ぐライン生成スクリプト

    Collider2D CO;

    [SerializeField]
    GameObject Line;//ユニット間に引くライン

    GameObject LineObj = null;//ライン生成用オブジェ

    private GameObject nearObject;         //最も近いオブジェクト
    private float searchTime = 0;    //経過時間

    // Use this for initialization
    void Start()
    {
        //ユニットのリジッドボディを取得
        CO = this.GetComponent<Collider2D>();
        //最も近かったオブジェクトを取得
        nearObject = serchTag(gameObject, "Player");
    }

    // Update is called once per frame
    void Update()
    {
        //経過時間を取得
        searchTime += Time.deltaTime;

        if (searchTime >= 0.2f)
        {
            //最も近かったオブジェクトを取得
            nearObject = serchTag(gameObject, "Player");

            //経過時間を初期化
            searchTime = 0;
        }

        if (nearObject != null)//最も近いオブジェクトがnullじゃなかったら
        {
            if(LineObj == null)//ライン未精製時
            //最も近いオブジェクト距離とのを取得して距離分の長さのラインを生成
            LineObj.transform.position = (this.gameObject.transform.position - nearObject.transform.position);
            //ラインの長さを決定
            LineObj.transform.localScale = new Vector3(1,(this.gameObject.transform.position.x - nearObject.transform.position.x),1);
            //ライン生成
            LineObj = Instantiate(Line);
        }
        else
        {
            //近くに対象ユニットがない場合
            if (LineObj != null)//ラインが残存する場合
            {
                //ライン消去
                Destroy(LineObj);
            }
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
            //自身と取得したオブジェクトの距離を取得（y座標の差がユニットの縦幅より小さければ）
            if ((Mathf.Abs(obs.transform.position.y - nowObj.transform.position.y) < CO.bounds.size.y))
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
