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


        NomalScore.text = "CLEARTIME\n" + Singleton<Score>.instance.GetHiScoreNormal().ToString("F2");

        HardScore.text = "CLEARTIME\n" + Singleton<Score>.instance.GetHiScoreHard().ToString("F2");

        InfinityScore.text = Singleton<Score>.instance.GetHiScoreInfinity().ToString() + "  KILL";


    }
}
