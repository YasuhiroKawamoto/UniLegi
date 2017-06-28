using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ScoreReset : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        // Buttonクリック時、OnClickメソッドを呼び出す
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Singleton<Score>.instance.ResetScore();
        Debug.Log("スコアリセット");
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
