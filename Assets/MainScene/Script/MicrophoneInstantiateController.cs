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

    [SerializeField]
    private string[] arObjTags = {"Rock", "Wood", "Parrot"};
    

    private void Awake()
    {
        AwakeObjArray(arObjTags[0], arObjects_Rock);
        AwakeObjArray(arObjTags[1], arObjects_Wood);
        AwakeObjArray(arObjTags[2], arObjects_Parrot);
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
            for (int i = 0; i < arObjects_Rock.Length; i++)
            {
                foreach (Transform child in arObjects_Rock[i].transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            
        }else if (s.Contains("F") || s.Contains("G") || s == "A+")
        {
            for (int i = 0; i < arObjects_Wood.Length; i++)
            {
                foreach (Transform child in arObjects_Wood[i].transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        else if (s.Contains("B"))
        {
            for (int i = 0; i < arObjects_Parrot.Length; i++)
            {
                foreach (Transform child in arObjects_Parrot[i].transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }      
    }

    void AwakeObjArray(string tag,  GameObject[] objArrayName)
    {
        var natureObj = GameObject.FindWithTag(tag).transform;
        int i = 0;
        foreach (Transform child in natureObj.transform)
        {            
            objArrayName[i] = child.gameObject;
            Debug.Log(objArrayName[i]);
            child.gameObject.SetActive(false);
        }
    }
}
