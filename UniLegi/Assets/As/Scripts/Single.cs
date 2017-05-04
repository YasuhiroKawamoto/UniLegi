using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single : MonoBehaviour {

    //弾のスピード
    [SerializeField]
    public float speed=5.0f;
    [SerializeField]
    private int m_BulletDamege = 1;


    // Use this for initialization
    void Start () {

        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;

        

	}
	
	// Update is called once per frame
	void Update () {
		
        

	}


    public int getBulletDamage()
    {

        return m_BulletDamege;
    }

}
