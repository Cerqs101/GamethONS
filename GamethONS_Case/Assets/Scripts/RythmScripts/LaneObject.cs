using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Linq;
// using Melanchall.DryWetMidi.Interaction.TempoMap;
// using Melanchall.DryWetMidi.MusicTheory;

public class LaneObject : MonoBehaviour
{
    [SerializeField] public static float xDistanceToHit = 8;
    private int spawnIndex = 0;
    public List<double> timeStamps = new List<double>(); // in seconds

    [SerializeField] private GameObject notePrefab;
    private SpriteRenderer spriteRenderer;
    public static LaneObject Instance;


    void Start()
    {
        Instance = this;
        Vector3 beatToHitDistance = new Vector3(xDistanceToHit, 0f, 0f);
        transform.position = FindObjectOfType<HitController>().transform.position + beatToHitDistance;
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetTimeStamps(LevelManager.GetDataFromMidi());
    }


    void Update()
    {
        if(spawnIndex < timeStamps.Count){
            if(LevelManager.timeSinceStarted >= timeStamps[spawnIndex])
            {
                if(LevelManager.noteGeneration)
                    Instantiate(notePrefab, transform.position, new Quaternion(0,0,0,0),  transform);
                spawnIndex++;
            }
        }
    }


    void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] notes)
    {
        foreach(var note in notes)
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, LevelManager.midiFile.GetTempoMap());
            timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
        }
    }
    public int histPerEncounter(){
        int qtdBeats = 0;
        foreach(double stamps in timeStamps){
            if(stamps >= LevelManager.timeSinceStarted && stamps <= LevelManager.timeSinceStarted + LevelManager.Instance.secondsPerEncounter){
                qtdBeats++;
            }
        }
    return qtdBeats;
    }

}
