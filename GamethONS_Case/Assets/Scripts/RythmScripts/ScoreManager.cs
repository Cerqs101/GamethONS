using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    

    [NonSerialized] public int levelHits = 0;
    [NonSerialized] public float levelAccuracy = 0;
    [NonSerialized] public int levelRecoveredHealth = 0;
    [NonSerialized] public int levelCompletedEncounters = 0;

    [NonSerialized] public int totalEncountersInAllLevels = 0;

    public static int totalScore = 0;
    public static int currentLevelScore = 0;
    public static int level01Score = 0;
    public static int level02Score = 0;
    public static int level03Score = 0;



    void Start()
    {
        Instance = this;
        totalEncountersInAllLevels = FindObjectsByType<Encounter>(FindObjectsSortMode.None).Count();
    }

    
    void Update()
    {
        currentLevelScore = (int)Mathf.Round(((float)levelRecoveredHealth*500f + (float)levelHits*100f) * (float)levelAccuracy * ((float)levelCompletedEncounters/((float)totalEncountersInAllLevels/2f)));
        // Debug.Log(currentLevelScore);
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += condicionalScoreSave;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= condicionalScoreSave;
    }


    public void UpdateOverallAccuray(float newAccuray)
    {
        levelAccuracy += (newAccuray-levelAccuracy)/levelCompletedEncounters;
    }


    public void condicionalScoreSave(Scene scene, LoadSceneMode mode)
    {
        string lastSceneLoaded = ScenesManager.previousScene;
        if(lastSceneLoaded == "Fase1"){
            level01Score = currentLevelScore;}
        else {if(lastSceneLoaded == "FaseWallJump"){
            level02Score = currentLevelScore;}
        else {if(lastSceneLoaded == "Fase3"){
            level03Score = currentLevelScore;}
        else{
            return;}}}
        Debug.Log(currentLevelScore);
        currentLevelScore = 0;
        Debug.Log(currentLevelScore);
        Debug.Log(level01Score);
        Debug.Log(level02Score);
        Debug.Log(level03Score);
    }
}
