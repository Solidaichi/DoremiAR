using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoStopController : MonoBehaviour
{
    [SerializeField] private GameObject placeOnPlaneObj;

    [HideInInspector] public PlaceOnPlane placeOnPlane;

    private void Start()
    {
        placeOnPlane = placeOnPlaneObj.GetComponent<PlaceOnPlane>();
    }

    public void PianoStop()
    {
        placeOnPlane.pianoStartBool = false;
        
    }
}
