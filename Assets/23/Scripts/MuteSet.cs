using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSet : MonoBehaviour {


    public bool IsMute = false;//ミュート状態の取得
	
	// Update is called once per frame
	void Update () {

        IsMute = Singleton<SoundManager>.instance.getIsMute();

        if (IsMute)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1.0f;
        }
		
	}
}
