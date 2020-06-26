using UnityEngine;

public class MicrophoneInstantiateController : MonoBehaviour
{
    // マイクからの音を拾う
    private new AudioSource audio;
    //private string mic_name = "UAB-80";

    private GameObject arObjects_Wood, arObjects_Rock, arObjects_Parrot;
    private int rand;

    [SerializeField]
    private string[] arObjTags = { "Rock", "Wood", "Parrot" };
    [SerializeField]
    private Transform[] arChildObjects_Wood, arChildObjects_Rock, arChildObjects_Parrot;

    private void Awake()
    {
        AwakeObjArray(arObjTags[0]);
        AwakeObjArray(arObjTags[1]);
        AwakeObjArray(arObjTags[2]);
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
        //Debug.Log(hertz + "Hz, Scale:" + scale + ", " + s);
        //Debug.Log("A+");

        if (s.Contains("C") || s.Contains("D") || s.Contains("E"))
        {
            rand = Random.Range(0, arChildObjects_Rock.Length - 1);
            if (!arChildObjects_Rock[rand].gameObject.activeSelf)
            {
                arChildObjects_Rock[rand].gameObject.SetActive(true);
            }

        }
        else if (s.Contains("F") || s.Contains("G") || s == "A+")
        {
            
            if (!arChildObjects_Wood[rand].gameObject.activeSelf)
            {
                arChildObjects_Wood[rand].gameObject.SetActive(true);
            }

        }
        else if (s.Contains("B"))
        {
            rand = Random.Range(0, arChildObjects_Parrot.Length - 1);
            {
                arChildObjects_Parrot[rand].gameObject.SetActive(true);
            }
        }
    }

     void AwakeObjArray(string tag)
    {
        var natureTransform = GameObject.FindWithTag(tag).transform;
        int i = 0;

        foreach (Transform child in natureTransform.transform)
        {
            arChildObjects_Wood[i] = child;
            child.gameObject.SetActive(false);
            i++;
        }       
    }
}
