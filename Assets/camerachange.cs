using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerachange : MonoBehaviour
{
 public Camera cam1;
 public Camera cam2;
 public GameObject playerMarker;
 public Healthbar hb;
 private bool damage = false;
 
 void Start() {
     cam1.enabled = true;
     cam2.enabled = false;
     hb = GameObject.Find("Healthbar").GetComponent<Healthbar>();
     playerMarker = GameObject.Find("Player").transform.Find("Marker").gameObject;
     togglePlayerMarker(false);
 }
 
 void togglePlayerMarker(bool value){
     for(int i=0; i<playerMarker.transform.childCount; i++){
        playerMarker.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = value;
     }
 }

 public void toggleCam(bool value, bool damage_is_on){
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
        togglePlayerMarker(value);
        damage=(value & damage_is_on);
 }

 void Update() {
 
     if (Input.GetKeyDown(KeyCode.C)) {
         toggleCam(true,true);
     }
     if(Input.GetKeyUp(KeyCode.C)) {
         toggleCam(false,true);
     }
     if(damage)
     {
         hb.TakeDamage(2);
     }
 }
}