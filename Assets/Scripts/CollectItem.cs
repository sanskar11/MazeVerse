using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    public bool keyCollected = false;
    List<GameObject> walls;
    public Healthbar hb;
    public Image keyimg;
    private bool canReduceHealth = true;
    
    void Start(){
        walls = GameObject.Find("Main Camera").GetComponent<MazeLoader>().walls;
        hb = GameObject.Find("Healthbar").GetComponent<Healthbar>();
        keyimg = GetComponent<Image>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Key"){
            keyCollected = true;
            Destroy(collisionInfo.gameObject);
            Color temp = keyimg.color; //we need to assign to a variable and do it
            temp.a = 1f;
            keyimg.color=temp;
            if(canReduceHealth)
            {
                hb.GainHealth(50);
                canReduceHealth=false;
            }
        }
        if(collisionInfo.collider.tag == "Door"){
            if(keyCollected)
            {
                Destroy(collisionInfo.gameObject);
                Color temp = keyimg.color;
                temp.a = 0.4f;
                keyimg.color=temp;
                if(canReduceHealth)
                {
                    hb.TakeDamage(50);
                    canReduceHealth=false;
                }
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
