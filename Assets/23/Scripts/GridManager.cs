using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// グリッド全体での共通データ
/// </summary>
public class GridManager : MonoBehaviour {


    
    private float WaitCnt;//点滅用のカウント
    [SerializeField]
    private float MaxCnt;//カウント最大値


    GameObject FlontLine;

	// Use this for initialization
	void Start () {
        FlontLine = GameObject.Find("FrontLine");
        WaitCnt = MaxCnt;

    }
	
	// Update is called once per frame
	void Update ()
    {
           WaitCnt -= Time.deltaTime;

            if (WaitCnt <= 0)
            {
                WaitCnt = MaxCnt;//リセット
            }




        foreach (Transform child in gameObject.transform)
        {
            child.GetComponentInChildren<Grid>().ChangeGridState(FlontLine);
        }

        //transform.GetComponentInChildren<Grid>().ChangeGridState(FlontLine);

       
    }

    public float getCnt()
    {
        return WaitCnt;
    }



}
