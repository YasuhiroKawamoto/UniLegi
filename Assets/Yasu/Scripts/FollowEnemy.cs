using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour {

    [SerializeField]
    float diff;

    [SerializeField]
    GameObject parent;


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        Vector3 parentPos = Vector3.zero;
        Vector3 pos = Vector3.zero;

       if (parent != null)
        {
            parentPos = parent.transform.position;
            pos = new Vector3(parentPos.x, parentPos.y + diff, parentPos.z);
            //pos = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
            gameObject.GetComponent<RectTransform>().position = pos;
        }

        if (parent == null)
        {
            Destroy(gameObject);
        }

    }
    public void SetParent(GameObject parentObject)
    {
        parent = parentObject;
    }
}

