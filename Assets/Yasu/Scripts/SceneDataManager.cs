using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDataManager : MonoBehaviour {

    [SerializeField]
    int stageNumber;

    [SerializeField]
    Fade fade;

    private void Awake()
    {
        fade.ResetFade();
    }

    // Use this for initialization
    void Start () {
        fade.FadeOut(1);

        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            Singleton<SoundManager>.instance.playBGM("BGM001", 0.0f);
        }

        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            Singleton<SoundManager>.instance.playBGM("BGM002", 0.0f);
        }


        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (stageNumber == 1)
            {
                Singleton<SoundManager>.instance.playBGM("BGM003", 0.0f);
            }
            if (stageNumber == 2)
            {
                Singleton<SoundManager>.instance.playBGM("BGM001", 0.0f);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        GameObject data = GameObject.FindGameObjectWithTag("SceneData");

        if (data != null && data != this.gameObject)
        {
            Destroy(data);
        }
    }


    private void OnDestroy()
    {
        fade.FadeIn(1);
    }

    public void SetStage(int stage)
    {
        stageNumber = stage;
    }

    public int GetStage()
    {
        return stageNumber;
    }

    public Fade GetFade()
    {
        return fade;
    }
}
