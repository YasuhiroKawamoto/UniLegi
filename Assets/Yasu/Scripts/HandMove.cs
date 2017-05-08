using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION
{
    LEFT,
    RIGHT
}
public class HandMove : MonoBehaviour
{

    [SerializeField]
    private DIRECTION direction = DIRECTION.LEFT;

    [SerializeField]
    private GameObject Area;
    private Vector3 AreaPos;


    private bool isMove;
    // Use this for initialization
    void Start()
    {
        isMove = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Area.tag == "Pinched")
        {
            isMove = true;
        }
        else
        {
            AreaPos = Area.transform.position;
        }

        if (isMove)
        {
            Vector3 newPos = this.gameObject.transform.position;
            if (direction == DIRECTION.LEFT)
            {
                if (this.gameObject.transform.position.x <= AreaPos.x-0.5f)
                {
                    newPos.x += 0.2f;
                    isMove = false;
                }
                else
                {
                    transform.position = new Vector3(-300, -300, -300);
                }
            }

            else if (direction == DIRECTION.RIGHT)
            {
                if (this.gameObject.transform.position.x >= AreaPos.x+0.5f)
                {
                    newPos.x += -0.2f;
                    isMove = false;
                }
                else
                {
                    transform.position = new Vector3(-300, -300, -300);
                }
            }
            this.gameObject.transform.position = newPos;
            newPos = this.gameObject.transform.position;

        }




    }
}
