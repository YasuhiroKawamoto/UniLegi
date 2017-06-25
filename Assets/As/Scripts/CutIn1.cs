using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutIn1 : MonoBehaviour {

    Image image;
    private float Scale = 0.0f;
    private float num = 0.0f;

	// Use this for initialization
	void Start () {

        image = GetComponent<Image>();
       
	}
	
	// Update is called once per frame
	void Update () {

        num += Time.deltaTime;

        image.fillAmount = num;

	}
}
