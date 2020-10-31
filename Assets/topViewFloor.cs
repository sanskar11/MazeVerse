using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topViewFloor : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    private bool canChange=true;
    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }
    void OnTriggerEnter()
    {
        cam2.enabled = true;
    }

    void OnTriggerExit()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }
}
