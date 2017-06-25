using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionSkil : MonoBehaviour {

    [SerializeField]
    GameObject AttackPrefab;

    [SerializeField]
    GameObject CutIn;

    GameObject frontLine;
    Canvas canvas;

    [SerializeField]
    GameObject ParentObj;

    private void Awake()
    {
        frontLine = GameObject.Find("FrontLine");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Use this for initialization
    void Start () {

        Vector3 pos = AttackPrefab.transform.position;
        pos.y = frontLine.transform.position.y;

        ParentObj.transform.position = pos;
        Instantiate(ParentObj);
        Instantiate(AttackPrefab.transform);
        Instantiate(CutIn, canvas.transform);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
