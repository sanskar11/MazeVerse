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
    public GameObject EndCardUI;
    public Material defaultMat;
    public Material immuneMat;
    public SkinnedMeshRenderer playerBodyMesh;

    void Start(){
        playerBodyMesh = GameObject.Find("Player").transform.Find("Model").gameObject.GetComponent<SkinnedMeshRenderer>();
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
        if(collisionInfo.collider.tag == "Lava Ball"){
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
            Time.timeScale = 0f;
            EndCardUI.SetActive(true);
        }
    }

    IEnumerator HideUnhideWalls(){
        toggleWalls(false);
        yield return (new WaitForSeconds(3));
        toggleWalls(true);
    }

    IEnumerator ActivateDeactivateImmunity(){
        immunityOrbCollected = true;
        setPlayerMaterial(immuneMat);
        yield return (new WaitForSeconds(20));
        setPlayerMaterial(defaultMat);
        immunityOrbCollected = false;
    }

    void setPlayerMaterial(Material mat){
        Material[] matArray = playerBodyMesh.materials;
        matArray[0] = mat;
        playerBodyMesh.materials = matArray;
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
