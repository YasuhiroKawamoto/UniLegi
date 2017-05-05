using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    private bool isPause;

    // Use this for initialization
    void Start () {
        isPause = false;

    }

    // Update is called once per frame
    void Update () {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isPause = !isPause;
            }
        }

        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
