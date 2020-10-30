using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MazeLoader : MonoBehaviour
{
    public string [] myArray;
    public List<List<int>> myli;
    public List<int> temp;
    static int rows = 3;
    static int cols = 3;
    static float xOffset = -1;
    static float zOffset = -1;
    static float height = 3;
    static float yOffset = 0.5f+height/2;
    int[, ,] mat = new int[rows,cols,4];
    int[,] keys = new int[rows,cols];
    float BOXSIZE = 3f;
    public GameObject wall;
    public GameObject door;
    public GameObject key;
    public string filePath;
    void Start(){
        filePath = Application.dataPath + "/" + "maze_dcheck.txt";
        ReadFromFile();
        ConstructMaze();
        // SpawnObstacle(3,2,-10,0);
        // SpawnObstacle(6,2,-10,0);
        // SpawnObstacle(3,1,-10,1);
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
            keys[int.Parse(nums[0]),int.Parse(nums[1])] = 1;
        }
    }

    public void SpawnObstacle(float x, float y, float z, int align, int value){
        // GameObject obs = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // obs.transform.localScale = new Vector3(3,height,0.1f);
        if(value>0)
            return;
        GameObject obs = null;
        if(value==0)
            obs = Instantiate(wall, transform.position, Quaternion.Euler(0,0,0));
        else if(value==-1)
            obs = Instantiate(door, transform.position, Quaternion.Euler(0,0,0));
        if(align==1){
            obs.transform.rotation = Quaternion.Euler(0,90,0);
            obs.transform.position = new Vector3(x,y,z+BOXSIZE/2);
        }
        else{
            obs.transform.position = new Vector3(x+BOXSIZE/2,y,z);
        }
    }

    public void SpawnKey(float x, float y, float z){
        GameObject obs = Instantiate(key, new Vector3(x,y,z), Quaternion.Euler(0,0,0));
    }

    public void ConstructMaze(){
        for(int i=0; i<rows; i++){
            for(int j=0; j<cols; j++){
                float x = (float)i*BOXSIZE + xOffset;
                float z = (float)j*BOXSIZE + zOffset;
                float y = yOffset;
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
                if(keys[i,j]>0)
                    SpawnKey(x+(BOXSIZE/2),1,z+(BOXSIZE/2));
                // if(j==2)
                //     return;
            }
        }
    }

}
