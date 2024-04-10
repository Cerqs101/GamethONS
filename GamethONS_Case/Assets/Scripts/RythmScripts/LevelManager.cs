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
    
    [SerializeField] private float musicStartDelay = 0f;         // in seconds
    public static double timeSinceStarted = 0;                   // in seconds
    public static bool hasLevelStarted = false;

    public static bool isEncounterHappening = false;
    public static bool noteGeneration = false;

    public string midiFilePath;


    public float bpm = 60f;
    [SerializeField] private float beatsPerMeasure = 4f; // time signature
    [SerializeField] private float measuresPerEncounter = 2f; 
    private double measureDuration;                             // in seconds

    public static LevelManager Instance;
    public static MidiFile midiFile;


    void Start()
    {
        Instance = this;

        // bea = midiFile.GetTempoMap().GetTempoAtTime(???); // <-- para tentar fazer no futuro futuro
        musicStartDelay = LaneObject.spawnX / (bpm * 4 / 60f);
        measureDuration = beatsPerMeasure * 1 * 60 / bpm;            // measureDuration = timeSignture * numberOfmeasures * 60seconds / Bpm;
        midiFile = ReadFromMidiFileDisc();

        // FindObjectOfType<SoundManager>().Invoke("PlayMusic", musicStartDelay);
        // hasLevelStarted = true;
    }


    private MidiFile ReadFromMidiFileDisc()
    {
        return MidiFile.Read(Application.dataPath + "/" + midiFilePath);
    }


    public static Melanchall.DryWetMidi.Interaction.Note[] GetDataFromMidi()
    {
        midiFile = FindObjectOfType<LevelManager>().ReadFromMidiFileDisc();
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        return array;
    }

    public IEnumerator StartEncounter(float durationInMeasures=0f)
    {
        if(durationInMeasures == 0f)
            durationInMeasures = measuresPerEncounter;
            
        noteGeneration = true;
        isEncounterHappening = true;
        Debug.Log("Started Encounter");

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime((float)(measureDuration*durationInMeasures));
        StartCoroutine(StopEncounter());
    }

    public IEnumerator StopEncounter()
    {
        noteGeneration = false;
        
        yield return new WaitForSecondsRealtime(musicStartDelay);
        Debug.Log("Stopped Encounter");

        isEncounterHappening = false;
        Time.timeScale = 1;
    }


    void Update()
    {
        if(hasLevelStarted)
            timeSinceStarted += Time.unscaledDeltaTime;

            
        if(!hasLevelStarted)
        {
            if(Input.anyKeyDown){
                FindObjectOfType<SoundManager>().Invoke("PlayMusic", musicStartDelay);
                hasLevelStarted = true;
            }
        }

        // if(encounterIndex < measuresForEncounters.Length){
        //     if(!isEncounterHappening && timeSinceStarted >= measureDuration*measuresForEncounters[encounterIndex])
        //         startEncounter();
        //     if(isEncounterHappening && timeSinceStarted >= measureDuration*(measuresForEncounters[encounterIndex]+measuresPerEncounter))
        //     {
        //         stopEncounter();        
        //         encounterIndex++;
        //     }
        // }
    }
}