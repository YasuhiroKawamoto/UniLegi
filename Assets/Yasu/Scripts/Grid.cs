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
    GameObject square2;//内側マス

    GameObject FlontLine;

    GameObject putEffect;

    GameObject putEffectReal = null;

    GameObject generator;

    bool IsWaiting;

    static float cnt; 

    // Use this for initialization
    void Start () {
        square = gameObject.transform.FindChild("grid2").gameObject;
        square2 = gameObject.transform.FindChild("grid").gameObject;
        FlontLine = GameObject.Find("FrontLine");
        generator = GameObject.Find("Generator");
        putEffect = Resources.Load<GameObject>("Prefabs/Put");
        IsWaiting = false;
    }
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody2D>().WakeUp();

        if (transform.position.y > FlontLine.transform.position.y )
        {

            isExisting = true;//ユニット配置不可にする
            square2.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);//不可視

        }
        else {
            square2.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);//可視

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
            if (generator.GetComponent<spawn>().getIsPut() == true)
            {
                if (this.transform.parent.GetComponentInParent<GridManager>().getIsWaiting() == false)
                {
                   
                    this.transform.parent.GetComponentInParent<GridManager>().setIsWaiting(true);
                    //putEffectReal = Instantiate(putEffect);
                    //putEffectReal.transform.position = gameObject.transform.position;  
                }
                else
                {

                    square.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, this.transform.parent.GetComponentInParent<GridManager>().getCnt());//可視
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
