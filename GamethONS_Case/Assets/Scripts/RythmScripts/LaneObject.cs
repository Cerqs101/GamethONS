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
    private Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;

    [SerializeField] private GameObject beatPrefab;
    [SerializeField] private HitController hit;
    private SpriteRenderer spriteRenderer;

    // public LaneObject instance;


    void Start()
    {
        // instance = this;
        Vector3 beatToHitDistance = new Vector3(xDistanceToHit, 0f, 0f);
        transform.position = hit.transform.position + beatToHitDistance;

        noteRestriction = hit.noteRestriction;

        SetTimeStamps(LevelManager.GetDataFromMidi());
    }


    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (LevelManager.timeSinceStarted >= timeStamps[spawnIndex])
            {
                if (LevelManager.noteGeneration)
                {
                    Debug.Log(spawnIndex);
                    Debug.Log("Teste");
                    GameObject newBeat = Instantiate(beatPrefab, transform.position, new Quaternion(0, 0, 0, 0), transform);
                    newBeat.GetComponent<BeatObject>().noteName = noteRestriction;
                }
                spawnIndex++;
            }
        }
    }


    void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] notes)
    {
        foreach (var note in notes)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, LevelManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
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
