using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnamyGageText : MonoBehaviour {

    GameObject UIManager;

	// Use this for initialization
	void Start () {
        UIManager = GameObject.Find("UIManager");
	}
	
	// Update is called once per frame
	void Update () {


        if (Singleton<SceneData>.instance.getStageNumber() == 3)
        {
            GetComponent<Text>().text = "ENEMY" + (Singleton<SceneData>.instance.getEnemyCnt() / 20).ToString() + "LEVEL";
        }
        else
        {
            GetComponent<Text>().text = "ENEMY" + UIManager.GetComponent<UIManager>().getEnemyMax().ToString()
                                        + "/" + Singleton<SceneData>.instance.getEnemyCnt().ToString();
        }

    }
}
