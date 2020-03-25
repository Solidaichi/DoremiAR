using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageRecognitionManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The camera to set on the world space UI canvas for each instantiated image info.")]
    Camera m_WorldSpaceCanvasCamera;

    public Camera worldSpaceCanvasCamera
    {
        get { return m_WorldSpaceCanvasCamera;  }
        set { m_WorldSpaceCanvasCamera = value; }
    }

    [SerializeField]
    [Tooltip("If an image is detected but no source texture can be found, this texture is used instead.")]
    Texture2D m_DefaultTexture;

    public Texture2D DefaultTexture
    {
        get { return m_DefaultTexture; }
        set { m_DefaultTexture = value; }
    }

    private ARTrackedImageManager _arTrackedImageManager;

    void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    void UpdataInfo(ARTrackedImage trackedImage)
    {
        var canvas = trackedImage.GetComponentInChildren<Canvas>();
        canvas.worldCamera = worldSpaceCanvasCamera;

        var text = canvas.GetComponentInChildren<Text>();

        text.text = string.Format(
            "{0}\ntrackingState: {1}\nGUID: {2}\nReference size: {3} cm\nDetected size: {4} cm",
            trackedImage.referenceImage.name,
            trackedImage.trackingState,
            trackedImage.referenceImage.guid,
            trackedImage.referenceImage.size * 100f,
            trackedImage.size * 100f
            );

        var planePerentGo = trackedImage.transform.GetChild(0).gameObject;
        var planeGo = planePerentGo.transform.GetChild(0).gameObject;

        if (trackedImage.trackingState != TrackingState.None)
        {
            planeGo.SetActive(true);

            trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);

            var material = planeGo.GetComponentInChildren<MeshRenderer>().material;
            material.mainTexture = (trackedImage.referenceImage.texture == null) ? DefaultTexture : trackedImage.referenceImage.texture;
        }else
        {
            planeGo.SetActive(false);
        }
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Debug.Log(trackedImage.name);
            trackedImage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            UpdataInfo(trackedImage);

        }

        foreach (var trackedImage in args.updated)
        {
            UpdataInfo(trackedImage);
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
