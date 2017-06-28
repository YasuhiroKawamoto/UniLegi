using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score {
    /// <summary>
    /// ハイスコア管理用のスクリプト
    /// 
    /// </summary>
   
    string key = "SaveScore";
    //********** 終了 **********// 
    float NorHiScore;//ノーマルscore

    float HarHiScore;//ハードscore

    float InfHiScore;//インフィニscore
    //********** 開始 **********// 
    void Start()
    {
        //保存キー「Saved」で保存されたint型のデータがあればそれを、
        //無ければブランクを取得
        NorHiScore = PlayerPrefs.GetFloat("SaveScoreNormal");

        HarHiScore = PlayerPrefs.GetFloat("SaveScoreHard");

        InfHiScore = PlayerPrefs.GetFloat("SaveScoreInfinity");
     
    }

    public void SaveScoreNormal(float score)
    {
        NorHiScore = score;//値を代入

        //保存キー「SaveScoreNormal」で入力文字を保存
        PlayerPrefs.SetFloat("SaveScoreNormal", NorHiScore);
        PlayerPrefs.Save();
       
    }

    public float GetHiScoreNormal()
    {
        return PlayerPrefs.GetFloat("SaveScoreNormal");
    }

    public void SaveScoreHard(float score)
    {
        HarHiScore = score;//値を代入

        //保存キー「SaveScoreHard」で入力文字を保存
        PlayerPrefs.SetFloat("SaveScoreHard", HarHiScore);
        PlayerPrefs.Save();
    }

    public float GetHiScoreHard()
    {
        return PlayerPrefs.GetFloat("SaveScoreHard");
    }


    public void SaveScoreInfinity(float score)
    {
        InfHiScore = score;//値を代入

        //保存キー「SaveScoreInfinity」で入力文字を保存
        PlayerPrefs.SetFloat("SaveScoreInfinity", InfHiScore);
        PlayerPrefs.Save();
     
    }

    public float GetHiScoreInfinity()
    {
        return PlayerPrefs.GetFloat("SaveScoreInfinity");
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
        SaveScoreNormal(9999.99f);//時間リセット
        SaveScoreHard(9999.99f);//時間リセット
        SaveScoreInfinity(0);//撃破数リセット

    }

}
