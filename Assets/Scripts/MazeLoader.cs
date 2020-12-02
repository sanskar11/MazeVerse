using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

static class Values
{
    public const int DOOR = -1;
    public const int WALL = 0;
    public const int KEY = 1;
    public const int INV_ORB = 11;
    public const int IMM_ORB = 12;
    public const int o3_ORB = 13;
    public const int GHOST = 21;
    public const int StartPlatform = 31;
    public const int EndPlatform = 32;
    public const int TrapDoorIn = 33;
    public const int LAVA_BALL = 41;
    public const float KEY_HEIGHT = 0.5f;
    public const float ORB_HEIGHT = 0;
    public const float GHOST_HEIGHT = 0.25f;
    public const float WALL_HEIGHT = 3;
    public const float LAVA_BALL_HEIGHT = 5f;
}

public class MazeLoader : MonoBehaviour
{
    public string [] myArray;
    public List<List<int>> myli;
    public List<int> temp;
    public static int max_rows = 50;
    public static int max_cols = 50;
    public int rows;
    public int cols;
    public static float xOffset = -1;
    public static float zOffset = -1;
    public static float g = 9.8f;
    public float lavaThrowPeriod = 5f;
    public float lavaAirTime = 2f;
    public float topCamPosX;
    public float topCamViewSize;
    public float topCamPosZ;
    static float yOffset = 0.5f;
    int[, ,] mat = new int[max_rows,max_cols,4];
    List<int>[,] mazeObjects = new List<int>[max_rows,max_cols];
    float BOXSIZE = 3f;
    public GameObject wall;
    public GameObject door;
    public GameObject key;
    public GameObject inv_orb;
    public GameObject imm_orb;
    public GameObject ghost;
    public GameObject start_platform;
    public GameObject end_platform;
    public GameObject trap_door_in;
    public GameObject lava_ball;
    public GameObject player;
    public bool waitingThrow = false;
    public List<GameObject> walls;
    public string filePath;
    public bool startCompleted = false;
    float startTime;

    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnLevelFinishedLoading;
    // }

    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    // }

    // void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    // {
    // }

    void Start(){
        filePath = Application.dataPath + "/Mazes/" + PlayerPrefs.GetString("mazename") + ".txt";
        print(filePath);
        ReadFromFile();
        ConstructMaze();
        topCamPosX = xOffset + BOXSIZE*rows/2;
        topCamPosZ = zOffset + BOXSIZE*cols/2;
        topCamViewSize = yOffset + Mathf.Max(rows,cols)*2 + 1;
        startTime = Time.time;
        SpawnMazeObject(xOffset+BOXSIZE/2,0,zOffset+BOXSIZE/2,Values.StartPlatform);
        SpawnMazeObject(xOffset+BOXSIZE*(rows-1)+BOXSIZE/2,0,zOffset+BOXSIZE*(cols-1)+BOXSIZE/2,Values.EndPlatform);
        startCompleted = true;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(!waitingThrow){
            StartCoroutine("ThrowLavaBall");
        } 
    }

    IEnumerator ThrowLavaBall(){
        waitingThrow = true;

        float landx = player.transform.position[0];
        float landz = player.transform.position[2];
        float initx = xOffset+(BOXSIZE/2)*rows;
        float initz = zOffset+(BOXSIZE/2)*cols;
        float t = lavaAirTime;
        GameObject obs = SpawnMazeObject(initx,0,initz,Values.LAVA_BALL);
        obs.GetComponent<Rigidbody>().velocity = new Vector3((landx-initx)/t,g*t/2 - Values.LAVA_BALL_HEIGHT/t,(landz-initz)/t); 

        yield return (new WaitForSeconds(lavaThrowPeriod));
        waitingThrow = false;
    }

