using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecognitionManager : MonoBehaviour
{
    private ARTrackedImageManager _arTrackedImageManager;

    void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();    
    }

    public void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Debug.Log(trackedImage.name);
            trackedImage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            trackedImage.transform.rotation = Quaternion.Euler(0.0f, 90f, 0.0f);
        }
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

}
