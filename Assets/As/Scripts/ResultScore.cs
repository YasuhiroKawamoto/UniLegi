using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour {

    //タイム
    [SerializeField]
    private float m_time;

    //敵撃破数
    [SerializeField]
    private float m_killEnemy;

    [SerializeField]
    private Text result;

    // Use this for initialization
    void Start() {

        m_time = Singleton<SceneData>.instance.getTime();

        m_killEnemy = Singleton<SceneData>.instance.getEnemyCnt();

        if (Singleton<SceneData>.instance.getStageNumber() == 3)//インフィニモードのリザルト処理
        {
            ResultInfinity();
        }
        else//通常モードのリザルト処理
        {
            ResultNormal();
        }

        


        

    }
	
	// Update is called once per frame
	void Update () {

       

	}


    void ResultInfinity()
    {
        if (m_killEnemy < 30)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank F";
        }
        else if (m_killEnemy > 30)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank E";
        }
        else if (m_killEnemy > 50)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank D";
        }
        if (m_killEnemy > 80)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank C";
        }
        if (m_killEnemy >120)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank B";
        }
        if (m_killEnemy > 150)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank A";
        }
        else
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank S";
        }
    }




    void ResultNormal()
    {




    }

}
