using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBallScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.name == "Floor"){
            FindObjectOfType<AudioManager>().Play("LavaBallDestroy");
            Destroy(gameObject);
        }
    }
}
