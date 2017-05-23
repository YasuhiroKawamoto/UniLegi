using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CameraState
{
    STAY,
    ZOOMIN,
    ZOOMOUT,

}
public class Derector : MonoBehaviour
{
    Camera mainCamera;

    CameraState state =CameraState.STAY;


    [SerializeField]
    GameManager manager;

    float m_time;
    float m_stayCnt = 0;

    bool Initflag = true;

    // Use this for initialization
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeStep = (Time.time - m_time) / 5.0f;

        GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
        if (effects.Length > 0)
        {
            state = CameraState.ZOOMIN;

        }

        else
        {
            state = CameraState.STAY;
        }


        switch (state)
        {
            case CameraState.STAY:
                break;
            case CameraState.ZOOMIN:
                Vector3 pos = new Vector3(effects[0].transform.position.x, effects[0].transform.position.y, this.transform.position.z);
                mainCamera.transform.position = Lerp(mainCamera.transform.position, pos, timeStep);
                mainCamera.orthographicSize = Lerp(mainCamera.orthographicSize, 2.0f, timeStep);
                break;
            case CameraState.ZOOMOUT:
                mainCamera.transform.position = Lerp(mainCamera.transform.position, new Vector3(0, 0, -10), timeStep);
                mainCamera.orthographicSize = Lerp(mainCamera.orthographicSize, 5.0f, timeStep);
                break;
            default:
                break;
        }
    }

    // 線形補間用関数
    static float Lerp(float startNum, float targetNum, float t)
    {
        float retNum = 0.0f;

        retNum = (1 - t) * startNum + t * targetNum;

        return retNum;
    }
    static Vector3 Lerp(Vector3 startNum, Vector3 targetNum, float t)
    {
        Vector3 pos;

        pos = (1 - t) * startNum + t * targetNum;

        return pos;
    }
}
