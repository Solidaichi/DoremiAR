using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;




[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField, Tooltip("AR空間に表示するプレハブを登録")] GameObject arObj;
    public GameObject birdSoundObj, windSoundObj, uiObj;

    [HideInInspector]public bool pianoStartBool;

    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    [HideInInspector] public AudioSource birdSound, windSound, pianoSound;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool pianoBtn;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Start()
    {
        birdSound = birdSoundObj.GetComponent<AudioSource>();
        windSound = windSoundObj.GetComponent<AudioSource>();
        pianoSound = this.GetComponent<AudioSource>();

        pianoBtn = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes))
            {
                // Raycastの衝突情報は距離によってソートされるため、0番目が最も近い場所でヒットした情報となります
                var hitPose = hits[0].pose;

                if (spawnedObject)
                {
                    spawnedObject.transform.position = hitPose.position;
                }
                else
                {
                    spawnedObject = Instantiate(arObj, hitPose.position, Quaternion.identity);
                    //birdSound.Play();
                    //windSound.Play();

                    pianoBtn = true;
                    
                }
            }
        }

        if (pianoBtn)
        {
            uiObj.SetActive(true);
        }

        
    }
}
