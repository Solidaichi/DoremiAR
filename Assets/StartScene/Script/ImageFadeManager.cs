using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeManager : MonoBehaviour
{

    private GameObject imageObj; // 自分のオブジェクト取得用変数
    [SerializeField] private float fadeStart = 1f; // フェード開始時間
    private bool fadeIn; // trueの場合はフェードイン
    [SerializeField] private float fadeSpeed = 1f; // フェード速度指定

    // Start is called before the first frame update
    void Start()
    {
        imageObj = this.gameObject; // 自分のオブジェクト取得
        fadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeStart > 0f)
        {
            fadeStart -= Time.deltaTime;
        }
        else
        {
            if (fadeIn)
            {
                fadeInFunction();
            }else
            {
                fadeOutFunction();
            }
        }
    }

    private void fadeOutFunction()
    {

        /*if (imageObj.GetComponent<Image>().color.a > 254)
        {
            Debug.Log("fadeout");
            Color tmp = imageObj.GetComponent<Image>().color;
            tmp.a = tmp.a - (Time.deltaTime * fadeSpeed);
            imageObj.GetComponent<Image>().color = tmp;
        }*/
        Debug.Log("fadeout");
        Color tmp = imageObj.GetComponent<Image>().color;
        tmp.a = tmp.a - (Time.deltaTime * fadeSpeed);
        imageObj.GetComponent<Image>().color = tmp;
    }

    void fadeInFunction()
    {
        if (imageObj.GetComponent<Image>().color.a < 255)
        {
            
            Color tmp = imageObj.GetComponent<Image>().color;
            tmp.a = tmp.a + (Time.deltaTime * fadeSpeed);
            imageObj.GetComponent<Image>().color = tmp;
        }

        Invoke("FadeSwtFunction", 6f);
    }

    void FadeSwtFunction()
    {
        fadeIn = false;
    }
}