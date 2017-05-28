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

    GameObject SceneData;


    protected override void Start()
    {
        base.Start();

        SceneData = GameObject.Find("SceneData");

        // Buttonクリック時、OnClickメソッドを呼び出す
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Singleton<SceneData>.instance.setStageNumber(stageNumber);

        // 「GameScene」シーンに遷移する
        SceneManager.LoadScene(seneName);

    }

}