using System;
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

    [SerializeField]
    PlayerControl player;

    [SerializeField]
    GameObject unionGage;

    [SerializeField]
    GameObject unionGage2;

    [SerializeField]
    GameObject overLoadGage;

    [SerializeField]
    GameObject overLoadGage2;

    // ゲージの表示割合
    float rate;

    // 補間開始点
    float startRate;

    // 補完終点
    float targetRate;

    float m_autoMoveTime;

    bool SoundFlag;

    float EnemyMax;

    public int getEnemyMax() { return (int)EnemyMax; }

    // Use this for initialization
    void Start () {
        m_autoMoveTime = Time.time;

        switch (Singleton<SceneData>.instance.getStageNumber())
        {
            case 1://ノーマル
                EnemyMax = 139.0f;
                break;

            case 2://ハード
                EnemyMax = 167.0f;
                break;


            case 3://インフィニティモード
                EnemyMax = 100.0f;
                break;
            default:
                EnemyMax = 100.0f;
                break;

        }

        

    }

    // Update is called once per frame
    void Update () {
        float timeStep = (Time.time - m_autoMoveTime)/ followTime;


        // ===================================================================================
        // 移動終点(現在の値)
        if (Singleton<SceneData>.instance.getStageNumber() != 3)
        {
            targetRate = hpGreen.GetComponent<Image>().fillAmount = (EnemyMax - Singleton<SceneData>.instance.getEnemyCnt()) / EnemyMax;
        }
        else {
            targetRate = hpGreen.GetComponent<Image>().fillAmount = 1;
        }

        // 移動始点(古い値)
        startRate = hpRed.GetComponent<Image>().fillAmount;

        // 線形補間で計算  
        rate = Lerp(startRate, targetRate, followTime, TimeStep);

        {
            hpRed.GetComponent<Image>().fillAmount = rate;
        }


        //インフィニティモード専用ゲージ処理
        if (Singleton<SceneData>.instance.getStageNumber() == 3)
            {

            if (EnemyMax - Singleton<SceneData>.instance.getEnemyCnt() <=0)
            {
                EnemyMax += 100;//最大値を増加（100体毎に）
            }

        }

        // ===================================================================================



        // コストゲージ
        costGage.GetComponent<Image>().fillAmount = (float)manager.GetCost() / manager.GetMaxCost();


        UnionGage();

        OverLoadGage();
        
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

    void UnionGage()
    {
        unionGage.GetComponent<Image>().fillAmount = 1 - (float)player.GetUnionCoolTime() / player.GetCoolTime();
        unionGage2.GetComponent<Image>().fillAmount = 1 - (float)player.GetUnionCoolTime() / player.GetCoolTime(); ;
        if (unionGage.GetComponent<Image>().fillAmount >= 1)
        {

            //if (SoundFlag == false)
            //{
            //    Singleton<SoundManager>.instance.playSE("se008");
            //    SoundFlag = true;
            //}

            // 最大時画像
            //unionGage.GetComponent<Image>().sprite = Resources.Load<Sprite>("gage2");
        }
        else
        {
            SoundFlag = false;
            //最大時じゃない画像
            //unionGage.GetComponent<Image>().sprite = Resources.Load<Sprite>("gage");
        }

        if (unionGage2.GetComponent<Image>().fillAmount >= 1)
        {

           

            // 最大時画像
            //unionGage.GetComponent<Image>().sprite = Resources.Load<Sprite>("gage2");
        }
        else
        {
            //最大時じゃない画像
            //unionGage.GetComponent<Image>().sprite = Resources.Load<Sprite>("gage");
        }
    }


    void OverLoadGage()
    {
        overLoadGage.GetComponent<Image>().fillAmount = (float)player.GetOverload() / player.GetOverMAX();
        overLoadGage2.GetComponent<Image>().fillAmount = (float)player.GetOverload() / player.GetOverMAX(); ;
        if (overLoadGage.GetComponent<Image>().fillAmount >= 1)
        {

            

            // 最大時画像
            //unionGage.GetComponent<Image>().sprite = Resources.Load<Sprite>("gage2");
        }
        else
        {
            SoundFlag = false;
            //最大時じゃない画像
            //unionGage.GetComponent<Image>().sprite = Resources.Load<Sprite>("gage");
        }

        if (overLoadGage2.GetComponent<Image>().fillAmount >= 1)
        {

           

            // 最大時画像
            //unionGage.GetComponent<Image>().sprite = Resources.Load<Sprite>("gage2");
        }
        else
        {
            //最大時じゃない画像
            //unionGage.GetComponent<Image>().sprite = Resources.Load<Sprite>("gage");
        }
    }
}

