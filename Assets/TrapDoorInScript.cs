using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorInScript : MonoBehaviour
{
    public GameObject player;
    public float distThreshold = 10f;
    public Vector3 exitLocation = new Vector3(0,1,0);
    bool isHonking = false; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void SetExitLocation(Vector3 location){
        exitLocation = location;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if(dist <= distThreshold && !isHonking){
            StartCoroutine("Honk", dist);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.name == "Player"){
            player.transform.position = exitLocation;
        }
    }

    IEnumerator Honk(float distance){
        isHonking = true;
        // gameObject.GetComponent<Renderer>().enabled = isHonking;
        FindObjectOfType<AudioManager>().Play("TrapDoorNearby");
        yield return (new WaitForSeconds(distance/8+0.1f));
        isHonking = false;
        // gameObject.GetComponent<Renderer>().enabled = isHonking;
    }


}
