using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class musicBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public void setMaxHeath(){
        slider.maxValue = LaneObject.Instance.histPerEncounter()*2;
    }
    public void SetMusicBar(){
        slider.value =  LevelManager.hits - LevelManager.misses;
    }
}
