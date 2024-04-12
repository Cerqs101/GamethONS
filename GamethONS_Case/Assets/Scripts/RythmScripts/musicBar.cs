using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class musicBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public void setMaxHeath(){
        // LaneObject[] lanes = FindObjectsByType<LaneObject>(FindObjectsSortMode.None);
        // foreach(LaneObject lane in lanes)
        //     slider.maxValue = lane.instance.histPerEncounter()*2;
    }
    public void SetMusicBar(){
        slider.value =  LevelManager.hits - LevelManager.misses;
    }
}
