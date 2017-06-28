using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDestory : MonoBehaviour {
    GameObject[] Units;

    // Use this for initialization
    void Start () {
        Units = GameObject.FindGameObjectsWithTag("Player");
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(5, 1);
    }
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<BoxCollider2D>().size.x <=2)
        {
            foreach(GameObject unit in Units)
            {
                Destroy(unit);
            }
        }
	}

}
