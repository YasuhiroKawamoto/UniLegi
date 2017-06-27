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

        GetComponent<Text>().text ="ENEMY"+ UIManager.GetComponent<UIManager>().getEnemyMax().ToString()
                                     + "/" + Singleton<SceneData>.instance.getEnemyCnt().ToString();

    }
}
