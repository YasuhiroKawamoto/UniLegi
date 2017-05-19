﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derector : MonoBehaviour
{
    Camera mainCamera;

    [SerializeField]
    GameManager manager;

    float m_time;
    bool initFlag = false;

    // Use this for initialization
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        m_time = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        float timeStep = (Time.time - m_time) / 15.0f;

        GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
        if (effects.Length > 0)
        {
            if (initFlag)
            {
                m_time = Time.time;
                initFlag = false;
            }
            manager.SetSpd(0.7f);

            Vector3 pos = new Vector3(effects[0].transform.position.x, effects[0].transform.position.y, this.transform.position.z);
            mainCamera.transform.position = Lerp(mainCamera.transform.position, pos, timeStep);
            mainCamera.orthographicSize = Lerp(mainCamera.orthographicSize, 1.0f, timeStep);

        }

        else
        {
            initFlag = true;

            manager.SetSpd(1.0f);

            mainCamera.transform.position = Lerp(mainCamera.transform.position, new Vector3(0,0,-10), timeStep);
            mainCamera.orthographicSize = Lerp(mainCamera.orthographicSize, 5.0f, timeStep);
        }

        if(!initFlag)
        {

            
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
