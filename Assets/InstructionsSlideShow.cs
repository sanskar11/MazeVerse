using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsSlideShow : MonoBehaviour
{
    public GameObject imagePanel;
    public Texture[] slides;
    public int currentSlideNumber = 0;
    // Start is called before the first frame update

    void Start(){
        UpdateSlides();
    }

    public void NextSlide(){
        if(currentSlideNumber != 4){
            currentSlideNumber+=1;
        }
        UpdateSlides();
    }
    public void PrevSlide(){
        if(currentSlideNumber != 0){
            currentSlideNumber-=1;
        }
        UpdateSlides();
    }

    void UpdateSlides(){
        imagePanel.GetComponent<RawImage>().texture = slides[currentSlideNumber];
    }

}
