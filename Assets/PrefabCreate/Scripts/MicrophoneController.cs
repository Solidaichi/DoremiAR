using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))] //AudioSourceは必須.
[DisallowMultipleComponent]     // 複数アタッチできないようにするため
public class MicrophoneController : MonoBehaviour
{
    NoteNameDetector noteName;
    Text noteText;

    void Start()
    {
        noteName = new NoteNameDetector();
        noteText = GameObject.FindWithTag("noteText").GetComponent<Text>();
        AudioSource aud = GetComponent<AudioSource>();
        // マイク名、ループするかどうか、AudioClipの秒数、サンプリングレート を指定する
        aud.clip = Microphone.Start(null, true, 10, 44100);
        aud.Play();
    }

    void FixedUpdate()
    {
        float[] spectrum = new float[256];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        var maxIndex = 0;
        var maxValue = 0.0f;
        for (int i = 0; i < spectrum.Length; i++)
        {
            var val = spectrum[i];
            if (val > maxValue)
            {
                maxValue = val;
                maxIndex = i;
            }
        }
        // maxValue が最も大きい周波数成分の値で、
        // maxIndex がそのインデックス。欲しいのはこっち。

        //音声出力のサンプリングレートをF,`spectrum`の長さをQとすると * *`spectrum[N]`には`N* F/ 2 / Q`Hzの周波数成分が含まれています。
        var freq = maxIndex * AudioSettings.outputSampleRate / 2 / spectrum.Length;

        string noteNamesText = noteName.GetNoteName(freq);
        noteText.text = noteNamesText;
    }
}
