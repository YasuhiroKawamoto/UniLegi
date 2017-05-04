using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour {

    //Statesコンポーネント
    States states;

    // Use this for initialization
    void Start ()
    {

        //Statesコンポーネントの取得
        states = GetComponent<States>();

       


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {

            Debug.Log("緋弾のアリア");

            Destroy(col);

        }


    }

    void OnCollisionStay2D(Collision2D col)
    {


     


    }

    void OnCollisionExit2D(Collision2D col)
    {


      

    }



}
