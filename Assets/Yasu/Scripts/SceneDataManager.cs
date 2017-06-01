using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDataManager : MonoBehaviour {

    [SerializeField]
    int stageNumber;

    [SerializeField]
    MyFade fade;

    float alpha = 1;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
       // fade.SetFade(false);

    }
	
	// Update is called once per frame
	void Update () {
        fade.SetAlpha(alpha -= 0.05f);
        if(alpha <=0 && fade!=null)
        {
            Destroy(fade.gameObject);
        }

        //GameObject data = GameObject.FindGameObjectWithTag("SceneData");

        //if (data != null && data != this.gameObject)
        //{
        //    Destroy(data);
        //}
    }


    private void OnDestroy()
    {
        //fade.FadeIn(1);


    }

    public void SetStage(int stage)
    {
        stageNumber = stage;
    }

    public int GetStage()
    {
        return stageNumber;
    }

    public MyFade GetFade()
    {
      return fade;
    }
}
