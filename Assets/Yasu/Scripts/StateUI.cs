using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StateUI : MonoBehaviour {

    static public float HP;
    static public float ATK;

    Text hp;
    Text atk;


    // Use this for initialization
    void Start () {
        hp = GameObject.Find("HpTxt").GetComponent<Text>();
        atk = GameObject.Find("AtkTxt").GetComponent<Text>();

        hp.text = HP.ToString();
        atk.text = ATK.ToString();
    }

    // Update is called once per frame
    void Update () {

    }

    public void SetHP(float hp)
    {
        HP = hp;
    }

    public void SetATK(float atk)
    {
        ATK = atk;
    }
}
