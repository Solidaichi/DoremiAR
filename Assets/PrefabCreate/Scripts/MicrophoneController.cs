﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[DisallowMultipleComponent]     // 複数アタッチできないようにするため
public class MicrophoneController : MonoBehaviour
{
    // 波形を描画する
    public LineRenderer line;

    // マイクからの音を拾う
    private new AudioSource audio;
    //private string mic_name = "UAB-80";

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

        audio = GetComponent<AudioSource>();

        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 10, 44100);  // マイクからのAudio-InをAudioSourceに流す
        audio.loop = true;                                      // ループ再生にしておく
        //audio.mute = true;                                      // マイクからの入力音なので音を流す必要がない
        while (!(Microphone.GetPosition("") > 0)) { }             // マイクが取れるまで待つ。空文字でデフォルトのマイクを探してくれる
        audio.Play();
    }

    void Update()
    {
        // 諸々の解析
        float hertz = NoteNameDetector.AnalyzeSound(audio, 1024, 0.04f);
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
