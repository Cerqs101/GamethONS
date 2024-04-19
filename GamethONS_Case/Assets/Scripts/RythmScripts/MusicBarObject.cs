using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicBarObject : MonoBehaviour
{
    // Start is called before the first frame update
    bool isToSetMusicBar = true;

    public Slider slider;
    public GameObject musicBarObject;
    public MusicBarFill fill;
    public Image borderImage;
    [SerializeField] Color32 standarBorderCollor = new Color32(127,127,127,255);


    void Start(){
        //musicBarObject.SetActive(false);
        fill = FindObjectOfType<MusicBarFill>();
        borderImage = FindObjectOfType<MusicBarBorder>().imagem;
    }


    void Update(){
        if(LevelManager.isEncounterHappening){
            borderImage.color = standarBorderCollor;
            if(isToSetMusicBar){
                setMaxValueMusicBar();
                isToSetMusicBar = false;
            }
            SetValueMusicBar();
            ChangeFillColor();
        }
        if(!LevelManager.isEncounterHappening){
            isToSetMusicBar = true;
            slider.maxValue = 0;
            borderImage.color = new Color(0,0,0,0);
        }
    }   


    public void setMaxValueMusicBar(){
        LaneObject[] lanes = FindObjectsByType<LaneObject>(FindObjectsSortMode.None);
        foreach(LaneObject lane in lanes)
            slider.maxValue += lane.histPerEncounter()*2;
    }

    
    public void SetValueMusicBar(){
        slider.value =  slider.maxValue/2 + (LevelManager.hits - LevelManager.misses);
    }


    void ChangeFillColor(){

        if(slider.value >= slider.maxValue*LevelManager.Instance.highAccuracyThreshold){
            fill.imagen.color = fill.perfectColor;
        }
        else if(slider.value >= slider.maxValue*LevelManager.Instance.midAccuracyThreshold){
            fill.imagen.color = fill.goodColor;
        }
        else{
            fill.imagen.color = fill.errorColor;
        }
    }
}
