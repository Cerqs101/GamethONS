using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ScoreTextDisplay : MonoBehaviour
{
    [SerializeField] int levelNumber = 0;
    TextMeshPro text;
    void Start()
    {
        text = GetComponent<TextMeshPro>();

        switch(levelNumber){
            case 1: SetValueToDisplay(ScoreManager.level01HighScore);
                    break;
            case 2: SetValueToDisplay(ScoreManager.level02HighScore);
                    break;
            case 3: SetValueToDisplay(ScoreManager.level03HighScore);
                    break;
            default: SetValueToDisplay(0);
                    break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetValueToDisplay(int value){
        if(value <= 0)
            text.text = "";
        else
            text.text = value.ToString();
    }
}
