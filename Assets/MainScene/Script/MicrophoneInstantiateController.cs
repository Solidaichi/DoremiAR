using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInstantiateController : MonoBehaviour
{
    // マイクからの音を拾う
    private new AudioSource audio;
    //private string mic_name = "UAB-80";

    private GameObject[] arObjects_Wood, arObjects_Rock, arObjects_Parrot;
    private string[] arObjTags = {"Rock", "Wood", "Parrot"};

    private void Awake()
    {
        for (int i = 0; i < arObjTags.Length; i++)
        {
            int length = 0;
            var natureObj = GameObject.FindWithTag(arObjTags[i]).transform;
            foreach (Transform child in natureObj.transform)
            {
                

            }
        }

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

        if (s.Contains("C") || s.Contains("D") || s.Contains("E"))
        {
            string tagName = "Rock" +  
            var natureObj = GameObject.FindWithTag("Rock").transform;
            Debug.Log(natureObj.name);
            foreach (Transform child in natureObj.transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
        }else if (s.Contains("F") || s.Contains("G") || s == "A+")
        {
            var natureObj = GameObject.FindWithTag("Wood").transform;
            Debug.Log(natureObj.name);
            foreach (Transform child in natureObj.transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
        }else if (s.Contains("B"))
        {
            var natureObj = GameObject.FindWithTag("Parrot").transform;
            Debug.Log(natureObj.name);
            foreach (Transform child in natureObj.transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
        }
        
    }

    
}
