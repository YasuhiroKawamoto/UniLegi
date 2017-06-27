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
    float delta;


    float alpha = 0;

	// Use this for initialization
	void Start () {
        alpha = 0;
    }
	
	// Update is called once per frame
	void Update () {

        alpha += delta;


        if (sprOrImg == SpriteOrImage.SpriteRenderer)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Sin(alpha) + 1);
        }
        if (sprOrImg == SpriteOrImage.Image)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Sin(alpha) + 1);
        }
        if (sprOrImg == SpriteOrImage.Text)
        {
            gameObject.GetComponent<Text>().color = new Color(1, 1, 1, Mathf.Sin(alpha) + 1);
        }


    }
}
