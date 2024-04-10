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

    public string midiFilePath;


    public float bpm = 60f;
    [SerializeField] private float beatsPerMeasure = 4f; // time signature
    [SerializeField] private float measuresPerEncounter = 2f; 
    private double measureDuration;                             // in seconds

    // [SerializeField] private int[] measuresForEncounters = new int[] {0,4,8}; // temporário enquanto encontro ainda não ocorre no metroidvania
    // private int encounterIndex = 0;                                           // temporário enquanto encontro ainda não ocorre no metroidvania

    public static LevelManager Instance;
    public static MidiFile midiFile;


    void Start()
    {
        Instance = this;
        // bea = midiFile.GetTempoMap().GetTempoAtTime(???); // <-- para tentar fazer no futuro futuro
        musicStartDelay = LaneObject.spawnX / (bpm * 4 / 60f);
        measureDuration = beatsPerMeasure * 1 * 60 / bpm;            // measureDuration = timeSignture * numberOfmeasures * 60seconds / Bpm;
        midiFile = ReadFromMidiFileDisc();
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
        Debug.Log("Started Encounter");
        if(durationInMeasures == 0f)
            durationInMeasures = beatsPerMeasure;
        Time.timeScale = 0;
        isEncounterHappening = true;
        yield return new WaitForSecondsRealtime((float)(measureDuration*durationInMeasures)-musicStartDelay);
        StopEncounter();
    }

    public void StopEncounter()
    {
        Debug.Log("Stopped Encounter");
        Time.timeScale = 1;
        isEncounterHappening = false;
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