using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionSkil : MonoBehaviour {

    [SerializeField]
    GameObject AttackPrefab;

    [SerializeField]
    GameObject CutIn;

    GameObject frontLine;
    Canvas canvas;

    [SerializeField]
    GameObject ParentObj;

    private void Awake()
    {
        frontLine = GameObject.Find("FrontLine");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Use this for initialization
    void Start () {

        Vector3 pos = new Vector3(0,0,0);
        pos.y = frontLine.transform.position.y;

        ParentObj.transform.position = pos;
        Instantiate(AttackPrefab, Instantiate(ParentObj).transform);

        switch (this.gameObject.GetComponent<States>().GetTypeId())
        {
            case 1://ドラゴン
                Singleton<SoundManager>.instance.playSE("se011");
                break;
            case 2://クラーケン
                Singleton<SoundManager>.instance.playSE("se015");
                break;
            case 3://リッチ
                Singleton<SoundManager>.instance.playSE("se014");
                break;
            case 4://リリス
                Singleton<SoundManager>.instance.playSE("se012");
                break;
            case 5://パラディン
                Singleton<SoundManager>.instance.playSE("se013");
                break;
        }
      

        Instantiate(CutIn, canvas.transform);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
