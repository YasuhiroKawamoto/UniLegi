using System;
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
    int m_stayCnt = 0;

    bool Initflag = true;

    // Use this for initialization
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeStep = (Time.time - m_time) / 25.0f;

        GameObject effect = GameObject.Find("Effect01(Clone)");
        if (effect != null)
        {
            state = CameraState.ZOOMIN;
            manager.SetSpd(0.4f);


        }

        else
        {
            state = CameraState.STAY;
            m_stayCnt++;
            manager.SetSpd(1.0f);

            if(m_stayCnt >15)
            {
                state = CameraState.ZOOMOUT;
            }
        }

        switch (state)
        {
            case CameraState.STAY:
                break;
            case CameraState.ZOOMIN:
                Vector3 pos = Vector3.zero;
                if (effect != null)
                {
                    pos = new Vector3(effect.transform.position.x, effect.transform.position.y, Camera.main.transform.position.z);
                }
                mainCamera.transform.position = Lerp(mainCamera.transform.position, pos, 0.5f, TimeStep );
                mainCamera.orthographicSize = Lerp(mainCamera.orthographicSize, 2.0f, 0.1f * Time.timeScale, TimeStep);
                break;
            case CameraState.ZOOMOUT:
                mainCamera.transform.position = Lerp(mainCamera.transform.position, new Vector3(0, 0, -10), 0.5f, TimeStep);
                mainCamera.orthographicSize = Lerp(mainCamera.orthographicSize, 5.0f, 0.1f * Time.timeScale, TimeStep);
                break;
            default:
                break;
        }
    }

    // 線形補間用関数
    static float Lerp(float startNum, float targetNum,float t, Func<float,float> v)
    {
        float retNum = 0.0f;
         

        retNum = (1 - v(t)) * startNum + v(t) * targetNum;

        return retNum;
    }

    static float TimeStep(float stepTime)
    {
        float m_currentTime = 0;
        if(m_currentTime <stepTime)
        {
            m_currentTime +=0.1f;
        }

        return m_currentTime;
    }
    static Vector3 Lerp(Vector3 startNum, Vector3 targetNum, float t, Func<float, float> v)
    {
        Vector3 pos;

        pos = (1 - v(t)) * startNum + v(t) * targetNum;

        return pos;
    }
}
