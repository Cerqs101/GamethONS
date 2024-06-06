using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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
    public static int level01HighScore = 0;
    public static int level02HighScore = 0;
    public static int level03HighScore = 0;



    void Start()
    {
        Instance = this;
        totalEncountersInAllLevels = FindObjectsByType<Encounter>(FindObjectsSortMode.None).Count();
    }

    
    void Update()
    {
        currentLevelScore = CalculateScore();
    }


    public int CalculateScore(){
        return (int)Mathf.Round(((float)levelRecoveredHealth*500f + (float)levelHits*100f) * (float)levelAccuracy * ((float)levelCompletedEncounters/((float)totalEncountersInAllLevels/2f)));
    }

    void OnDisable()
    {
        condicionalScoreSave();
    }


    public void UpdateOverallAccuray(float newAccuray)
    {
        levelAccuracy += (newAccuray-levelAccuracy)/levelCompletedEncounters;
    }


    public void condicionalScoreSave()
    {
        string lastSceneLoaded = ScenesManager.previousScene;
        if(lastSceneLoaded == "Fase1")
            level01HighScore = Math.Max(CalculateScore(), level01HighScore);
        else if(lastSceneLoaded == "FaseWallJump")
            level02HighScore = Math.Max(CalculateScore(), level02HighScore);
        else if(lastSceneLoaded == "Fase3")
            level03HighScore = Math.Max(CalculateScore(), level03HighScore);
        else
            return;
        currentLevelScore = 0;
    }
}
