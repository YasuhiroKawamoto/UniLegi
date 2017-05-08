using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    public float speed = 5.0f;
    [SerializeField]
    private int m_BulletDamege = 1;
    [SerializeField]
    private bool m_flag=true;
    Tap tap;


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

    public void setInverdFlag(bool flag)
    {
        m_flag = flag;
    }

    public bool getInverdFlag()
    {
        return m_flag;
    }


}
