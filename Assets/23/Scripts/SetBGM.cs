using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetBGM : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //シーン切り替わり時にBGMをスタート

        //タイトルシーン
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            Singleton<SoundManager>.instance.playBGM("BGM001", 0.0f);
        }

        //セレクトシーン
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            Singleton<SoundManager>.instance.playBGM("BGM002", 0.0f);
        }

        //メイン（戦闘）
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (Singleton<SceneData>.instance.getStageNumber() == 0)
            {
                Singleton<SoundManager>.instance.playBGM("BGM003", 0.0f);
            }
            if (Singleton<SceneData>.instance.getStageNumber() == 1)
            {
                Singleton<SoundManager>.instance.playBGM("BGM001", 0.0f);
            }
        }

    }



}



