using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Unity.VisualScripting;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using System.Linq;
using System.IO;
using UnityEngine.Windows;
using UnityEngine.Networking;

public class LaneContainer : MonoBehaviour
{
    public static Dictionary<NoteName, int> beatIndexes = new Dictionary<NoteName, int>();
    public static MidiFile midiFile;
    [SerializeField] public MidiFile MidiFileTest;
    [SerializeField] public string midiFilePath;
    public static List<KeyCode> activeLanes = new List<KeyCode>();
    public bool wasLaneAdded = false;

    void Awake()
    {
        foreach(NoteName lane in beatIndexes.Keys.ToList())
            beatIndexes[lane] = 0;
        
        // MidiFileTest.
    }

    void Start()
    {

        foreach(BeatCreator beatCreator in transform.GetComponentsInChildren<BeatCreator>(true))
            if(!beatIndexes.Keys.ToList().Contains(beatCreator.hit.noteRestriction))
                beatIndexes.Add(beatCreator.hit.noteRestriction, 0);

        foreach(LaneWindow lane in transform.GetComponentsInChildren<LaneWindow>(true))
            if(activeLanes.Contains(lane.transform.GetComponentInChildren<HitObject>().keyToPress))
                lane.gameObject.SetActive(true);

        if(Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https;//"))
            ReadMidiFileFromWeb();
        else
            midiFile = ReadMidiFileFromDisc();
    }


    private IEnumerator ReadMidiFileFromWeb()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + midiFilePath))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.ConnectionError && www.result != UnityWebRequest.Result.ProtocolError)
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                    midiFile = MidiFile.Read(stream);
            }
        }
    }
    

    private MidiFile ReadMidiFileFromDisc()
    {
        return MidiFile.Read(Application.streamingAssetsPath + "/" + midiFilePath);
    }


    public Melanchall.DryWetMidi.Interaction.Note[] GetDataFromMidi()
    {
        ICollection<Melanchall.DryWetMidi.Interaction.Note> notes = midiFile.GetNotes();
        Melanchall.DryWetMidi.Interaction.Note[] array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        return array;
    }
}
