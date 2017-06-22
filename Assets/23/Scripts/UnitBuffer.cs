using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuffer : MonoBehaviour {

    States state;

    BoxCollider2D BC;

    [SerializeField]
    GameObject buffEffect;//バフエフェクト

    private float buffRate;

    private float buffCnt;

    private bool buffFlag;

    private List<GameObject> TargetUnit;

    // Use this for initialization
    void Start () {
        BC = GetComponent<BoxCollider2D>();

        state = GetComponentInParent<States>();
		
	}

    // Update is called once per frame
    void Update()
    {
        if (state.getDead() == false)
        {

            if (buffFlag)//攻撃フラグがONであれば
            {
                buffCnt += Time.deltaTime;

                if (buffRate <= buffCnt)//攻撃間隔にカウントが到達
                {
                    foreach (GameObject obj in TargetUnit)//範囲内ユニットに対して
                    {


                        obj.GetComponent<States>().SetAtk(obj.GetComponent<States>().getAttack() * 2);//攻撃バフ（暫定で2倍）


                        if (buffEffect != null)//エフェクトスロットに設定してある場合
                        {


                            buffEffect.transform.position = obj.transform.position;//エフェクトの位置を設定
                            Instantiate(buffEffect);//エフェクト生成




                        }

                    }

                    Debug.Log("攻撃発動");
                    buffCnt = 0;//カウントリセット
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player")//接触オブジェクトタグがPlayer
        {
            TargetUnit.Add(col.gameObject);

            buffFlag = true;//フラグON

            //col.gameObject.GetComponent<States>().SetLockOn(true);
           

        }


    }

    void OnTriggerStay2D(Collider2D col)
    {

    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag == "player")
        {
            TargetUnit.Clear();
            //col.gameObject.GetComponent<States>().SetLockOn(false);
            buffFlag = false;//フラグOFF


        }

    }





}
