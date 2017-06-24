using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionAttack : MonoBehaviour {

    [SerializeField]
    int atk;
	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<States>().setDamege(atk);
        }
        
    }

}
