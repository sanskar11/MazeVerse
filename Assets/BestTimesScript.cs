using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestTimesScript : MonoBehaviour
{
    public GameObject bestTimesTextUI;
    void Start()
    {
        string bestTimesText = "";
        for(int i=1; i<=4; i++){
            string mazenameVal = "maze "+i.ToString()+" lol" + " score";
            string besttime = "Not completed yet";
            if(PlayerPrefs.HasKey(mazenameVal)){
                besttime = formatTime(PlayerPrefs.GetFloat(mazenameVal));
            }
            bestTimesText += "Level "+i.ToString()+": "+besttime+"\n";
        }
        bestTimesTextUI.GetComponent<TextMeshProUGUI>().text = bestTimesText;
    }

    string formatTime(float t){
        float seconds = Mathf.Floor(t%60);
        t /= 60;
        float minutes = Mathf.Floor(t%60);
        return (noSingleDigit(minutes) + ":" + noSingleDigit(seconds));
    }

    string noSingleDigit(float a){
        string strf = "";
        if(a<10)
            strf += "0";
        return strf+a.ToString();
    }
}
