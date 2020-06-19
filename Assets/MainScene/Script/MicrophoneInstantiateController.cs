using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInstantiateController : MonoBehaviour
{
    // マイクからの音を拾う
    private new AudioSource audio;
    //private string mic_name = "UAB-80";

    void Start()
    {

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
        //Debug.Log("A+");

        if (s == "A2")
        {
            var natureObj = GameObject.FindWithTag("A2").transform;
            Debug.Log(natureObj.name);
            foreach (Transform child in natureObj.transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
        }
        else if (s == "D2")
        {
            var natureObj2 = GameObject.FindWithTag("D2").transform;
            foreach (Transform child in natureObj2.transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
        }
        else if (s == "C2")
        {
            var natureObj3 = GameObject.FindWithTag("C2").transform;
            foreach (Transform child in natureObj3.transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
        }
    }
}
