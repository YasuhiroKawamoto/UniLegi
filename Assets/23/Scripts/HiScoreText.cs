using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScoreText : MonoBehaviour {

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Text>().text = "HISCORE  \n"+Singleton<Score>.instance.GetHiScoreInfinity().ToString()+"  KILL";
		
	}
}
