using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static List<AudioSource> activeSongLayers = new List<AudioSource>();
    public static bool wasSongLayerAdded = false;
    [SerializeField] public AudioSource firstSongLayer;
    [NonSerialized] public static List<AudioSource> allSongLayers = new List<AudioSource>();
    [SerializeField] public float songStartingTime = 0f;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] public bool playFirstSongLayerOnAwake = true;
    
    private static int currentSongLayer = 0;
    
    public static SoundManager Instance;



    void Awake()
    {
        if(Instance != null)
        {
            playFirstSongLayerOnAwake = true;
            
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
            
            allSongLayers.Add(firstSongLayer);
            activeSongLayers.Add(firstSongLayer);
                
            foreach(AudioSource songLayer in GetComponentsInChildren<AudioSource>(true)){
                allSongLayers.Add(songLayer);
                if(SaveSystem.CheckUpgrade("WallJump") && songLayer.clip.name.ToLower() == "track01_noise"){
                    activeSongLayers.Add(songLayer);
                }

                if(SaveSystem.CheckUpgrade("Dash") && songLayer.clip.name.ToLower() == "track01_pulse01"){
                    activeSongLayers.Add(songLayer);
                }
            }
                
            for(int i = currentSongLayer; i < allSongLayers.Count(); i++)
                allSongLayers[i].volume = 0;

            if(playFirstSongLayerOnAwake)
                StartCoroutine(FadeIn(firstSongLayer));
        }
    }


    void Update()
    {
        // double timeInSongLoop = LevelManager.timeInSongLoop;

        // SetTimeToAllSongLayers((float)(timeInSongLoop - LevelManager.Instance.musicStartDelay));
    }


    public void StartNextSongLayer()
    {
        if(currentSongLayer+1 < allSongLayers.Count())
        {
            currentSongLayer++;
            FadeIn(allSongLayers[currentSongLayer]);
        }
    }


    public void StartSongLayer(AudioSource songLayer)
    {
        if(songLayer == firstSongLayer)
            foreach(AudioSource song in allSongLayers)
                StartCoroutine(FadeOut(song, 0.1f));
        StartCoroutine(FadeIn(songLayer, fadeInDuration));
    }


    public static double GetAudioTime(AudioSource audio = null){
        if(audio == null)
            audio = allSongLayers[0];
        return audio.time;
    }


    public static void SetTimeToAllSongLayers(float time)
    {
        foreach(AudioSource songLayer in allSongLayers)
            songLayer.time = time + Instance.songStartingTime;
    }

    public static double GetAudioLenght(AudioSource audio = null)
    { 
        if(audio == null)
            audio = allSongLayers[0];
        return audio.clip.length;
    }


    public void PlayAllSongs(){
        foreach(AudioSource song in allSongLayers)
            PlaySong(song);
    }

    public void PlaySong(AudioSource song)
    {
        song.time = songStartingTime;
        song.Play();
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
        while(songLayer.volume > 0.01f)
        {
            songLayer.volume -= intensity;
            yield return new WaitForSecondsRealtime(waitPerLoop);
        }
    }

    public void FadeOutAllSongLayers(float waitTime=1f, bool activeOnly=false)
    {
        if(activeOnly)
            foreach(AudioSource songLayer in activeSongLayers)
                StartCoroutine(FadeOut(songLayer, waitTime));
        else
            foreach(AudioSource songLayer in allSongLayers)
                StartCoroutine(FadeOut(songLayer, waitTime));

    }

    public void FadeInAllSongLayers(float waitTime=1f, bool activeOnly=false)
    {
        if(activeOnly)
            foreach(AudioSource songLayer in activeSongLayers)
                StartCoroutine(FadeIn(songLayer, waitTime));
        else
            foreach(AudioSource songLayer in allSongLayers)
                StartCoroutine(FadeIn(songLayer, waitTime));
    }

    public void addToSongLayers(AudioSource song)
    {
        song.volume = 0;
        song.time = firstSongLayer.time;
        allSongLayers.Add(song);
    }
}
