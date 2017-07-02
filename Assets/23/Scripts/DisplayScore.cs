using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {



    [SerializeField]
    private Text NomalScore;

    [SerializeField]
    private Text HardScore;

    [SerializeField]
    private Text InfinityScore;



    // Use this for initialization
    void Start () 
	{
		if (Singleton<Score>.instance.GetHiScoreNormal() == 0) 
		{
		
			Singleton<Score>.instance.SaveScoreNormal(300.0f);
		
		}
		if (Singleton<Score>.instance.GetHiScoreHard() == 0) {

			Singleton<Score>.instance.SaveScoreHard(300.0f);

		}

    }
	
	// Update is called once per frame
	void Update () {


        NomalScore.text ="TIME  " +Singleton<Score>.instance.GetHiScoreNormal().ToString("F2");

        HardScore.text = "TIME  " + Singleton<Score>.instance.GetHiScoreHard().ToString("F2");

        InfinityScore.text = "KILL  " + Singleton<Score>.instance.GetHiScoreInfinity().ToString() ;


    }
}
