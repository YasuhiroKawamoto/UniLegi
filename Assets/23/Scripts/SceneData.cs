using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData {


    private int StageNumber = 1;//ステージの番号

    private float time;

    private int enemyCnt = 0;

    public int getEnemyCnt()
    {
        return enemyCnt;
    }
    public void setEnemyCnt(int num)
    {
        enemyCnt = num;

    }

    public void setStageNumber(int num)//ステージ番号設定
    {
        StageNumber = num;
    }

    public int getStageNumber()//ステージ番号取得
    {
        return StageNumber;

    }



    public void setTime(float num)
    {
        time = num;
    }

    public float getTime()
    {
        return time;

    }

}
