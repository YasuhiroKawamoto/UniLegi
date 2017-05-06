using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField]
    private int m_hp = 1000;

	// Use this for initialization
	void Start ()
    {
       

    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void SetHp(int hp)
    {
        m_hp = hp;
    }

    public int GetHp()
    {

        return m_hp;
    }



}
