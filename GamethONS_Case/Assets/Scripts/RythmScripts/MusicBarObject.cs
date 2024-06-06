using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicBarObject : MonoBehaviour
{
    [SerializeField]public Color32 errorColor = new Color32(207,38,53,255);
    [SerializeField]public Color32 goodColor = new Color32(39,115,185,255);
    [SerializeField]public Color32 perfectColor = new Color32(54,207,38,255);
    bool isToSetMusicBar = true;

    public Slider slider;
    public  Image fill;

    private Image borderImage;
    [SerializeField] Color32 standarBorderCollor = new Color32(127, 127, 127, 255);


    void Start()
    {
        borderImage = this.gameObject.transform.GetChild(0).GetComponent<Image>();
    }


    void Update()
    {
        if (LevelManager.isEncounterHappening)
        {
            borderImage.color = standarBorderCollor;
            if (isToSetMusicBar)
            {
                setMaxValueMusicBar();
                isToSetMusicBar = false;
            }
            SetValueMusicBar();
            ChangeFillColor();
        }
        if (!LevelManager.isEncounterHappening)
        {
            isToSetMusicBar = true;
            slider.maxValue = 0;
            borderImage.color = new Color(0, 0, 0, 0);
        }
    }


    public void setMaxValueMusicBar()
    {
        BeatCreator[] lanes = FindObjectsByType<BeatCreator>(FindObjectsSortMode.None);
        foreach (BeatCreator lane in lanes)
            slider.maxValue += lane.histInEncounter() * 2;
    }


    public void SetValueMusicBar()
    {
        slider.value = slider.maxValue / 2 + (Encounter.hits - Encounter.misses);
    }


    void ChangeFillColor()
    {

        if (slider.value >= slider.maxValue * LevelManager.Instance.highAccuracyThreshold)
        {
            fill.color = perfectColor;
        }
        else if (slider.value >= slider.maxValue * LevelManager.Instance.midAccuracyThreshold)
        {
            fill.color = goodColor;
        }
        else
        {
            fill.color = errorColor;
        }
    }
}
