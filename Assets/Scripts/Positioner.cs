using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioner : MonoBehaviour
{
    public GameObject cam;
    MazeLoader ml;
    bool pos_has_been_set;

    // Start is called before the first frame update
    void Start()
    {
        ml = GameObject.Find("Main Camera").GetComponent<MazeLoader>();
        pos_has_been_set = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ml.startCompleted && !pos_has_been_set){
            float topCamPosX = ml.topCamPosX;
            float topCamViewSize = ml.topCamViewSize;
            float topCamPosZ = ml.topCamPosZ;
            print("SETTING POSITION " + topCamViewSize.ToString());
            cam.GetComponent<Camera>().orthographicSize = topCamViewSize; 
            cam.transform.position = new Vector3(topCamPosX, 50, topCamPosZ);
            pos_has_been_set = true;
        }
    }
}
