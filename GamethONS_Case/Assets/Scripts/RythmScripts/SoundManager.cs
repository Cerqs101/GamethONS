using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [NonSerialized] public List<AudioSource> songLayers = new List<AudioSource>();
    [SerializeField] public AudioSource firstSongLayer;
    [SerializeField] private float songStartingTime = 0f;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private bool playFirstSongLayerOnAwake = true;
    
    private static int currentSongLayer = 0;
    
    public static SoundManager Instance;

    private static float commonSongAndBeatDisalingment;


    void Start()
    {
        // Instance = this;
        
        if(Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }

        songLayers.Add(firstSongLayer);
        foreach(AudioSource songLayer in GetComponentsInChildren<AudioSource>(true))
                songLayers.Add(songLayer);

        for(int i = currentSongLayer; i < songLayers.Count(); i++)
            songLayers[i].volume = 0;
        if(playFirstSongLayerOnAwake)
            StartCoroutine(FadeIn(firstSongLayer));

        // Invoke("PlayAllSongs", LevelManager.Instance.musicStartDelay);
        // SetTimeToAllSongLayers((float)(LevelManager.timeInSongLoop - LevelManager.Instance.musicStartDelay));
    }


    void Update()
    {
        double timeInSongLoop = LevelManager.timeInSongLoop;

        commonSongAndBeatDisalingment *= 0.7f;
        commonSongAndBeatDisalingment += ((float)timeInSongLoop - firstSongLayer.time)*0.3f;

        if(firstSongLayer.isPlaying 
        && Mathf.Round((float)timeInSongLoop - firstSongLayer.time) != Mathf.Round(commonSongAndBeatDisalingment))
            SetTimeToAllSongLayers((float)(timeInSongLoop - LevelManager.Instance.musicStartDelay));
    }


    public void StartNextSongLayer()
    {
        if(currentSongLayer+1 < songLayers.Count())
        {
            currentSongLayer++;
            FadeIn(songLayers[currentSongLayer]);
        }
    }


    public void StartSongLayer(AudioSource songLayer)
    {
        if(songLayer == firstSongLayer)
            foreach(AudioSource song in songLayers)
                StartCoroutine(FadeOut(song));
        StartCoroutine(FadeIn(songLayer, fadeInDuration));
    }


    public static double GetAudioTime(AudioSource audio = null){
        // return (double) instance.music.timeSamples / instance.music.clip.frequency;
        if(audio == null)
            audio = Instance.songLayers[0];
        return audio.time;
    }


    public static void SetTimeToAllSongLayers(float time)
    {
        foreach(AudioSource songLayer in Instance.songLayers)
            songLayer.time = time + Instance.songStartingTime;
    }

    public static double GetAudioLenght(AudioSource audio = null)
    { 
        if(audio == null)
            audio = Instance.songLayers[0];
        return audio.clip.length;
    }


    public void PlayAllSongs(){
        foreach(AudioSource song in songLayers)
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

    public IEnumerator FadeIn(AudioSource songLayer, float waitTime=1f)
    {
        float intensity = 0.01f;
        float waitPerLoop = waitTime*intensity;
        while(songLayer.volume < 1)
        {
            songLayer.volume += intensity;
            yield return new WaitForSecondsRealtime(waitPerLoop);
        }
    }

    public IEnumerator FadeOut(AudioSource songLayer, float waitTime=1f)
    {
        float intensity = 0.01f;
        float waitPerLoop = waitTime*intensity;
        while(songLayer.volume > 0)
        {
            songLayer.volume -= intensity;
            yield return new WaitForSecondsRealtime(waitPerLoop);
        }
    }

    public void FadeOutAllSongLayers(float waitTime=1f)
    {
        foreach(AudioSource songLayer in songLayers)
            StartCoroutine(FadeOut(songLayer, waitTime));
    }

    public void FadeInAllSongLayers(float waitTime=1f)
    {
        foreach(AudioSource songLayer in songLayers)
            StartCoroutine(FadeIn(songLayer, waitTime));
    }

    public void addToSongLayers(AudioSource song)
    {
        song.volume = 0;
        song.time = firstSongLayer.time;
        songLayers.Add(song);
    }
}
