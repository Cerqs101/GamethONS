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

    private Player player;
    public static LevelManager Instance;
    public static MidiFile midiFile;
    [SerializeField] public string midiFilePath;


    void Start()
    {
        Instance = this;

        // bea = midiFile.GetTempoMap().GetTempoAtTime(???); // <-- para tentar fazer no futuro futuro
        musicStartDelay = (LaneObject.xDistanceToHit / (bpm * 4 / 60f)) - hitDelay;
        measureDuration = beatsPerMeasure * 1 * 60 / bpm;            // measureDuration = timeSignture * numberOfmeasures * 60seconds / Bpm;
        midiFile = ReadMidiFileFromDisc();
        player = FindFirstObjectByType<Player>();
    }


    void Update()
    {
        if(hasLevelStarted)
            timeInSongLoop += Time.unscaledDeltaTime;

        if(!hasLevelStarted)
            if(Input.anyKeyDown)
            {
                SoundManager.Instance.Invoke("PlayAllSongs", musicStartDelay);
                hasLevelStarted = true;
            }
        
        if(timeInSongLoop >= SoundManager.GetAudioLenght() + musicStartDelay)
        {   
            foreach(LaneObject lane in FindObjectsByType<LaneObject>(FindObjectsSortMode.None))
                lane.spawnIndex = 0;
            timeInSongLoop = musicStartDelay;
        }
    }


    private MidiFile ReadMidiFileFromDisc()
    {
        return MidiFile.Read(Application.dataPath + "/" + midiFilePath);
    }


    public static Note[] GetDataFromMidi()
    {
        midiFile = FindObjectOfType<LevelManager>().ReadMidiFileFromDisc();
        ICollection<Note> notes = midiFile.GetNotes();
        Note[] array = new Note[notes.Count];
        notes.CopyTo(array, 0);
        return array;
    }


    public void AcurracyConsequences(float playerAccuracy)
    {
        float damage;
        if(playerAccuracy >= highAccuracyThreshold)
            damage = Mathf.Pow(2, (playerAccuracy-highAccuracyThreshold)*16) * -1;
        else if(playerAccuracy >= midAccuracyThreshold)
            damage = 0f;
        else
            damage = (10*(midAccuracyThreshold-playerAccuracy))+1;

        player.AplicaDano((int) Mathf.Round(damage));
        FindObjectOfType<ScriptLogic>().subtraiVida();
    }

}