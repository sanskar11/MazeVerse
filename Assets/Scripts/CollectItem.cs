using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public bool keyCollected = false;
    List<GameObject> walls;
    public Healthbar hb;

    private bool canReduceHealth = true;
    
    void Start(){
        walls = GameObject.Find("Main Camera").GetComponent<MazeLoader>().walls;
        hb = GameObject.Find("Healthbar").GetComponent<Healthbar>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Key"){
            Destroy(collisionInfo.gameObject);
            keyCollected = true;
            hb.TakeDamage(50);
        }
        if(collisionInfo.collider.tag == "Door"){
            if(keyCollected)
            {
                Destroy(collisionInfo.gameObject);
            }
            else
            {
                if(canReduceHealth)
                {
                    hb.TakeDamage(50);
                    canReduceHealth=false;
                }
            }
        }
        if(collisionInfo.collider.tag == "Invisibility Orb"){
            StartCoroutine("HideUnhideWalls");
            Destroy(collisionInfo.gameObject);
        }
    }

    IEnumerator HideUnhideWalls(){
        toggleWalls(false);
        yield return (new WaitForSeconds(3));
        toggleWalls(true);
    }

    void toggleWalls(bool value){
        for(int i=0; i<walls.Count; i++){
            walls[i].GetComponent<Renderer>().enabled = value;
        }
    }

    void LateUpdate()
    {
        canReduceHealth=true;
    }
}
