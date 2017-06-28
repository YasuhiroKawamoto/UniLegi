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

        //SEの登録
        audioClips.Add("se001", new AudioClipInfo("hit", "se001"));//被弾音SE
        audioClips.Add("se002", new AudioClipInfo("Sammon", "se002"));//召喚音SE
        audioClips.Add("se003", new AudioClipInfo("Dead", "se003"));//死亡音SE
        audioClips.Add("se004", new AudioClipInfo("Button", "se004"));//ボタンSE
        audioClips.Add("se005", new AudioClipInfo("Fanfare", "se005"));//勝利時SE
        audioClips.Add("se006", new AudioClipInfo("Lose", "se006"));//敗北時SE
        audioClips.Add("se007", new AudioClipInfo("Pick", "se007"));//持ち上げSE
        audioClips.Add("se008", new AudioClipInfo("GaugeMax", "se008"));//ユニオンゲージMAX音SE
        audioClips.Add("se009", new AudioClipInfo("Guard", "se009"));//ガード音SE
        audioClips.Add("se010", new AudioClipInfo("AlartSE", "se010"));//アラートオンSE
        audioClips.Add("se011", new AudioClipInfo("Dragon", "se011"));//ドラゴンSE
        audioClips.Add("se012", new AudioClipInfo("LilithSpear", "se012"));//リリスSE
        audioClips.Add("se013", new AudioClipInfo("PatadinSlash", "se013"));//パラディンSE
        audioClips.Add("se014", new AudioClipInfo("Thunder", "se014"));//リッチSE
        audioClips.Add("se015", new AudioClipInfo("Water", "se015"));//クラケンSE
        audioClips.Add("se016", new AudioClipInfo("Gage", "se016"));//オバロゲージSE
        audioClips.Add("se017", new AudioClipInfo("SuperUnion", "se017"));//超合体SE


        //BGMの登録
        audioClips.Add("BGM001", new AudioClipInfo("TitleBGM", "BGM001"));//タイトルBGM
        audioClips.Add("BGM002", new AudioClipInfo("SelectBGM", "BGM002"));//セレクトシーンBGM
        audioClips.Add("BGM003", new AudioClipInfo("BattleBGM2", "BGM003"));//ノーマルBGM
        audioClips.Add("BGM004", new AudioClipInfo("TutorialBGM", "BGM004"));//ハードBGM
        audioClips.Add("BGM005", new AudioClipInfo("Infinity", "BGM005"));//インフィニBGM
        //audioClips.Add("BGM006", new AudioClipInfo("", ""));
        //audioClips.Add("BGM007", new AudioClipInfo("", ""));


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