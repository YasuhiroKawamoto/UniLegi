using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    
    private float WaitCnt;
    [SerializeField]
    private float MaxCnt;


   static bool IsWaiting = false;

	// Use this for initialization
	void Start () {

        WaitCnt = MaxCnt;

    }
	
	// Update is called once per frame
	void Update () {

        if (IsWaiting)
        {
            WaitCnt -= Time.deltaTime;

            if (WaitCnt <= 0)
            {
                IsWaiting = false;
            }
        }

        if (!IsWaiting)
        {
            WaitCnt = MaxCnt;    
        }
		
	}

    public float getCnt()
    {

        return WaitCnt;
    }


    public void setIsWaiting(bool f)
    {
        IsWaiting = f;
    }

    public bool getIsWaiting()
    {
        return IsWaiting;
    }


}
