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

    GameObject FlontLine;

    // Use this for initialization
    void Start () {
        square = gameObject.transform.FindChild("grid2").gameObject;

        FlontLine = GameObject.Find("FlontLine");

    }
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody2D>().WakeUp();

        if (transform.position.y > FlontLine.transform.position.y - 0.5f)
        {

            isExisting = true;//ユニット配置不可にする

        }
        else {


            if (checkPlayer)
            {
                isExisting = true;
                checkPlayer = false;

            }
            else
            {
                isExisting = false;

            }
        }
      


        if (isExisting)//ユニット配置不可なら
        {
            square.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);//不可視
        }
        else
        {
            square.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);//可視
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checkPlayer = true;
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
