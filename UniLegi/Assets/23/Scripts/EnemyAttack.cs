using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    //Statesコンポーネント
    States states;

    Collider2D C;


   
    

    // Use this for initialization
    void Start () {
        states = this.gameObject.transform.parent.GetComponent<States>();

      this.gameObject.transform.position =  new Vector3 (this.gameObject.transform.parent.position.x,
          (this.gameObject.transform.parent.position.y-(this.gameObject.transform.parent.localScale.y/2)), this.gameObject.transform.parent.position.z);

        C = this.gameObject.GetComponent<Collider2D>();

        C.transform.localScale = new Vector3(1.0f, states.getRenge(), 1.0f);

       
    }

    // Update is called once per frame
    void Update () {
       
    }


    void OnTriggerEnter2D(Collider2D col)
    {
       

        if (col.gameObject.tag == "Player")
        {

            col.GetComponent<States>().setDamege(states.getAttack());



        }


    }

    void OnCollisionStay2D(Collision2D col)
    {





    }

    void OnCollisionExit2D(Collision2D col)
    {




    }

}
