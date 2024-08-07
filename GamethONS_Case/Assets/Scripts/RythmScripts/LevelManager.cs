using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;
using Unity.Burst.Intrinsics;
using UnityEngine.SceneManagement;
using Melanchall.DryWetMidi.MusicTheory;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    [NonSerialized] public float musicStartDelay = 0f;         // in seconds
    public static double timeInSongLoop = 0;                   // in seconds
    public static bool hasLevelStarted = false;

    public static bool isEncounterHappening = false;
    public static bool noteGeneration = false;

    [SerializeField] private float hitDelay = 0f;


    [SerializeField] public float bpm = 60f;
    [SerializeField] private float beatsPerMeasure = 4f; // time signature
    [NonSerialized] public double measureDuration;   

    [SerializeField] public float highAccuracyThreshold = 0.9f;
    [SerializeField] public float midAccuracyThreshold = 0.7f;

    private float lastRecordedSongtime = 0f;

    private Player player;
    public static LevelManager Instance;



    void Start()
    {
        Instance = this;

        musicStartDelay = (BeatCreator.xDistanceToHit / (bpm * 3 / 60f)) - hitDelay;
        measureDuration = beatsPerMeasure * 1 * 60 / bpm;            // measureDuration = timeSignture * numberOfmeasures * 60seconds / Bpm;
        player = FindFirstObjectByType<Player>();

        if(timeInSongLoop == 0)
            timeInSongLoop = musicStartDelay;
    }


    void Update()
    {
        if(!hasLevelStarted)
            if(Input.anyKeyDown)
            {
                if(!SoundManager.Instance.firstSongLayer.isPlaying)
                    SoundManager.Instance.Invoke("PlayAllSongs", musicStartDelay);
                if(SoundManager.Instance.playFirstSongLayerOnAwake)
                    SoundManager.Instance.FadeInAllSongLayers(activeOnly:true);
                hasLevelStarted = true;
            }

        if(timeInSongLoop >= SoundManager.GetAudioLenght() + Instance.musicStartDelay || timeInSongLoop - (Instance.musicStartDelay*2) > SoundManager.Instance.firstSongLayer.time)
            RestartSpawnIndexes();
        ResyncBeatsToAudio();

        if(hasLevelStarted)
            timeInSongLoop += Time.unscaledDeltaTime;
    }


    public static void ResyncBeatsToAudio()
    {
        timeInSongLoop = SoundManager.Instance.firstSongLayer.time + Instance.musicStartDelay;
    }


    public static void RestartSpawnIndexes()
    {
        foreach(NoteName lane in LaneContainer.beatIndexes.Keys.ToList())
            LaneContainer.beatIndexes[lane] = 0;
        foreach(RhythmicAnimation animation in FindObjectsByType<RhythmicAnimation>(FindObjectsSortMode.None))
            animation.spawnIndex = 0;
        timeInSongLoop = Instance.musicStartDelay;
    }


    public void AcurracyConsequences(float playerAccuracy)
    {
        float deltaHealth;
        if(playerAccuracy >= highAccuracyThreshold)
            deltaHealth = Mathf.Pow(2, (playerAccuracy-highAccuracyThreshold)*16);
        else if(playerAccuracy >= midAccuracyThreshold)
            deltaHealth = 0f;
        else
            deltaHealth = ((10*(midAccuracyThreshold-playerAccuracy))+1) *-1;

        int deltaHealthInt = (int) Mathf.Round(deltaHealth);
        
        player.AplicaDano(deltaHealthInt * -1, false);
        FindObjectOfType<ScriptLogic>().subtraiVida();

        if(deltaHealthInt > 0)
            ScoreManager.Instance.levelRecoveredHealth += deltaHealthInt;
    }


    public IEnumerator EndGame()
    {
        float waitTime = 5f;
        SoundManager.Instance.FadeOutAllSongLayers(waitTime);
        FindObjectOfType<SceneFadeObject>().Fade("in");
        yield return new WaitForSecondsRealtime(waitTime);

        hasLevelStarted = false;
        timeInSongLoop = 0;

        if(FindObjectOfType<LaneContainer>().wasLaneAdded)
            LaneContainer.activeLanes.RemoveAt(LaneContainer.activeLanes.Count()-1);
        if(SoundManager.wasSongLayerAdded)
        {
            SoundManager.activeSongLayers.RemoveAt(SoundManager.activeSongLayers.Count() - 1);
            SoundManager.wasSongLayerAdded = false;
        }
        if(FindObjectOfType<Player>()._gotAPowerUp)
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case "Fase1":           
                    FindObjectOfType<Player>().RemoveWallJump();
                    break;
                case "FaseWallJump":    
                    FindObjectOfType<Player>().RemoveDash();
                    break;
            }
        }
        
        ScenesManager.previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
    }
}