﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Union : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collider")
        {
            this.gameObject.tag = "isPinched";
        }
    }
}
