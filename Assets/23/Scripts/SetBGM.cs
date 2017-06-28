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

        //タイトルシーン
        if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            Singleton<SoundManager>.instance.playBGM("BGM004", 0.0f);
        }

        //セレクトシーン
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            Singleton<SoundManager>.instance.playBGM("BGM002", 0.0f);
        }

        //メイン（戦闘）
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (Singleton<SceneData>.instance.getStageNumber() == 1)//ノーマル
            {
                Singleton<SoundManager>.instance.playBGM("BGM003", 0.0f);
            }
            if (Singleton<SceneData>.instance.getStageNumber() == 2)//ハード
            {
                Singleton<SoundManager>.instance.playBGM("BGM001", 0.0f);
            }
            if (Singleton<SceneData>.instance.getStageNumber() == 3)//インフィニモード
            {
                Singleton<SoundManager>.instance.playBGM("BGM001", 0.0f);
            }

            if (Singleton<SceneData>.instance.getStageNumber() == 4)//チュートリアル
            {
                Singleton<SoundManager>.instance.playBGM("BGM005", 0.0f);
            }


        }

    }



}



