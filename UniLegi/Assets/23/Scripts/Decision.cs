using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour {

    //Statesコンポーネント
    States states;

    [SerializeField]
    private GameManager manager;
   


   
    // Use this for initialization
    void Start ()
    {

        //Statesコンポーネントの取得
        states = GetComponent<States>();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();





    }
	
	// Update is called once per frame
	void Update ()
    {


        if (states.getHp() <= 0)
        {
            // コスト回復
            manager.RecoverCost(states.getCost());

            Destroy(this.gameObject);
        }

	}


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {


            Debug.Log("緋弾のアリア");

            states.setDamege(col.transform.parent.GetComponent<Single>().getBulletDamage());


            Destroy(col);

        }

        if (col.gameObject.tag == "Player")
        {


            Debug.Log("KILL");

           

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
