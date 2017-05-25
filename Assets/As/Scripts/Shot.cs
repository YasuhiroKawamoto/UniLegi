using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    [SerializeField]
    private bool m_flag = true;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        // 子オブジェクトがなければ消滅
        if (gameObject.transform.childCount == 0)
        {
            Destroy(gameObject);
        }

    }

    public void setInverdFlag(bool flag)
    {
        m_flag = flag;
    }

}
