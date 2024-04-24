using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private List<AudioSource> songs = new List<AudioSource>();
    [SerializeField] private float songStartingTime = 0f;
    [SerializeField] private float fadeInTime = 1f;
    
    private static int currentSongLayer = 0;
    
    public static SoundManager Instance;


    void Start()
    {
        Instance = this;

        foreach(Encounter encounter in FindObjectsByType<Encounter>(FindObjectsSortMode.None))
            songs.Add(encounter.songLayer);

        for(int i = currentSongLayer; i < songs.Count(); i++)
            songs[i].volume = 0;
        
    }


    void Update()
    {
    }


    public void StartNextSongLayer()
    {
        if(currentSongLayer+1 < songs.Count())
        {
            currentSongLayer++;
            songs[currentSongLayer].volume = 1;
        }
    }

    public void StartSongLayer(AudioSource songLayer)
    {
        StartCoroutine(FadeIn(songLayer, fadeInTime));
    }


    public static double GetAudioTime(){
        // return (double) instance.music.timeSamples / instance.music.clip.frequency;
        return Instance.songs[0].time;
    }


    public void PlayAllSongs(){
        foreach(AudioSource song in songs)
            PlaySong(song);
    }

    public void PlaySong(AudioSource song)
    {
        song.time = songStartingTime;
        song.Play();
    }


    public void PlayNoteHitSfx(){
        // Debug.Log("Hell YEAH");
    }


    public void PlayNoteMissSfx(){
        // Debug.Log("O MAYY GAAA");
    }

    private IEnumerator FadeIn(AudioSource songLayer, float waitTime=1f)
    {
        float intensity = 0.01f;
        float waitPerLoop = waitTime*intensity;
        while(songLayer.volume != 1)
        {
            songLayer.volume += intensity;
            yield return new WaitForSecondsRealtime(waitPerLoop);
        }
    }
}
