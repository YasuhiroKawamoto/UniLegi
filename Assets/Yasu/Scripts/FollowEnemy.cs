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
        parent = gameObject.transform.parent.parent.parent.gameObject;
        Vector3 parentPos = Vector3.zero;
        Vector3 pos = Vector3.zero;
        if (parentPos == Vector3.zero)
        {
            parentPos = parent.transform.position;
            pos = new Vector3(parentPos.x, parentPos.y + 0.5f, parentPos.z);
            pos = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        }
        
        if(parentPos != Vector3.zero)
        {
            gameObject.GetComponent<RectTransform>().position = pos;
        }






    }
}
