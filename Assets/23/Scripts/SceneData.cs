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
        int local = 0;

        local = num;


        enemyCnt += local;

    }

   


    public void setStageNumber(int num)
    {
        StageNumber = num;
    }

    public int getStageNumber()
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
