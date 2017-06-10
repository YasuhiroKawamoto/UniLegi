using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class button : UIBehaviour {

    bool flag;

    [SerializeField]
    string seneName;



	// Use this for initialization
	void Start () {

        flag = false;

        GetComponent<Button>().onClick.AddListener(onClick);

    }

    void onClick()
    {
        flag = true;
    }

    // Update is called once per frame
    void Update () {
		
        if(flag)
        {
            Time.timeScale = 1;
            //タイトルへ移動
            SceneManager.LoadScene(seneName);
        }

	}

   

}
