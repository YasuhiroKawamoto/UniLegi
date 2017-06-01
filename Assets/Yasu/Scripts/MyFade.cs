using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyFade : MonoBehaviour
{

    Image image;
    bool initflag = true;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.SetAsLastSibling();

    }

    public void SetFade(bool InOut)
    {
        if (InOut == true)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
        }

        if (InOut == false)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }


    public void SetAlpha(float alpha)
    {
        if(image!= null)
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
}