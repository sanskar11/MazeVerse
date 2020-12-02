using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("mazename", "maze 1 lol");
    }
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
        Time.timeScale = 1f;
    }
    
    public void QuitGame(){
        Application.Quit();
    }

    public void SelectLevel(int val){
        PlayerPrefs.SetString("mazename", "maze " + (val+1).ToString() + " lol");
        Debug.Log("Set " + (val+1).ToString());
    }
}
