using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    [SerializeField]
    GameObject hand1;

    [SerializeField]
    GameObject hand2;

    [SerializeField]
    GameObject Area;

    [SerializeField]
    GameObject Effect;

    [SerializeField]
    MyFade fade;

    [SerializeField]
    MyFade fade2;

    [SerializeField]
    Canvas canvas;

    float alpha = 0;
    int phase = 0;

    // Use this for initialization
    void Start()
    {
        alpha = 0;
        phase = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 0)
        {
            if (Input.touchCount > 0)
            {
                if (Input.touchCount == 2)
                {

                    Area.transform.position = new Vector3(0, -2, 0);

                    Vector3 pos1 = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    pos1.y = -2;
                    pos1.z = 0;

                    Vector3 pos2 = Camera.main.ScreenToWorldPoint(Input.touches[1].position);
                    pos2.y = -2;
                    pos2.z = 0;


                    Area.GetComponent<BoxCollider2D>().size = new Vector2(Mathf.Abs(pos1.x - pos2.x), 1);
                    pos1 = new Vector2(Area.transform.position.x - Area.GetComponent<BoxCollider2D>().size.x / 2, -2);
                    hand1.transform.position = pos1;

                    pos2 = new Vector2(Area.transform.position.x + Area.GetComponent<BoxCollider2D>().size.x / 2, -2);
                    hand2.transform.position = pos2;

                    // シーン繊維
                    if (Area.GetComponent<BoxCollider2D>().size.x <= 2)
                    {
                        phase = 1;
                    }
                }
            }
            else
            {
                hand1.transform.position = new Vector2(-3.45f, -2);
                hand2.transform.position = new Vector2(3.45f, -2);
            }
        }
        else if (phase == 1)
        {
            Instantiate(Effect);
            fade2 = Instantiate(fade2, canvas.transform);

            phase = 2;

        }
        else
        {
            fade2.SetAlpha(alpha += Time.deltaTime * 0.5f);
            if (alpha >= 1)
            {
                phase = 0;
                // 「GameScene」シーンに遷移する
                SceneManager.LoadScene("SelectScene");
            }
        }
    }
}