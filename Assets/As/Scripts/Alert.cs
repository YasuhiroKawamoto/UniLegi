using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour {


    //オブジェクト
    [SerializeField]
    private Text wave;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameManager gameManager;

    Emitter emitter;
    GameManager m_gameManager;

    public GameObject gameObject1;
    public GameObject gameObject2;

    GameObject start;
    GameObject Next;
    GameObject Last;

    private int m_cnt = 0;

    private int m_cnt2 = 1;

    private float m_time = 0;

    private bool m_flag = true;

    private bool m_lastFlag = false;

    [SerializeField]
    private Vector3 m_pos;
    [SerializeField]
    private Vector3 m_rot;

    //ラストウェーブの数
    private int m_lastWave;

    private bool m_startFlag = true;

    // Use this for initialization
    void Start () {

        //Emitterのコンポーネント
        emitter = gameObject.GetComponent<Emitter>();
        m_gameManager = gameManager.GetComponent<GameManager>();
        m_lastWave = emitter.GetWaveSize();//最後のウェーブ取得

        start = (GameObject)Resources.Load("Prefabs/start");
        Next = (GameObject)Resources.Load("Prefabs/next");
        Last = (GameObject)Resources.Load("Prefabs/warning");

    }
	
	//  is called once per frame
	void Update ()
    {


        int CurrentWave = emitter.GetCurrentWave()+1;

        if (m_gameManager.IsLose() == false)
        {
            Debug.Log(CurrentWave);
            if (m_cnt != CurrentWave)
            {
                Debug.Log(m_lastWave);
                //wave数の表示
                if (m_lastWave == CurrentWave && m_lastFlag==false)
                {
                   
                    Instantiate(Last);
                    m_startFlag = false;
                    m_lastFlag = true;
                    Singleton<SoundManager>.instance.playSE("SE010");//ボスアラート音再生
                    //Instantiate(gameObject1, transform.position + m_pos, transform.rotation);
                    //Instantiate(gameObject2, transform.position - m_pos, transform.rotation);
                    m_flag = true;
                    m_time = 0.0f;
                }
                else if (m_cnt != CurrentWave && CurrentWave > 1 && CurrentWave < m_lastWave)
                {
                    //wave.text = "WAVE " + CurrentWave.ToString();
                   
                    Instantiate(Next);
                    m_startFlag = false;
                    m_cnt = CurrentWave;
                    m_flag = true;
                    m_time = 0.0f;
                }
                else if (CurrentWave != m_cnt  && m_cnt == 0)
                {
                    Instantiate(start);
                    m_startFlag = false;
                    m_cnt = CurrentWave;

                    m_time = 0.0f;
                }

            }
        }
        m_time += Time.deltaTime;

       

        if (m_flag==true)
        {
            if (m_time > 2.0f)
            {
                m_flag = false;
            }
        }

    }

}
