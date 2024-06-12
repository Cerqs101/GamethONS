using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    
    void Start(){
        Instance = this;
        //PlayerPrefs.DeleteAll();
        //SetHighScore("Fase1",100);
        Debug.Log(GetHighScore("Fase1"));
    }
    
    public void SetTutorial(int state){ 
        PlayerPrefs.SetInt("Tutorial",state);
    }

    public void SetHighScore(string fase,int score){
        PlayerPrefs.SetInt(fase,score);
    }

    public int GetTutorial(){
        return PlayerPrefs.GetInt("Tutorial");
    }

    public int GetHighScore(string fase){
        return PlayerPrefs.GetInt(fase);
    }
}
