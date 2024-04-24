using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class LevelManager : MonoBehaviour
{
    private float musicStartDelay = 0f;         // in seconds
    public static double timeSinceStarted = 0;                   // in seconds
    public static bool hasLevelStarted = false;

    public static bool isEncounterHappening = false;
    public static bool noteGeneration = false;

    [SerializeField] private float hitDelay = 0f;



    public float bpm = 60f;
    [SerializeField] private float beatsPerMeasure = 4f; // time signature
    public double measureDuration;   
    // public double secondsPerEncounter;  

    [SerializeField] public float highAccuracyThreshold = 0.9f;
    [SerializeField] public float midAccuracyThreshold = 0.7f;
    public static int hits = 0;
    public static int misses = 0;

    private Player player;
    public static LevelManager Instance;
    public static MidiFile midiFile;
    public string midiFilePath;



    void Start()
    {
        Instance = this;

        // bea = midiFile.GetTempoMap().GetTempoAtTime(???); // <-- para tentar fazer no futuro futuro
        musicStartDelay = (LaneObject.xDistanceToHit / (bpm * 4 / 60f)) + hitDelay;
        measureDuration = beatsPerMeasure * 1 * 60 / bpm;            // measureDuration = timeSignture * numberOfmeasures * 60seconds / Bpm;
        midiFile = ReadMidiFileFromDisc();
        player = FindFirstObjectByType<Player>();
        // secondsPerEncounter = measuresPerEncounter * measureDuration;
    }


    void Update()
    {
        if(hasLevelStarted)
            timeSinceStarted += Time.unscaledDeltaTime;

        if(!hasLevelStarted)
        {
            if(Input.anyKeyDown){
                SoundManager.Instance.Invoke("PlayAllSongs", musicStartDelay);
                hasLevelStarted = true;
            }
        }
    }


    private MidiFile ReadMidiFileFromDisc()
    {
        return MidiFile.Read(Application.dataPath + "/" + midiFilePath);
    }


    public static Melanchall.DryWetMidi.Interaction.Note[] GetDataFromMidi()
    {
        midiFile = FindObjectOfType<LevelManager>().ReadMidiFileFromDisc();
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        return array;
    }


    public IEnumerator Encounter(GameObject obj, float durationInMeasures=4f)
    {
        obj.GetComponent<Encounter>().isHappening = true;

        StartEncounter();
        yield return new WaitForSecondsRealtime((float)(measureDuration*durationInMeasures));
        yield return StopEncounter();

        SolveEncounter(obj);
    }

    public void StartEncounter()
    {
        hits = 0;
        misses = 0;
        
        noteGeneration = true;
        isEncounterHappening = true;

        Time.timeScale = 0;

        // yield return new WaitForSecondsRealtime((float)(measureDuration*durationInMeasures));
        // StartCoroutine(StopEncounter(obj));
    }


    public IEnumerator StopEncounter()
    {
        noteGeneration = false;
        
        yield return new WaitForSecondsRealtime(musicStartDelay);

        isEncounterHappening = false;
        Time.timeScale = 1;
    }


    public void SolveEncounter(GameObject obj)
    {
        float accuracy = (float)hits/(hits+misses);
        AcurracyConsequences(accuracy);

        SoundManager.Instance.StartSongLayer(obj.GetComponent<Encounter>().songLayer);

        Destroy(obj);
    }


    private void AcurracyConsequences(float playerAccuracy){
        float dano;
        if(playerAccuracy >= highAccuracyThreshold)
            dano = Mathf.Pow(2, (playerAccuracy-highAccuracyThreshold)*16) * -1;
        else if(playerAccuracy >= midAccuracyThreshold)
            dano = 0f;
        else
            dano = (10*(midAccuracyThreshold-playerAccuracy))+1;

        player.AplicaDano((int) Mathf.Round(dano));
        FindObjectOfType<ScriptLogic>().subtraiVida();
    }

}