using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource music;
    [SerializeField] private float songStartingTime = 0f;
    
    public static SoundManager Instance;


    void Start()
    {
        Instance = this;
    }


    void Update()
    {
    }


    public static double GetAudioTime(){
        // return (double) instance.music.timeSamples / instance.music.clip.frequency;
        return Instance.music.time;
    }


    public void PlayMusic(){
        Instance.music.time = songStartingTime;
        Instance.music.Play();
    }


    public void PlayNoteHitSfx(){
        // Debug.Log("Hell YEAH");
    }


    public void PlayNoteMissSfx(){
        // Debug.Log("O MAYY GAAA");
    }
}
