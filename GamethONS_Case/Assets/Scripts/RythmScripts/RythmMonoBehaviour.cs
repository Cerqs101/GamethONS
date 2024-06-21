using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Interaction;
using System;

public class RhythmMonoBehaviour : MonoBehaviour
{
    [NonSerialized] public List<double> timeStamps = new List<double>();
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    [NonSerialized] public int spawnIndex = 0;


    public void PerformInEveryBeat(Action callBack) {
        if (spawnIndex < timeStamps.Count)
            if (LevelManager.timeInSongLoop >= timeStamps[spawnIndex])
            {
                callBack();
                spawnIndex++;
            }
    }


    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] notes)
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

    public int beatsInInterval(double start, double end)
    {
        int qtdBeats = 0;
        foreach(double stamp in timeStamps)    
            if(stamp >= start && stamp <= end)
                qtdBeats++;
        return qtdBeats;
    }
}
