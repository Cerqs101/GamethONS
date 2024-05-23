using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Unity.VisualScripting;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using System.Linq;

public class LaneContainer : MonoBehaviour
{
    public static Dictionary<NoteName, int> beatIndexes = new Dictionary<NoteName, int>();
    public static MidiFile midiFile;
    [SerializeField] public string midiFilePath;
    public static List<KeyCode> activeLanes = new List<KeyCode>();
    public bool wasLaneAdded = false;

    void Awake()
    {
        foreach(NoteName lane in beatIndexes.Keys.ToList())
            beatIndexes[lane] = 0;
    }

    void Start()
    {

        foreach(BeatCreator beatCreator in transform.GetComponentsInChildren<BeatCreator>(true))
            if(!beatIndexes.Keys.ToList().Contains(beatCreator.hit.noteRestriction))
                beatIndexes.Add(beatCreator.hit.noteRestriction, 0);

        foreach(LaneWindow lane in transform.GetComponentsInChildren<LaneWindow>(true))
            if(activeLanes.Contains(lane.transform.GetComponentInChildren<HitObject>().keyToPress))
                lane.gameObject.SetActive(true);

        midiFile = ReadMidiFileFromDisc();
    }
    

    private MidiFile ReadMidiFileFromDisc()
    {
        return MidiFile.Read(Application.dataPath + "/" + midiFilePath);
    }


    public Melanchall.DryWetMidi.Interaction.Note[] GetDataFromMidi()
    {
        midiFile = FindObjectOfType<LaneContainer>().ReadMidiFileFromDisc();
        ICollection<Melanchall.DryWetMidi.Interaction.Note> notes = midiFile.GetNotes();
        Melanchall.DryWetMidi.Interaction.Note[] array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        return array;
    }
}
