using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class musicBar : MonoBehaviour
{
    // Start is called before the first frame update
    bool isToSetMusicBar = true;
    public Slider slider;
    public GameObject musicBarObject;
    public fillBar fill;
    public void setMaxValueMusicBar(){
        LaneObject[] lanes = FindObjectsByType<LaneObject>(FindObjectsSortMode.None);
        foreach(LaneObject lane in lanes)
            slider.maxValue += lane.histPerEncounter()*2;
    }
    public void SetValueMusicBar(){
        slider.value =  slider.maxValue/2 + (LevelManager.hits - LevelManager.misses);
        Debug.Log(LevelManager.misses);
        Debug.Log(LevelManager.hits);
        Debug.Log(slider.maxValue/2 + (LevelManager.hits - LevelManager.misses));
    }
    void Start(){
        //musicBarObject.SetActive(false);
    }
    void Update(){
        if(LevelManager.isEncounterHappening){
            musicBarObject.SetActive(true);
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
            musicBarObject.SetActive(true);
        }
            

    }
    void ChangeFillColor(){
        fill = FindObjectOfType<fillBar>();

        if(slider.value >= slider.maxValue*0.9){
            fill.imagen.color = fill.perfectColor;
        }
        else if(slider.value >= slider.maxValue*0.5){
            fill.imagen.color = fill.goodColor;
        }
        else{
            fill.imagen.color = fill.errorColor;
        }
    }
}
