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

    Emitter emitter;

    private int m_cnt = 0;

    private float m_time = 0;

    private bool m_flag = true;

    // Use this for initialization
    void Start () {

        //Emitterのコンポーネント
        emitter = gameObject.GetComponent<Emitter>();

    }
	
	//  is called once per frame
	void Update () {


        int CurrentWave = emitter.GetCurrentWave()+1;

        if (m_cnt != CurrentWave)
        {
            Debug.Log("WAVE");
            //wave数の表示
            wave.text = "WAVE " + CurrentWave.ToString();
            Instantiate(wave, canvas.transform);
            m_cnt = CurrentWave;
            m_flag = true;
            m_time = 0.0f;
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
