using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public bool isHit;
	// Use this for initialization
	void Start ()
    {
        isHit = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isHit = true;
        }
    }
}
