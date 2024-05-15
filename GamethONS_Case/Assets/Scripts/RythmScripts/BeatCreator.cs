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

public class BeatCreator : MonoBehaviour
{
    [SerializeField] public static float xDistanceToHit = 8;
    [NonSerialized] public List<double> timeStamps = new List<double>(); // in seconds
    [NonSerialized] public int spawnIndex = 0;
    private Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;

    [SerializeField] private GameObject beatPrefab;
    [SerializeField] public HitObject hit;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        // instance = this;
        Vector3 beatToHitDistance = new Vector3(xDistanceToHit, 0f, 0f);
        transform.position = hit.transform.position + beatToHitDistance;

        noteRestriction = hit.noteRestriction;

        SetTimeStamps(FindObjectOfType<LaneContainer>().GetDataFromMidi());
    }


    void Update()
    {
        spawnIndex = LaneContainer.beatIndexes[noteRestriction];
        if (spawnIndex < timeStamps.Count)
            if (LevelManager.timeInSongLoop >= timeStamps[spawnIndex])
            {
                if (LevelManager.noteGeneration)
                {
                    GameObject newBeat = Instantiate(beatPrefab, transform.position, new Quaternion(0, 0, 0, 0), transform);
                    newBeat.GetComponent<BeatObject>().noteName = noteRestriction;
                }
                LaneContainer.beatIndexes[noteRestriction]++;
            }
    }


    private void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] notes)
    {
        foreach (var note in notes)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, LaneContainer.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }

    public int histInEncounter(){
        Encounter encounter = Encounter.GetCurrentEncounter();
        double songLegth = SoundManager.GetAudioLenght();

        int songLoopsInEncounter = (int) Mathf.Floor((float) (encounter.secondsInEncounter/songLegth));
        if(LevelManager.timeInSongLoop + encounter.secondsInEncounter > songLegth)
            songLoopsInEncounter +=1;

        int qtdBeats = 0;
        qtdBeats += songLoopsInEncounter*hitsInInterval(0, songLegth);
        qtdBeats += hitsInInterval(LevelManager.timeInSongLoop, LevelManager.timeInSongLoop + encounter.secondsInEncounter - (songLoopsInEncounter*songLegth));
        return qtdBeats;
    }


    private int hitsInInterval(double start, double end)
    {
        int qtdBeats = 0;
        foreach(double stamp in timeStamps)    
            if(stamp >= start && stamp <= end)
                qtdBeats++;
        return qtdBeats;
    }
}
