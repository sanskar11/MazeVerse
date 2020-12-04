using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topViewFloor : MonoBehaviour
{
    camerachange cam_change;
    // private bool canChange=true;
    void Start()
    {
        cam_change = GameObject.Find("Player").GetComponent<camerachange>();
    }
    void OnTriggerEnter()
    {
        cam_change.toggleCam(true,false);
    }

    void OnTriggerExit()
    {
        cam_change.toggleCam(false,false);
    }
}
