using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour {

    private float m_nextTime;

    [SerializeField]
    private float interval;

    // Use this for initialization
    void Start () {

        StartCoroutine("Blink");

    }

    // Update is called once per frame
        IEnumerator Blink() {

        Renderer rend = GetComponent<Renderer>();

        while (true)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(interval);
        }

    }
}
