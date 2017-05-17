using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataManager : MonoBehaviour {

    [SerializeField]
    int stageNumber;
	// Use this for initialization
	void Start () {
        // オブジェクトを破棄しない
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetStage(int stage)
    {
        stageNumber = stage;
    }
}
