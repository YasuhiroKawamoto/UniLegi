using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGageText : MonoBehaviour {

    GameObject UIManager;

	// Use this for initialization
	void Start () {
        UIManager = GameObject.Find("UIManager");
	}
	
	// Update is called once per frame
	void Update () {


        if (Singleton<SceneData>.instance.getStageNumber() == 3)
        {
            GetComponent<Text>().text = "LEVEL "+((Singleton<SceneData>.instance.getEnemyCnt() / 5 + 1).ToString());
        }
        else
        {
            GetComponent<Text>().text = "ENEMY " + Singleton<SceneData>.instance.getEnemyCnt().ToString()
                                        + "/" + UIManager.GetComponent<UIManager>().getEnemyMax().ToString();
        }

    }
}
