using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Twitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Buttonクリック時、OnClickメソッドを呼び出す
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Application.OpenURL("https://twitter.com/intent/tweet?text=" + "お疲れ様です。" + "&hashtags=#UniLegi");
    }

}
