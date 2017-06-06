using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StepText : MonoBehaviour {

	//オブジェクト
    [SerializeField]
    private Text step;
    [SerializeField]
    private Canvas canvas;

    GameObject TutorialManager;
    public GameObject gameObject1;
    public GameObject gameObject2;

    private int m_cnt = 0;

    private float m_time = 0;

    private bool m_flag = true;

    private int CurrentStep = 0;

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
        TutorialManager = GameObject.Find("TutorialManager");
    }
	
	//  is called once per frame
	void Update () {

        //現在のステップ取得
        CurrentStep = TutorialManager.GetComponent<TutorialManager>().getCurrentStep()+1;
        

        if (m_cnt != CurrentStep)
        {
            //wave数の表示
            if (m_lastWave == m_cnt)
            {
                step.text = "LASTSTEP";
                Instantiate(step, canvas.transform);
                Instantiate(gameObject1, transform.position + m_pos, transform.rotation);
                Instantiate(gameObject2, transform.position - m_pos, transform.rotation);
                m_cnt = CurrentStep;
                m_flag = true;
                m_time = 0.0f;
            }
            else if(m_lastWave > m_cnt)
            {
                step.text = "STEP " + CurrentStep.ToString();
                Instantiate(step, canvas.transform);
                m_cnt = CurrentStep;
                m_flag = true;
                m_time = 0.0f;
            }
        }

        m_time += Time.deltaTime;

        if (m_flag == true)
        {
            if (m_time > 2.0f)
            {
                m_flag = false;
            }
        }

    }

}
