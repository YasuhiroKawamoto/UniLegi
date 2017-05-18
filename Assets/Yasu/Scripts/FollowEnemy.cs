using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour {

    GameObject parent;
	// Use this for initialization
	void Start () {
        parent = gameObject.transform.parent.parent.parent.gameObject;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 parentPos = parent.transform.position;

        Vector3 pos = new Vector3(parentPos.x, parentPos.y + 0.5f, parentPos.z);
        gameObject.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);




    }
}
