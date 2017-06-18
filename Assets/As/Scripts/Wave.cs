using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    SpriteRenderer sprite;

    float m_alha = 0.0f;


    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(255, 255, 255, m_alha);
    }
	
	// Update is called once per frame
	void Update () {

        m_alha += Time.deltaTime;

        if (m_alha < 1.0f)
        {
            sprite.color = new Color(255, 255, 255, m_alha);
        }
       else if(m_alha > 2.0f)
        {
            Destroy(gameObject);
        }

    }
}
