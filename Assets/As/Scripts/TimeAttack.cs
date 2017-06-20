﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttack : MonoBehaviour {


    //タイムカウント
    private int m_timeCount = 0;

    GameObject[] enemy;
    private int enemyNum;

	// Use this for initialization
	void Start () {
        enemyNum = 0;
	}
	
	// Update is called once per frame
	void Update () {

        enemyNum = Singleton<SceneData>.instance.getEnemyCnt();
        //倒した敵の数を表示
        GetComponent<Text>().text = enemyNum.ToString()+" Kills";
     
	}


    void Check(string tag)
    {

    }

    void OnDestroy()
    {

        Singleton<SceneData>.instance.setTime(m_timeCount);

    }
}
