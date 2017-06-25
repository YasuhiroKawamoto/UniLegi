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

    MyFade fade;


    float time;

    bool flag;
    float alpha;

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
        flag = true;
        MyFade fade_ = Resources.Load<MyFade>("Prefabs/MyFade");
        fade = Instantiate(fade_, GameObject.Find("Canvas").transform);

        Singleton<SceneData>.instance.setEnemyCnt(0);//敵撃破数初期化
    }


    private void Update()
    {
        if (flag)

        {
            fade.SetAlpha(alpha += 0.05f);
            if (Time.time - time > 1)

                // 「GameScene」シーンに遷移する
                SceneManager.LoadScene(seneName);
        }
    }
}