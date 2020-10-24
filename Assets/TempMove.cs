using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float forwardForce = 1000f;
    public float sidewaysForce = 10f;
    // Update is called once per frame
    void Update()
    {
        // rb.AddForce(0, 0 ,forwardForce*Time.deltaTime);
        if(Input.GetKey("d")){
            rb.AddForce(sidewaysForce*Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if(Input.GetKey("a")){
            rb.AddForce(-sidewaysForce*Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if(Input.GetKey("w")){
            rb.AddForce(0, sidewaysForce*Time.deltaTime, 0, ForceMode.VelocityChange);
        }
        if(Input.GetKey("s")){
            rb.AddForce(0, -sidewaysForce*Time.deltaTime, 0, ForceMode.VelocityChange);
        }
    }
}
