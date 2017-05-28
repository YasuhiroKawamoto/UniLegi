using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager 
{

    GameObject soundPlayerObj;
    AudioSource audioSource;
    Dictionary<string, AudioClipInfo> audioClips = new Dictionary<string, AudioClipInfo>();

    BGMPlayer curBGMPlayer;
    BGMPlayer fadeOutBGMPlayer;

    bool IsMute;//ミュートしているか
    

    // AudioClip information
    class AudioClipInfo
    {
        public string resourceName;
        public string name;
        public AudioClip clip;

        public AudioClipInfo(string resourceName, string name)
        {
            this.resourceName = resourceName;
            this.name = name;
        }
    }

    public SoundManager()
    {


        audioClips.Add("se001", new AudioClipInfo("hit", "se001"));
        audioClips.Add("se002", new AudioClipInfo("Sammon", "se002"));
        audioClips.Add("se003", new AudioClipInfo("Dead", "se003"));
        audioClips.Add("se004", new AudioClipInfo("Button", "se004"));
        audioClips.Add("se005", new AudioClipInfo("Fanfare", "se005"));
        audioClips.Add("se006", new AudioClipInfo("Lose", "se006"));
        audioClips.Add("se007", new AudioClipInfo("Pick", "se007"));
        audioClips.Add("se008", new AudioClipInfo("GaugeMax", "se008"));
        //audioClips.Add("se009", new AudioClipInfo("Danger", "se009"));





        audioClips.Add("BGM001", new AudioClipInfo("TitleBGM", "BGM001"));
        audioClips.Add("BGM002", new AudioClipInfo("SelectBGM", "BGM002"));
        audioClips.Add("BGM003", new AudioClipInfo("BattleBGM2", "BGM003"));
        //audioClips.Add("BGM004", new AudioClipInfo("BattleBGM2", "BGM003"));
        //audioClips.Add("BGM005", new AudioClipInfo("BattleBGM2", "BGM003"));


    }

    public bool playSE(string seName)
    {
        if (audioClips.ContainsKey(seName) == false)
            return false; // not register

        AudioClipInfo info = audioClips[seName];

        // Load
        if (info.clip == null)
            info.clip = (AudioClip)Resources.Load(info.resourceName);

        if (soundPlayerObj == null)
        {
            soundPlayerObj = new GameObject("SoundPlayer");
            audioSource = soundPlayerObj.AddComponent<AudioSource>();
        }

        // Play SE
        audioSource.PlayOneShot(info.clip);

        return true;
    }


    public void playBGM(string bgmName, float fadeTime)
    {
        // destory old BGM
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.destory();

        // change to fade out for current BGM
        if (curBGMPlayer != null)
        {
            curBGMPlayer.stopBGM(fadeTime);
            fadeOutBGMPlayer = curBGMPlayer;
        }

        // play new BGM
        if (audioClips.ContainsKey(bgmName) == false)
        {
            // null BGM
            curBGMPlayer = new BGMPlayer();
        }
        else
        {
            curBGMPlayer = new BGMPlayer(audioClips[bgmName].resourceName);
            curBGMPlayer.playBGM(fadeTime);
        }
    }

    public void playBGM()
    {
        if (curBGMPlayer != null && curBGMPlayer.hadFadeOut() == false)
            curBGMPlayer.playBGM();
        if (fadeOutBGMPlayer != null && fadeOutBGMPlayer.hadFadeOut() == false)
            fadeOutBGMPlayer.playBGM();
    }

    public void pauseBGM()
    {
        if (curBGMPlayer != null)
            curBGMPlayer.pauseBGM();
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.pauseBGM();
    }

    public void stopBGM(float fadeTime)
    {
        if (curBGMPlayer != null)
            curBGMPlayer.stopBGM(fadeTime);
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.stopBGM(fadeTime);
    }


    public bool getIsMute()
    {
        return IsMute;
    }


    public void setIsMute(bool f)
    {

        IsMute = f;
    }


}