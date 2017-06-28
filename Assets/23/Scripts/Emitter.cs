using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class Emitter : MonoBehaviour {

    // Waveプレハブを格納する
    [SerializeField]
    public GameObject[] stage1;
    [SerializeField]
    public GameObject[] stage2;
   
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    public GameObject Clear;
    

    public GameObject[][] Waves;

    GameObject Manager;

    int stageNum;
    // 現在のWave
    private int currentWave;

    private void Awake()
    {
            
        Waves = new GameObject[][] { stage1, stage2};
       
    }

    IEnumerator Start()
    {
        stageNum = Singleton<SceneData>.instance.getStageNumber()-1;

        Debug.Log("currentWave" + currentWave);
        Debug.Log("Waves.Length" + Waves[stageNum].Length);

        Manager = GameObject.Find("GameManager");

        // Waveが存在しなければコルーチンを終了する
        if (Waves[stageNum].Length == 0)
        {
            yield break;
        }

        while (true)
        {

            // Waveを作成する
            GameObject wave = (GameObject)Instantiate(Waves[stageNum][currentWave], this.transform.position, Quaternion.identity);

            // WaveをEmitterの子要素にする
            wave.transform.parent = transform;

            // Waveの子要素のEnemyが全て削除されるまで待機する
            while (wave.transform.childCount != 0)
            {
                yield return new WaitForEndOfFrame();
            }

            // Waveの削除
            Destroy(wave);
            currentWave++;

            // 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
            if (currentWave >= Waves[stageNum].Length && Manager.GetComponent<GameManager>().IsLose()==false)
            {
                Singleton<SoundManager>.instance.pauseBGM();
                Singleton<SoundManager>.instance.playSE("se005");

                Instantiate(Clear);

                Instantiate(Clear, canvas.transform);

                yield return new WaitForSeconds(6.0f);

                SceneManager.LoadScene("ResultScene");

                break;
            }

        }
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public int GetWaveSize()
    {
        return Waves[stageNum].Length;
    }
}
