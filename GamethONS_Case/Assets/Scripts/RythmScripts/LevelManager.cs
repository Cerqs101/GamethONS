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

    public static int hits = 0;
    public static int misses = 0;

    private Player player;
    public double secondsPerEncounter;


    void Start()
    {
        Instance = this;

        // bea = midiFile.GetTempoMap().GetTempoAtTime(???); // <-- para tentar fazer no futuro futuro
        musicStartDelay = LaneObject.xDistanceToHit / (bpm * 4 / 60f);
        measureDuration = beatsPerMeasure * 1 * 60 / bpm;            // measureDuration = timeSignture * numberOfmeasures * 60seconds / Bpm;
        midiFile = ReadMidiFileFromDisc();
        player = FindFirstObjectByType<Player>();
        secondsPerEncounter = measuresPerEncounter * measureDuration;
        // FindObjectOfType<SoundManager>().Invoke("PlayMusic", musicStartDelay);
        // hasLevelStarted = true;
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


    public IEnumerator StartEncounter(float durationInMeasures=0f)
    {
        if(durationInMeasures == 0f)
            durationInMeasures = measuresPerEncounter;

        hits = 0;
        misses = 0;
            
        noteGeneration = true;
        isEncounterHappening = true;

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime((float)(measureDuration*durationInMeasures));
        StartCoroutine(StopEncounter());
    }


    public IEnumerator StopEncounter()
    {
        noteGeneration = false;
        
        yield return new WaitForSecondsRealtime(musicStartDelay);

        isEncounterHappening = false;
        Time.timeScale = 1;
        
        float accuracy = (float)hits/(hits+misses);
        Debug.Log($"\nHits{hits}    Misses{misses}    Acurr.{accuracy}");
        if(accuracy >= 0.9)
            HighhAccuray();
        else if(accuracy >= 0.7)
            MidAccuracy();
        else
            LowAccuracy();
    }

    private void HighhAccuray()
    {
            Debug.Log("Brabo demais!!");
            player.AplicaDano(-1);
            FindObjectOfType<ScriptLogic>().subtraiVida();
    }

    private void MidAccuracy()
    {
            Debug.Log("Tá, ok");

    }

    private void LowAccuracy()
    {
            Debug.Log("Dano");
            player.AplicaDano(2);
            FindObjectOfType<ScriptLogic>().subtraiVida();
    }


}