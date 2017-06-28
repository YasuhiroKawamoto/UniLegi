using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Flash : MonoBehaviour {

    enum SpriteOrImage{
        Image,
        SpriteRenderer,
        Text

    }
    [SerializeField]
    SpriteOrImage sprOrImg;

    [SerializeField]
    float MaxAlpha;

    [SerializeField]
    float delta;


    float alpha = 0;

	// Use this for initialization
	void Start () {
        alpha = 0;
    }
	
	// Update is called once per frame
	void Update () {

        alpha += delta*Time.deltaTime   ;


        if (sprOrImg == SpriteOrImage.SpriteRenderer)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Sin(alpha)* MaxAlpha + 0.5f);
        }
        if (sprOrImg == SpriteOrImage.Image)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Sin(alpha) * MaxAlpha + 0.5f);
        }
        if (sprOrImg == SpriteOrImage.Text)
        {
            gameObject.GetComponent<Text>().color = new Color(1, 1, 1, Mathf.Sin(alpha) * MaxAlpha + 0.5f);
        }


    }
}
