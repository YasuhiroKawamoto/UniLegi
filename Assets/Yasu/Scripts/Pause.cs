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
        }
        else
        {
            Time.timeScale = manager.GetSpd();
            text.text = "||";
        }
    }
}
