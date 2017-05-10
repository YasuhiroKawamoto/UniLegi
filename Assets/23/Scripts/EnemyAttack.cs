using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    //Statesコンポーネント
    States states;

    Collider2D C;

    Vector2 pos;


    [SerializeField]
    GameObject effect;

    int rate;

    int cnt;

    // Use this for initialization
    void Start () {
        states = this.gameObject.transform.parent.GetComponent<States>();

      this.gameObject.transform.position =  new Vector3 (this.gameObject.transform.parent.position.x,
          (this.gameObject.transform.parent.position.y-0.2f), this.gameObject.transform.parent.position.z);

        C = this.gameObject.GetComponent<Collider2D>();

        C.transform.localScale = new Vector3(1.0f, states.getRenge(), 0.0f);

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
            //this.transform.parent.GetComponent<Mover>().setMoveFlag(false);


        }


        if (col.gameObject.tag == "DangerZone")
        {
            Debug.Log("拠点接敵");
            pos = this.gameObject.transform.parent.position;
            //this.transform.parent.GetComponent<Mover>().setMoveFlag(false);
           
            
        }


    }

    void OnTriggerStay2D(Collider2D col)
    {
        
        

        if (col.gameObject.tag == "Player")
        {
            if (pos != new Vector2(0, 0))
            {
                this.gameObject.transform.parent.position = pos;
            }
            cnt++;


            if (rate <= cnt / 60)
            {
                
                col.GetComponent<States>().setDamege(states.getAttack());

                effect.transform.position = col.transform.position;

                if (effect != null)
                {
                    Instantiate(effect);
                }

                Debug.Log("攻撃");
                cnt = 0;
            }
         
        }

        if (col.gameObject.tag == "DangerZone")
        {

            if (pos != new Vector2(0, 0))
            {
                this.gameObject.transform.parent.position = pos;
            }

            cnt++;


            if (rate <= cnt / 60)
            {
                col.GetComponent<DangerZone>().SetHp(col.GetComponent<DangerZone>().GetHp() - states.getAttack());


                effect.transform.position = new Vector3(this.gameObject.transform.position.x, col.transform.position.y, 0);

                if (effect != null)
                {
                    Instantiate(effect);
                }

                Debug.Log("攻撃");
                cnt = 0;
            }

        }



    }

    void OnTriggerExit2D(Collider2D col)
    {

<<<<<<< HEAD
        pos = new Vector2(0, 0);
=======

>>>>>>> fdd8c999e7ca9990ffe7ff989d9a5a1cee4bc8f0
        //this.transform.parent.GetComponent<Mover>().setMoveFlag(true);
        Debug.Log("離脱");


    }

}
