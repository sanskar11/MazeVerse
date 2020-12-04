using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBallScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.name == "Floor"){
            StartCoroutine("WaitAndDestroyBall");
        }
    }

    IEnumerator WaitAndDestroyBall(){
        FindObjectOfType<AudioManager>().Play("LavaBallDestroy");
        yield return (new WaitForSeconds(2));
        Destroy(gameObject);
    }
}