    public void ReadFromFile(){
        myArray = File.ReadAllLines(filePath);
        int r = 0;
        int c = 0;
        print(myArray.Length);
        rows = int.Parse(myArray[0].Split(' ')[0]);
        cols = int.Parse(myArray[1].Split(' ')[0]);
        int doors_till = rows*cols+2;
        for(int l=2; l<doors_till; l++){
            string line = myArray[l];
            temp = new List<int>();
            string [] nums = line.Split(' ');
            for(int i=0; i<4; i++){
                mat[r,c,i] = int.Parse(nums[i]);
            }
            c = (c+1)%cols;
            if(c==0){
                r = (r+1)%rows;
            }
        }
        for(int l=doors_till; l<myArray.Length; l++){
            string line = myArray[l];
            string [] nums = line.Split(' ');

            int i = int.Parse(nums[0]);
            int j = int.Parse(nums[1]);
            int val = int.Parse(nums[2]);

            if(val == Values.TrapDoorIn){
                float x = (float)i*BOXSIZE + xOffset + BOXSIZE/2;
                float z = (float)j*BOXSIZE + zOffset + BOXSIZE/2;
                GameObject obs = SpawnMazeObject(x,yOffset,z,val);
                float exitx = (float)(int.Parse(nums[3]))*BOXSIZE + xOffset + BOXSIZE/2;
                float exitz = (float)(int.Parse(nums[4]))*BOXSIZE + zOffset + BOXSIZE/2;
                obs.GetComponent<TrapDoorInScript>().SetExitLocation(new Vector3(exitx,3f,exitz));
            }

            else{
                if(mazeObjects[i,j]==null)
                    mazeObjects[i,j] = new List<int>();
                mazeObjects[i,j].Add(val);
            }
        }
    }

    public void SpawnObstacle(float x, float y, float z, int align, int value){
        // GameObject obs = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // obs.transform.localScale = new Vector3(3,height,0.1f);
        if(value>0)
            return;
        GameObject obs = null;
        if(value==Values.WALL)
            obs = Instantiate(wall, transform.position, Quaternion.Euler(0,0,0));
        else if(value==Values.DOOR)
            obs = Instantiate(door, transform.position, Quaternion.Euler(0,0,0));

        if(align==1){
            obs.transform.rotation = Quaternion.Euler(0,90,0);
            obs.transform.position = new Vector3(x,y,z+BOXSIZE/2);
        }
        else{
            obs.transform.position = new Vector3(x+BOXSIZE/2,y,z);
        }
        if(value==Values.WALL)
            walls.Add(obs);
    }

    public GameObject SpawnMazeObject(float x, float y, float z, int value){
        GameObject obs = null;
        var rot = Quaternion.Euler(0,0,0);
        if(value==Values.KEY)
            obs = Instantiate(key, new Vector3(x,yOffset+Values.KEY_HEIGHT,z), rot);
        if(value==Values.INV_ORB)
            obs = Instantiate(inv_orb, new Vector3(x,yOffset+Values.ORB_HEIGHT,z), rot);
        if(value==Values.IMM_ORB)
            obs = Instantiate(imm_orb, new Vector3(x,yOffset+Values.ORB_HEIGHT,z), rot);
        if(value==Values.GHOST)
            obs = Instantiate(ghost, new Vector3(x,yOffset+Values.GHOST_HEIGHT,z), rot);
        if(value==Values.StartPlatform)
            obs = Instantiate(start_platform, new Vector3(x,yOffset,z), rot);
        if(value==Values.EndPlatform)
            obs = Instantiate(end_platform, new Vector3(x,yOffset,z), rot);
        if(value==Values.TrapDoorIn)
            obs = Instantiate(trap_door_in, new Vector3(x,yOffset,z), rot);
        if(value==Values.LAVA_BALL)
            obs = Instantiate(lava_ball, new Vector3(x,yOffset+Values.LAVA_BALL_HEIGHT,z), rot);
        return obs;
    }

    public void ConstructMaze(){
        for(int i=0; i<rows; i++){
            for(int j=0; j<cols; j++){
                float x = (float)i*BOXSIZE + xOffset;
                float z = (float)j*BOXSIZE + zOffset;
                float y = yOffset+Values.WALL_HEIGHT/2;

                SpawnObstacle(x,y,z,1,mat[i,j,0]);
                SpawnObstacle(x,y,z+BOXSIZE,0,mat[i,j,1]);
                SpawnObstacle(x+BOXSIZE,y,z,1,mat[i,j,2]);
                SpawnObstacle(x,y,z,0,mat[i,j,3]);
                List<int> li = mazeObjects[i,j];
                if(li!=null){
                    for(int l=0; l<li.Count; l++)
                        SpawnMazeObject(x+(BOXSIZE/2),y,z+(BOXSIZE/2),li[l]);
                }
            }
        }
    }

}
