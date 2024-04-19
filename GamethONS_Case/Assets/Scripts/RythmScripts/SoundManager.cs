using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource[] songs;
    [SerializeField] private float songStartingTime = 0f;
    
    private static int currentSongLayer = 0;
    
    public static SoundManager Instance;


    void Start()
    {
        Instance = this;
        for(int i = currentSongLayer+1; i < songs.Count(); i++)
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
}
