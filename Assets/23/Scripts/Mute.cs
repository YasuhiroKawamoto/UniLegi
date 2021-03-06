﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mute: MonoBehaviour
{
    [SerializeField]
    Text MuteText;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(IsMute);//ボタンプッシュでミュート切り替え
       
    }

  

    void IsMute()
    {
        if (Singleton<SoundManager>.instance != null)
        {
            if (Singleton<SoundManager>.instance.getIsMute() == true)
            {
                Singleton<SoundManager>.instance.setIsMute(false);//ミュートフラグをオフ
                Singleton<SoundManager>.instance.playBGM();//BGMを再生
                MuteText.text = "♪";


            }
            else
            {

                Singleton<SoundManager>.instance.setIsMute(true);//ミュートフラグをオン
                Singleton<SoundManager>.instance.pauseBGM();//BGMを一時停止
                MuteText.text = "♪×";


            }
        }
    }

    // Update is called once per frame

}
