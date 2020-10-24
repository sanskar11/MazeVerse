using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MazeLoader : MonoBehaviour
{
    public string [] myArray;
    public List<List<int>> myli;
    public List<int> temp;
    static int rows = 15;
    static int cols = 15;
    static float xOffset = 0;
    static float zOffset = -10;
    static float height = 3;
    static float yOffset = 0.5f+height/2;
    bool[, ,] mat = new bool[rows,cols,4];
    float BOXSIZE = 3f;
    public GameObject wall;
    public string filePath;
    void Start(){
        filePath = Application.dataPath + "/" + "maze.txt";
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
        foreach(string line in myArray){
            temp = new List<int>();
            string [] nums = line.Split(' ');
            for(int i=0; i<4; i++){
                mat[r,c,i] = int.Parse(nums[i]) != 0;
            }
            c = (c+1)%cols;
            if(c==0){
                r = (r+1)%rows;
            }
        }
    }

    public void SpawnObstacle(float x, float y, float z, int align){
        // GameObject obs = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // obs.transform.localScale = new Vector3(3,height,0.1f);
        GameObject obs = Instantiate(wall, transform.position, Quaternion.Euler(0,0,0));
        if(align==1){
            obs.transform.rotation = Quaternion.Euler(0,90,0);
            obs.transform.position = new Vector3(x,y,z+BOXSIZE/2);
        }
        else{
            obs.transform.position = new Vector3(x+BOXSIZE/2,y,z);
        }
    }

    public void ConstructMaze(){
        for(int i=0; i<rows; i++){
            for(int j=0; j<cols; j++){
                float x = (float)i*BOXSIZE + xOffset;
                float z = (float)j*BOXSIZE + zOffset;
                float y = yOffset;
                // if(j<2)
                //     continue;
                if(!mat[i,j,0])
                    SpawnObstacle(x,y,z,1);
                if(!mat[i,j,1])
                    SpawnObstacle(x,y,z+BOXSIZE,0);
                if(!mat[i,j,2])
                    SpawnObstacle(x+BOXSIZE,y,z,1);
                if(!mat[i,j,3])
                    SpawnObstacle(x,y,z,0);
                // if(j==2)
                //     return;
            }
        }
    }

}
