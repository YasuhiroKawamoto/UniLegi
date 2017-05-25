﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    [SerializeField]
    private float followTime;

    [SerializeField]
    GameObject hpGreen;

    [SerializeField]
    GameObject hpRed;

    [SerializeField]
    GameManager manager;

    [SerializeField]
    GameObject costGage;

    // ゲージの表示割合
    float rate;

    // 補間開始点
    float startRate;

    // 補完終点
    float targetRate;

    float m_autoMoveTime;


    // Use this for initialization
    void Start () {
        m_autoMoveTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        float timeStep = (Time.time - m_autoMoveTime)/ followTime;



        // 移動終点(現在の値)
       targetRate = hpGreen.GetComponent<Image>().fillAmount = manager.GetHp() / 100.0f;

        // 移動始点(古い値)
        startRate = hpRed.GetComponent<Image>().fillAmount;

        // 線形補間で計算  
        rate = Lerp(startRate, targetRate, followTime, TimeStep);


        //if (startRate != targetRate)
        {
            hpRed.GetComponent<Image>().fillAmount = rate;
        }


        // コストゲージ
        costGage.GetComponent<Image>().fillAmount = (float)manager.GetCost() / manager.GetMaxCost();
    }


    // 線形補間用関数
    static float Lerp(float startNum, float targetNum, float t, Func<float, float> v)
    {
        float retNum = 0.0f;


        retNum = (1 - v(t)) * startNum + v(t) * targetNum;

        return retNum;
    }

    static float TimeStep(float stepTime)
    {
        float m_currentTime = 0;
        if (m_currentTime < stepTime)
        {
            m_currentTime += 0.1f;
        }

        return m_currentTime;
    }

}
