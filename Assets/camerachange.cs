using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerachange : MonoBehaviour
{
 public Camera cam1;
 public Camera cam2;
public Healthbar hb;
private bool damage = false;
 
 void Start() {
     cam1.enabled = true;
     cam2.enabled = false;
     hb = GameObject.Find("Healthbar").GetComponent<Healthbar>();
 }
 
 void Update() {
 
     if (Input.GetKeyDown(KeyCode.C)) {
         cam1.enabled = !cam1.enabled;
         cam2.enabled = !cam2.enabled;
         damage=true;
     }
     if(Input.GetKeyUp(KeyCode.C)) {
         cam1.enabled = !cam1.enabled;
         cam2.enabled = !cam2.enabled;
         damage=false;
     }
     if(damage==true)
     {
         hb.TakeDamage(2);
     }
 }
}