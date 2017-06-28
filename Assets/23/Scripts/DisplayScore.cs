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
    void Start () {

       

    }
	
	// Update is called once per frame
	void Update () {


        NomalScore.text ="TIME  " +Singleton<Score>.instance.GetHiScoreNormal().ToString("F2");

        HardScore.text = "TIME  " + Singleton<Score>.instance.GetHiScoreHard().ToString("F2");

        InfinityScore.text = "KILL  " + Singleton<Score>.instance.GetHiScoreInfinity().ToString() ;


    }
}
