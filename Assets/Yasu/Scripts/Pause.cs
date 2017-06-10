using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Pause : MonoBehaviour
{
    private bool isPause;
    [SerializeField]
    GameManager manager;
    [SerializeField]
    GameObject gameObject;
    [SerializeField]
    Canvas canvas;

    private bool m_flag = false; 

    [SerializeField]
    Text text;
    // Use this for initialization
    void Start()
    {
        isPause = false;
 

        // Buttonクリック時、OnClickメソッドを呼び出す
        GetComponent<Button>().onClick.AddListener(OnTap);
    }

    // Update is called once per frame
    void OnTap()
    {
        isPause = !isPause;
    }

    void Update()
    {
        
        if (isPause)
        {
            Time.timeScale = 0;
            text.text = "▶";

            if (m_flag == false)
            {
                Instantiate(gameObject, canvas.transform);
                m_flag = true;
            }
        }
        else
        {
            Time.timeScale = manager.GetSpd();
            text.text = "||";
            m_flag = false;
            Destroy(gameObject);
        }
    }
}
