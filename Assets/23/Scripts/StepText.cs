using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StepText : MonoBehaviour {

	//オブジェクト
    [SerializeField]
    private GameObject step;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Text text1;
    [SerializeField]
    private Text text2;
    [SerializeField]
    private Text text3;
    [SerializeField]
    private Text text4;
    [SerializeField]
    private Text text5;
    [SerializeField]
    private Text text6;

    [SerializeField]
    private GameObject TB;

    [SerializeField]
    private GameObject SB;



    Text tmpText;

    GameObject tmpObj;

    GameObject TutorialManager;
    

    private int m_cnt = 0;

    private float m_time = 0;

    private bool m_flag = true;

    private int CurrentStep = 0;

   

    //最終ステップの数
    [SerializeField]
    private int m_lastStep;

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
            if(tmpText != null)
            Destroy(tmpText.gameObject);
            Destroy(tmpObj);
            //wave数の表示
            if (m_lastStep == m_cnt)
            {

                //ウィンドウの召喚
                tmpObj = Instantiate(step, canvas.transform);
                tmpText = Instantiate(text6, canvas.transform);

                if (TB != null)
                {
                    TB.transform.position = new Vector3(-400, 400, 0);
                    Instantiate(TB,canvas.transform);
                }

                if (SB != null)
                {
                    SB.transform.position = new Vector3(400,400, 0);
                    Instantiate(SB, canvas.transform);
                }



                m_cnt = CurrentStep;
                m_flag = true;
                m_time = 0.0f;
            }
            else if(m_lastStep > m_cnt)
            {
                
                //ウィンドウの召喚
                tmpObj = Instantiate(step, canvas.transform);
                

                switch (CurrentStep)
                {
                    case 1:
                        tmpText = Instantiate(text1, canvas.transform);
                        break;
                    case 2:
                        tmpText = Instantiate(text2, canvas.transform);
                        break;
                    case 3:
                        tmpText = Instantiate(text3, canvas.transform);
                        break;
                    case 4:
                        tmpText = Instantiate(text4, canvas.transform);
                        break;
                    case 5:
                        tmpText = Instantiate(text5, canvas.transform);
                        break;

                }
               
                m_cnt = CurrentStep;
                m_flag = true;
                m_time = 0.0f;
            }
        }

        if (tmpText != null)
        {
            tmpText.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(0, 2.5f, 0));
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
