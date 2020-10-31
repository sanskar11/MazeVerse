using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

static class Values
{
    public const int DOOR = -1;
    public const int WALL = 0;
    public const int KEY = 1;
    public const int INV_ORB = 11;
    public const int o2_ORB = 12;
    public const int o3_ORB = 13;
    public const float KEY_HEIGHT = 0.5f;
    public const float ORB_HEIGHT = 0;
    public const float WALL_HEIGHT = 3;
}

public class MazeLoader : MonoBehaviour
{
    public string [] myArray;
    public List<List<int>> myli;
    public List<int> temp;
    public static int rows = 15;
    public static int cols = 15;
    public static float xOffset = -1;
    public static float zOffset = -1;
    public float topCamPosX;
    public float topCamPosZ;
    static float yOffset = 0.5f;
    int[, ,] mat = new int[rows,cols,4];
    List<int>[,] mazeObjects = new List<int>[rows,cols];
    float BOXSIZE = 3f;
    public GameObject wall;
    public GameObject door;
    public GameObject key;
    public GameObject inv_orb;
    public List<GameObject> walls;
    public string filePath;
    public bool startCompleted = false;
    float startTime;
    void Start(){
        topCamPosX = xOffset + BOXSIZE*rows/2;
        topCamPosZ = zOffset + BOXSIZE*cols/2;
        print("Here"+topCamPosX.ToString());
        filePath = Application.dataPath + "/" + "maze 2 lol.txt";
        ReadFromFile();
        ConstructMaze();
        startTime = Time.time;
        // SpawnObstacle(3,2,-10,0);
        // SpawnObstacle(6,2,-10,0);
        // SpawnObstacle(3,1,-10,1);
        startCompleted = true;
    }

    public void ReadFromFile(){
        myArray = File.ReadAllLines(filePath);
        int r = 0;
        int c = 0;
        print(myArray.Length);
        int doors_till = rows*cols;
        for(int l=0; l<doors_till; l++){
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
            if(mazeObjects[i,j]==null)
                mazeObjects[i,j] = new List<int>();
            mazeObjects[i,j].Add(val);
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

    public void SpawnMazeObject(float x, float y, float z, int value){
        GameObject obs = null;
        if(value==Values.KEY)
            obs = Instantiate(key, new Vector3(x,yOffset+Values.KEY_HEIGHT,z), Quaternion.Euler(0,0,0));
        if(value==Values.INV_ORB)
            obs = Instantiate(inv_orb, new Vector3(x,yOffset+Values.ORB_HEIGHT,z), Quaternion.Euler(0,0,0));
        return;
    }

    public void ConstructMaze(){
        for(int i=0; i<rows; i++){
            for(int j=0; j<cols; j++){
                float x = (float)i*BOXSIZE + xOffset;
                float z = (float)j*BOXSIZE + zOffset;
                float y = yOffset+Values.WALL_HEIGHT/2;
                // if(j<2)
                //     continue;
                // if(mat[i,j,0]<=0)
                //     SpawnObstacle(x,y,z,1,mat[i,j,0]);
                // if(mat[i,j,1]<=0)
                //     SpawnObstacle(x,y,z+BOXSIZE,0);
                // if(mat[i,j,2]<=0)
                //     SpawnObstacle(x+BOXSIZE,y,z,1);
                // if(mat[i,j,3]<=0)
                //     SpawnObstacle(x,y,z,0);
                SpawnObstacle(x,y,z,1,mat[i,j,0]);
                SpawnObstacle(x,y,z+BOXSIZE,0,mat[i,j,1]);
                SpawnObstacle(x+BOXSIZE,y,z,1,mat[i,j,2]);
                SpawnObstacle(x,y,z,0,mat[i,j,3]);
                List<int> li = mazeObjects[i,j];
                if(li!=null){
                    print("key");
                    for(int l=0; l<li.Count; l++)
                        SpawnMazeObject(x+(BOXSIZE/2),y,z+(BOXSIZE/2),li[l]);
                }
                // if(j==2)
                //     return;
            }
        }
    }

}
