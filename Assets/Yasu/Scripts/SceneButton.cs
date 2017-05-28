using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class SceneButton : UIBehaviour
{
    [SerializeField]
    int stageNumber;

    [SerializeField]
    string seneName;


    float time;
    Fade fade;

    bool flag;

    GameObject SceneData;
    //SceneFade fade;

    protected override void Start()
    {
        base.Start();
        flag = false;

        SceneData = GameObject.Find("SceneData");
        fade = SceneData.GetComponent<SceneDataManager>().GetFade();

        // Buttonクリック時、OnClickメソッドを呼び出す
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Singleton<SceneData>.instance.setStageNumber(stageNumber);
        Singleton<SoundManager>.instance.playSE("se004");
        time = Time.time;
        fade.FadeIn(1);
        flag = true;

    }


    private void Update()
    {
        if (flag)

        {
            if (Time.time - time > 1)

                // 「GameScene」シーンに遷移する
                SceneManager.LoadScene(seneName);
        }
    }
}