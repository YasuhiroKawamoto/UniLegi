using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour {

    //タイム
    [SerializeField]
    private float m_time = 0.0f;

    [SerializeField]
    private Text result;

	// Use this for initialization
	void Start () {

        m_time = Singleton<SceneData>.instance.getTime();


        if (m_time <= 120)
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " +  "Rank A" ;
        }
        else if (m_time >= 121 && m_time <= 180)
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " + "   Rank B";
        }
        else if (m_time >= 181 && m_time <= 240)
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " + "   Rank C";
        }

    }
	
	// Update is called once per frame
	void Update () {

       

       


	}
}
