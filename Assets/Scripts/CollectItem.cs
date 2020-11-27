using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    public bool keyCollected = false;
    public bool immunityOrbCollected = false;
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
            FindObjectOfType<AudioManager>().Play("KeyCollect");
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
        if(collisionInfo.collider.tag == "Immunity Orb"){
            StartCoroutine("ActivateDeactivateImmunity");
            Destroy(collisionInfo.gameObject);
        }
        if(collisionInfo.collider.tag == "Ghost"){
            if(immunityOrbCollected)
                Destroy(collisionInfo.gameObject);
            else{
                FindObjectOfType<AudioManager>().Play("OOF");
                hb.TakeDamage(50);
            }
        }
        if(collisionInfo.collider.tag == "Start Platform"){
            Debug.Log("Starting");
        }
        if(collisionInfo.collider.tag == "End Platform"){
            Debug.Log("Finished Level! Congrats!");
        }
    }

    IEnumerator HideUnhideWalls(){
        toggleWalls(false);
        yield return (new WaitForSeconds(3));
        toggleWalls(true);
    }

    IEnumerator ActivateDeactivateImmunity(){
        immunityOrbCollected = true;
        yield return (new WaitForSeconds(20));
        immunityOrbCollected = false;
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
