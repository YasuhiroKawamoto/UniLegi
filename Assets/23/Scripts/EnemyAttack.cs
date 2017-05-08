using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    //Statesコンポーネント
    States states;

    Collider2D C;

    GameManager G;

    Vector2 pos;

    int rate;

    int cnt;

    // Use this for initialization
    void Start () {
        states = this.gameObject.transform.parent.GetComponent<States>();

      this.gameObject.transform.position =  new Vector3 (this.gameObject.transform.parent.position.x,
          (this.gameObject.transform.parent.position.y-(this.gameObject.transform.parent.localScale.y/2)), this.gameObject.transform.parent.position.z);

        C = this.gameObject.GetComponent<Collider2D>();

        C.transform.localScale = new Vector3(1.0f, states.getRenge(), 1.0f);

        rate = states.getrate();

        cnt = 0;

       
    }

    // Update is called once per frame
    void Update () {
       
    }


    void OnTriggerEnter2D(Collider2D col)
    {
       

        if (col.gameObject.tag == "Player")
        {
            Debug.Log("接敵");
            pos = this.gameObject.transform.parent.position;

           
        }


        if (col.gameObject.tag == "DangerZone")
        {
            Debug.Log("拠点接敵");
            pos = this.gameObject.transform.parent.position;


        }


    }

    void OnTriggerStay2D(Collider2D col)
    {
        
        

        if (col.gameObject.tag == "Player")
        {

            this.transform.parent.position = pos;

            cnt++;


            if (rate <= cnt / 60)
            {
                
                col.GetComponent<States>().setDamege(states.getAttack());
                Debug.Log("攻撃");
                cnt = 0;
            }
         
        }

        if (col.gameObject.tag == "DangerZone")
        {

            this.transform.parent.position = pos;

            cnt++;


            if (rate <= cnt / 60)
            {
                col.GetComponent<DangerZone>().SetHp(col.GetComponent<DangerZone>().GetHp() - states.getAttack());
               
                Debug.Log("攻撃");
                cnt = 0;
            }

        }



    }

    void OnTriggerExit2D(Collider2D col)
    {

        Debug.Log("離脱");


    }

}
