using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Emitter : MonoBehaviour {

    // Waveプレハブを格納する
    [SerializeField]
    public GameObject[] stage1;
    [SerializeField]
    public GameObject[] stage2;
    [SerializeField]
    public GameObject[] stage3;

    GameObject sceneData;

    public GameObject[][] Waves;


    // 現在のWave
    private int currentWave;

    private void Awake()
    {
            
        Waves = new GameObject[][] { stage1, stage2, stage3};
        sceneData = GameObject.Find("SceneData");
    }

    IEnumerator Start()
    {
        int stageNum = sceneData.GetComponent<SceneDataManager>().GetStage();

        Debug.Log("currentWave" + currentWave);
        Debug.Log("Waves.Length" + Waves[stageNum].Length);

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
            if (currentWave >= Waves[stageNum].Length)
            {
                SceneManager.LoadScene("ClearScene");

                break;
            }

        }
    }
}
