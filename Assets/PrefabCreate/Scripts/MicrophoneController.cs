using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))] //AudioSourceは必須.
[DisallowMultipleComponent]     // 複数アタッチできないようにするため
public class MicrophoneController : MonoBehaviour
{
    // 波形を描画する
    public LineRenderer line;

    // マイクからの音を拾う
    public AudioSource mic;
    private string mic_name = "UAB-80";

    // 波形描画のための変数
    private float[] wave;
    private int wave_num;
    private int wave_count;


    void Start()
    {
        // 波形描画のための変数の初期化
        wave_num = 300;
        wave = new float[wave_num];
        wave_count = 0;

        // micにマイクを割り当てる
        mic.clip = Microphone.Start(mic_name, true, 999, 44100);
        if (mic.clip == null)
        {
            Debug.LogError("Microphone.Start");
        }
        mic.loop = true;
        mic.mute = true;

        // 録音の準備が出来るまで待つ
        while (!(Microphone.GetPosition(mic_name) > 0)) { }
        mic.Play();
    }

    void Update()
    {
        // 諸々の解析
        float hertz = NoteNameDetector.AnalyzeSound(mic, 1024, 0.04f);
        float scale = NoteNameDetector.ConvertHertzToScale(hertz);
        string s = NoteNameDetector.ConvertScaleToString(scale);
        Debug.Log(hertz + "Hz, Scale:" + scale + ", " + s);

        // 波形描画
        wave[wave_count] = scale;
        NoteNameDetector.ScaleWave(wave, wave_count, line);
        wave_count++;
        if (wave_count >= wave_num) wave_count = 0;
    }
}
