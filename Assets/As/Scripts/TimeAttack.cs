using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttack : MonoBehaviour {


    //タイムカウント
    private float m_timeCount = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //スタートしてからの秒数を代入
        m_timeCount += Time.deltaTime;
        GetComponent<Text>().text = m_timeCount.ToString("F2");
        

	}


    void OnDestroy()
    {

        Singleton<SceneData>.instance.setTime(m_timeCount);

    }
}
