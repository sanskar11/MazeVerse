using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.IO;
using TMPro;

public class RandomMazeGenerator : MonoBehaviour
{
    public GameObject Entry;
    public GameObject Status;
    public string saveName = "randommaze";

    public void DownloadRandomMaze(){
        string config = GetInputFromEntry(Entry);
        if(config == "")
            config = "12-12-20-2-3-2-2-1";
        string url = "http://localhost:5000/maze/"+config;
        print(url);
        Status.GetComponent<TextMeshProUGUI>().text = "Downloading...";
        DownloadFile(url);
    }

    string GetInputFromEntry(GameObject obs){
        return obs.transform.GetChild(1).gameObject.GetComponent<TMP_InputField>().text;
    }

    void DownloadFile(string url)
    {
        WebClient client = new WebClient();
        client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler( DownloadFileCompleted );
        client.DownloadFileAsync (new Uri (url), Application.dataPath + "/Mazes/" + saveName + ".txt");
    }

    void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            PlayerPrefs.SetString("mazename", saveName);
            Status.GetComponent<TextMeshProUGUI>().text = "Downloaded";
        }
    }
}
