using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyNum : MonoBehaviour
{


    //全体の数
    private int num = 0;
    //カウントする数
    private int cnt = 0;
    //セーブする数
    private int m_save = 0;
    private int save = 0;
    //一気に倒したときの数
    private int allDestroy = 0;
    //タイムカウント
    private float time = 0.0f;
    //数えるフラグ
    private bool m_Flag = false;
    //タイム
    [SerializeField]
    private float m_limit = 0.0f;

    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Text text;

    GameObject gameObject;

    // Use this for initialization
    void Start () {

        gameObject = (GameObject)Resources.Load("Prefabs/KillBack");

    }
	
	// Update is called once per frame
	void Update () {

        num = Singleton<SceneData>.instance.getEnemyCnt();

        if (m_Flag == false)
        {
            save = num;
            cnt = save - m_save;

            if(0 < cnt)
            {
                m_Flag = true;
            }
        }
        else if(m_Flag == true)
        {
            time += Time.deltaTime ;
            if(time <= 2.0f)
            {
                cnt = num - m_save;
            }
            else
            {
                //撃破数が10以上なら
                if (cnt > 9)
                {
                    text.text = cnt.ToString() + "Kills";
                    Instantiate(text, canvas.transform);
                    Instantiate(gameObject);
                }
               
                time = 0.0f;
                m_save = num;
                m_Flag = false;
            }
        }

	}
}
