using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayMenuScript : MonoBehaviour
{
    public GameObject SelectedLevelText;

    void Update()
    {
        UpdateSelectedLevelText();    
    }

    public void UpdateSelectedLevelText()
    {
        string levelTextValue = "Random Level";
        string mazename = PlayerPrefs.GetString("mazename");
        for(int i=1; i<=4; i++){
            if("maze "+i.ToString()+" lol" == mazename){
                levelTextValue = "Level "+i.ToString();
            }
        }
        SelectedLevelText.GetComponent<TextMeshProUGUI>().text = levelTextValue;
    }
    
}
