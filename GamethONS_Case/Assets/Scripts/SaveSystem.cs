using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    void Start(){
        Instance = this;
        Debug.Log(GetHighScore("Fase1"));
        ResetAll();
    }
    
    public static void SetTutorial(int state){ 
        PlayerPrefs.SetInt("Tutorial",state);
    }

    public static void SetHighScore(string fase,int score){
        PlayerPrefs.SetInt(fase,score);
    }

    public static int GetTutorial(){
        return PlayerPrefs.GetInt("Tutorial");
    }

    public static int GetHighScore(string fase){
        return PlayerPrefs.GetInt(fase);
    }
     public static void SetUpgrade(string upgrade, bool state){
        int i;
        if(state == true){
            i = 1;
        }
        else{
            i = 0;
        }
        PlayerPrefs.SetInt(upgrade,i);
     }
     public static bool CheckUpgrade(string upgrade){
       int i = PlayerPrefs.GetInt(upgrade);
       if(i == 1){
        return true;
       }
       else{
        return false;
       }
     }

     public static void ResetAll()
     {
            SetHighScore("Fase1",0);
            SetHighScore("Fase2",0);
            SetHighScore("Fase2",0);
            SetUpgrade("Dash",false);
            SetUpgrade("WallJump",false);
            SetUpgrade("Zerou",false);
     }
}
