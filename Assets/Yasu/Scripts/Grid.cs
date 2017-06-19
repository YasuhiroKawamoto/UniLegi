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

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if(checkPlayer)
        {
            isExisting = true;
            checkPlayer = false;
        }
        else
        {
            isExisting = false;
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
