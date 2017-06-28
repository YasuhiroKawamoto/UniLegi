using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score {

   
    string key = "SaveScore";
    //********** 終了 **********// 
    float HiScore;
    
 
    //********** 開始 **********// 
    void Start()
    {
        //保存キー「Saved」で保存されたint型のデータがあればそれを、
        //無ければブランクを取得
        HiScore = PlayerPrefs.GetFloat("SaveScore");
        //********** 終了 **********// 
    }

    public void SaveScore(float score)
    {
        HiScore = score;
        //********** 開始 **********//
        //保存キー「SavedText」で入力文字を保存
        PlayerPrefs.SetFloat("SaveScore", HiScore);
        PlayerPrefs.Save();
        //********** 終了 **********// 
     
       
    }

    public float GetHiScore()
    {
        return PlayerPrefs.GetFloat("SaveScore");
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();

    }

}
