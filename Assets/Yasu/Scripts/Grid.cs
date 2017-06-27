using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    // マスにユニとがいるかどうか
    bool isExisting;

    bool checkPlayer;

    // 何行目か
    [SerializeField]
    int row;

    GameObject square;//内側マス
    GameObject square2;//外側マス

    GameObject FlontLine;
    GameObject putEffect;
    GameObject generator;
    
    
    // Use this for initialization
    void Start () {
        square = gameObject.transform.FindChild("grid2").gameObject;
        square2 = gameObject.transform.FindChild("grid").gameObject;
     
        generator = GameObject.Find("Generator");
        putEffect = Instantiate(Resources.Load<GameObject>("Prefabs/Put"),transform);
        putEffect.transform.position = gameObject.transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {

    }



    public void ChangeGridState(GameObject obj)
    {
        if (transform.position.y > obj.transform.position.y)//フロントラインより上にある場合
        {
            isExisting = true;//ユニット配置不可にする
            square2.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);//不可視（外枠）
            square.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);//不可視（外枠）
            putEffect.transform.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            square2.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);//可視(外枠)

            if (checkPlayer)//プレイヤーが乗っていたら
            {
                square.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);//不可視（中枠）
                isExisting = true;//ユニット配置不可
            }
            else
            {
                square.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);//可視（中枠）
                isExisting = false;//ユニット配置可能
            }

            if (!isExisting)//ユニット配置不可でなければ
            {
                if (generator.GetComponent<spawn>().getIsPut() == true)//generatorに触れている場合
                {
                    putEffect.transform.GetComponent<SpriteRenderer>().enabled = true;
                    //square.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, transform.parent.GetComponent<GridManager>().getCnt());//点滅   
                }
                else
                {
                    putEffect.transform.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checkPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"||collision.gameObject.tag == "HavingPlayer" || collision.gameObject.tag == "isPinched")
        {
            checkPlayer = false;
        }
    }


    public bool GetIsExisting()
    {
        return isExisting;
    }

    public  void SetIsExisting(bool flag)
    {
        isExisting = flag;
    }

    public int GetRow()
    {
        return row;
    }
}
