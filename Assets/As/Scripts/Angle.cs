using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour {

    //
    Firing firing;

    [SerializeField]
    private float m_spd;
    //角度の設定
    [SerializeField]
    private float m_dir = 0;
    //Bulletのプレハブ
    public GameObject bullet;
    //フラグ
    public bool m_flag = true;
    //稼働範囲
    [SerializeField]
    private float m_angle = 180.0f;
    //リロード中のフラグ
    private bool m_coolFlag = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        firing = GetComponent<Firing>();
        m_coolFlag = firing.GetFlag();

        if(m_dir > m_angle && m_flag==true)
        {
            m_flag = false;
        } 
        else if(m_dir < 30.0f && m_flag==false)
        {
            m_flag = true;
        }

        if (m_coolFlag == false)
        {
            if (m_flag == true)
            {
                m_dir += m_spd;
            }
            else if (m_flag == false)
            {
                m_dir -= m_spd;
            }
        }
        Transform Children = bullet.GetComponentInChildren<Transform>();

        foreach (Transform ob in Children)
        {
            ob.GetComponentInChildren<Bullet>().setDir(m_dir);
        }

    }

    
}
