using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARRaycastManager : MonoBehaviour
{
    [SerializeField, Tooltip("AR空間に表示させたいプレハブを登録")] GameObject _ARobj;

    private GameObject _SpawnedObj;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
       if (Input.touchCount > 0)
        {
            Vector2 _TouchPosition = Input.GetTouch(0).position;
            
            if (raycastManager.Raycast(_TouchPosition, hits, TrackableType.Planes))
            {

            }
        } 
    }
}
