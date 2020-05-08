using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjDestroyController : MonoBehaviour
{
    public GameObject arSessionOrigine;
    [HideInInspector] public PlaceOnPlane place; 

    // Start is called before the first frame update
    void Update()
    {
        place = arSessionOrigine.GetComponent<PlaceOnPlane>();
    }

    public void ObjDestroy()
    {
        foreach (Transform child in place.spawnedObject.transform)
        {
            // 一つずつ破棄する
            Destroy(child.gameObject);
        }
    }
}
