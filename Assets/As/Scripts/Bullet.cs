using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    public float speed = 5.0f;
    [SerializeField]
    private int m_BulletDamege = 1;
    [SerializeField]
    private bool m_flag=false;
    Tap tap;
    [SerializeField]
    private float m_dir = 90;



    // Use this for initialization
    void Start () {

        Vector2 v;

        if (m_flag == false)
        {
            v.x = Mathf.Cos(Mathf.Deg2Rad * m_dir) * speed;
            v.y = Mathf.Sin(Mathf.Deg2Rad * m_dir) * speed;
            GetComponent<Rigidbody2D>().velocity = v;
        }
        else if (m_flag == true)
        {
            v.x = Mathf.Cos(Mathf.Deg2Rad * m_dir) * speed;
            v.y = Mathf.Sin(Mathf.Deg2Rad * m_dir) * speed;
            GetComponent<Rigidbody2D>().velocity -= v;
        }
        //GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;



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
