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

    GameObject SeneData;


    protected override void Start()
    {
        base.Start();

        SeneData = GameObject.Find("SceneData");

        // Buttonクリック時、OnClickメソッドを呼び出す
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SeneData.GetComponent<SceneDataManager>().SetStage(stageNumber);

        // 「GameScene」シーンに遷移する
        SceneManager.LoadScene(seneName);

    }

}