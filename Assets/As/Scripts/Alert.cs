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
    //private GameManager gameManajer;

    Emitter emitter;
    GameManager m_gameManajer;

    public GameObject gameObject1;
    public GameObject gameObject2;

    private int m_cnt = 0;

    private float m_time = 0;

    private bool m_flag = true;

    [SerializeField]
    private Vector3 m_pos;
    [SerializeField]
    private Vector3 m_rot;

    //ラストウェーブの数
    [SerializeField]
    private int m_lastWave;

    // Use this for initialization
    void Start () {

        //Emitterのコンポーネント
        emitter = gameObject.GetComponent<Emitter>();
        //m_gameManajer = gameManajer.GetComponent<GameManager>();
    }
	
	//  is called once per frame
	void Update () {


        int CurrentWave = emitter.GetCurrentWave() + 1;

        //if (m_gameManajer.GetOverFlag() == true)
        //{ 

            if (m_cnt != CurrentWave)
            {
                //wave数の表示
                if (m_lastWave == m_cnt)
                {
                    wave.text = "LASTWAVE";
                    Instantiate(wave, canvas.transform);
                    Instantiate(gameObject1, transform.position + m_pos, transform.rotation);
                    Instantiate(gameObject2, transform.position - m_pos, transform.rotation);
                    m_cnt = CurrentWave;
                    m_flag = true;
                    m_time = 0.0f;
                }
                else if (m_lastWave > m_cnt)
                {
                    wave.text = "WAVE " + CurrentWave.ToString();
                    Instantiate(wave, canvas.transform);
                    m_cnt = CurrentWave;
                    m_flag = true;
                    m_time = 0.0f;
                }
            }
        //}
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
