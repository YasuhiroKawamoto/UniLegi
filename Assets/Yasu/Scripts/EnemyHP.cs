using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float followTime;

    [SerializeField]
    GameObject hpGage;

    GameObject obj;

    private Canvas canvas;

    // ゲージの表示割合
    float rate;

    // 補間開始点
    float startRate;

    // 補完終点
    float targetRate;

    // 最大HP
    int maxHp;

    GameObject hpGreen = null;
    GameObject hpRed = null;

    private void Awake()
    {
        // 生成時のHPを最大値として設定
        maxHp = gameObject.GetComponent<States>().getHp();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();


    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (hpGreen != null && hpRed != null)
        {
            // 移動終点(現在の値)
            targetRate = hpGreen.GetComponent<Image>().fillAmount = (float)gameObject.GetComponent<States>().getHp() / maxHp;

            // 移動始点(古い値)
            startRate = hpRed.GetComponent<Image>().fillAmount;

            // 線形補間で計算  
            rate = Lerp(startRate, targetRate, followTime, TimeStep);

            hpRed.GetComponent<Image>().fillAmount = rate;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DestroyArea")
        {
            // HPゲージをキャンバスの子として生成
            //hpGage.transform.position = new Vector3(-300, -300, -300);
            hpGage.GetComponent<FollowEnemy>().SetParent(this.gameObject);

            obj = Instantiate(hpGage, canvas.transform);


            hpGreen = obj.transform.FindChild("GageBase/GageG").gameObject;
            hpRed = obj.transform.FindChild("GageBase/GageR").gameObject;
        }
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
    static float Lerp(float startNum, float targetNum, float t, Func<float, float> v)
    {
        float pos;

        pos = (1 - v(t)) * startNum + v(t) * targetNum;

        return pos;
    }
}
