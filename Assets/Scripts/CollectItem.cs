using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public bool keyCollected = false;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Key"){
            Debug.Log("Hit a key!");
            Destroy(collisionInfo.gameObject);
            keyCollected = true;
        }
        if(collisionInfo.collider.tag == "Door"){
            Debug.Log("Hit a Door!");
            if(keyCollected)
                Destroy(collisionInfo.gameObject);
        }
    }
}
