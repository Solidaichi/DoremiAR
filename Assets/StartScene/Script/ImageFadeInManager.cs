using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeInManager : MonoBehaviour
{

    private GameObject imageObj; // 自分のオブジェクト取得用変数
    [SerializeField] private float fadeStart = 1f; // フェード開始時間
    private bool fadeIn = true; // trueの場合はフェードイン
    [SerializeField] private float fadeSpeed = 1f; // フェード速度指定


    // Start is called before the first frame update
    void Start()
    {
        imageObj = this.gameObject; // 自分のオブジェクト取得
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
            }
        }
    }

    void fadeInFunction()
    {
        if (imageObj.GetComponent<Image>().color.a < 255)
        {
            UnityEngine.Color tmp = imageObj.GetComponent<Image>().color;
            tmp.a = tmp.a + (Time.deltaTime * fadeSpeed);
            imageObj.GetComponent<Image>().color = tmp;
        }
    }
}