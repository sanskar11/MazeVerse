﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public void QuitGame(){
        Application.Quit();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        print("LOL");
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu(){
        Vector3 playerLocation = GameObject.Find("Player").transform.position;
        string prefix = PlayerPrefs.GetString("mazename") + " location";
        PlayerPrefs.SetFloat(prefix+"x", playerLocation.x);
        PlayerPrefs.SetFloat(prefix+"y", playerLocation.y);
        PlayerPrefs.SetFloat(prefix+"z", playerLocation.z);
        PlayerPrefs.SetFloat(prefix+" currenttime", GameObject.Find("Main Camera").GetComponent<MazeLoader>().timeElapsed);
        Time.timeScale = 1f;
        GameIsPaused = false;
        // SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        // SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
    }
}
