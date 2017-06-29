using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttack : MonoBehaviour {


    //タイムカウント
    private float m_timeCount = 0;

    private int enemyNum;

	// Use this for initialization
	void Start () {
        enemyNum = 0;
	}
	
	// Update is called once per frame
	void Update () {

        m_timeCount += Time.deltaTime;

        enemyNum = Singleton<SceneData>.instance.getEnemyCnt();
        //倒した敵の数を表示
        if (Singleton<SceneData>.instance.getStageNumber() == 3)
        {
            GetComponent<Text>().text = "KILL  " + enemyNum.ToString();
        }
        else
        {
            GetComponent<Text>().text = "TIME  " + m_timeCount.ToString("F2");
        }


     
	}


    void Check(string tag)
    {

    }

    void OnDestroy()
    {

        Singleton<SceneData>.instance.setTime(m_timeCount);

    }
}
