using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWay : MonoBehaviour {

    //
    [SerializeField]
    private float m_speed=5.0f;
    //弾のダメージ
    [SerializeField]
    private int m_BulletDamege = 1;


    // Use this for initialization
    void Start () {
        
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * m_speed;

    }
	
	// Update is called once per frame
	void Update () {

        // 子オブジェクトがなければ消滅
        if (gameObject.transform.childCount == 0)
        {
            Destroy(gameObject);
        }

    }

    //与えるダメージを取得させる

    public int getBulletDamage()
    {

        return m_BulletDamege;
    }

}
