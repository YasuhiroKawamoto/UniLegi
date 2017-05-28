using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFade : MonoBehaviour {
    [SerializeField]
    Fade fade = null;

    public void FadeIn(float time)
    {
        fade.FadeIn(time);
    }

    public void FadeOut(float time)
    {
        fade.FadeOut(time);
    }

    public void OnClick()
    {
        fade.FadeIn(1, () =>
        {
            fade.FadeOut(1);
        });
    }
}
