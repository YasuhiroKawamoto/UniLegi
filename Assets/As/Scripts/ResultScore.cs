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

    [SerializeField]
    private Text HiScore;


    

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

        if (Singleton<Score>.instance.GetHiScoreInfinity() < m_killEnemy)
        {
            Singleton<Score>.instance.SaveScoreInfinity(m_killEnemy);
            HiScore.text = "HISCORE!!";
        }
        else
        {
            HiScore.text = "";
        }


        if (m_killEnemy < 30)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank F";
        }
        else if (m_killEnemy >= 30 && m_killEnemy <= 49)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank E";
        }
        else if (m_killEnemy >= 50 && m_killEnemy <= 79)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank D";
        }
        else if (m_killEnemy >= 80 && m_killEnemy <= 129)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank C";
        }
        else if (m_killEnemy >= 130 && m_killEnemy <= 149)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank B";
        }
        else if (m_killEnemy >= 150 && m_killEnemy <= 199)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank A";
        }
        else if (m_killEnemy >= 200 && m_killEnemy <= 299)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank S";
        }
        else if (m_killEnemy >= 300 && m_killEnemy <= 500)
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank SS";
        }
        else
        {
            result.GetComponent<Text>().text = " KILL COUNT\n" + m_killEnemy.ToString("F0") + "\n " + "Rank SSS";
        }
    }




    void ResultNormal()
    {
        //最速タイムよりも早ければ
        switch (Singleton<SceneData>.instance.getStageNumber())
        {
            case 1:
                if (Singleton<Score>.instance.GetHiScoreNormal() > m_time)
                {
                    Singleton<Score>.instance.SaveScoreNormal(m_time);
                    HiScore.text = "BESTTIME!!";
                }
                else
                {
                    HiScore.text = "";
                }
                break;
            case 2:

                if (Singleton<Score>.instance.GetHiScoreHard() > m_time)
                {
                    Singleton<Score>.instance.SaveScoreHard(m_time);
                    HiScore.text = "BESTTIME!!";
                }
                else
                {
                    HiScore.text = "";
                }

                break;

        }

        

        if (m_time > 240.0f)
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " + "Rank F";
        }
        else if (m_time > 220.0f)
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " + "Rank E";
        }
        else if (m_time > 210.0f)
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " + "Rank D";
        }
        else if (m_time > 200.0f)
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " + "Rank C";
        }
        else if (m_time > 185.0f)
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " + "Rank B";
        }
        else if (m_time > 150.0f)
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " + "Rank A";
        }
        else
        {
            result.GetComponent<Text>().text = " CLEAR TIME\n" + m_time.ToString("F2") + "\n " + "Rank S";
        }



    }

}
