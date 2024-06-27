using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Interaction;
using System;

public class BeatCreator : RhythmMonoBehaviour
{
    [SerializeField] public static float xDistanceToHit = 8;

    [SerializeField] private GameObject beatPrefab;
    [SerializeField] public HitObject hit;


    void Start()
    {
        Vector3 beatToHitDistance = new Vector3(xDistanceToHit, 0f, 0f);
        transform.position = hit.transform.position + beatToHitDistance;

        noteRestriction = hit.noteRestriction;

        SetTimeStamps(FindObjectOfType<LaneContainer>().GetDataFromMidi());
    }


    void Update()
    {
        spawnIndex = LaneContainer.beatIndexes[noteRestriction];
        PerformInEveryBeat(CreateBeat);
        LaneContainer.beatIndexes[noteRestriction] = spawnIndex;
    }


    private void CreateBeat(){
        if (LevelManager.noteGeneration){
            GameObject newBeat = Instantiate(beatPrefab, transform.position, new Quaternion(0, 0, 0, 0), transform);
            newBeat.GetComponent<BeatObject>().noteName = noteRestriction;
        }
    }

    public int beatsInEncounter(){
        Encounter encounter = Encounter.GetCurrentEncounter();
        double songLegth = SoundManager.GetAudioLenght();

        int songLoopsInEncounter = (int) Mathf.Floor((float) (encounter.secondsInEncounter/songLegth));
        if(LevelManager.timeInSongLoop + encounter.secondsInEncounter > songLegth)
            songLoopsInEncounter +=1;

        int qtdBeats = 0;
        qtdBeats += songLoopsInEncounter*beatsInInterval(0, songLegth);
        qtdBeats += beatsInInterval(LevelManager.timeInSongLoop, LevelManager.timeInSongLoop + encounter.secondsInEncounter - (songLoopsInEncounter*songLegth));
        return qtdBeats;
    }
}
