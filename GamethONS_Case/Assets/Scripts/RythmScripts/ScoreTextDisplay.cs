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
            case 1: setValueToDisplay(ScoreManager.level01Score);
                    break;
            case 2: setValueToDisplay(ScoreManager.level02Score);
                    break;
            case 3: setValueToDisplay(ScoreManager.level03Score);
                    break;
            default: setValueToDisplay(0);
                    break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setValueToDisplay(int value){
        if(value <= 0)
            text.text = "";
        else
            text.text = value.ToString();
    }
}
