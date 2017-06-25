using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionSkil : MonoBehaviour {

    [SerializeField]
    GameObject AttackPrefab;

    GameObject frontLine;

    [SerializeField]
    GameObject ParentObj;

    private void Awake()
    {
        frontLine = GameObject.Find("FrontLine");
    }

    // Use this for initialization
    void Start () {

        Vector3 pos = AttackPrefab.transform.position;
        pos.y = frontLine.transform.position.y;

        ParentObj.transform.position = pos;
        Instantiate(ParentObj);
        Instantiate(AttackPrefab.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
