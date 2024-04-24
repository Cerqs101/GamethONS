using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Encounter : MonoBehaviour
{
    [SerializeField] public AudioSource songLayer;

    [SerializeField] private float measuresInEncounter = 4f;
    [NonSerialized] public double secondsPerEncounter;
    public static int hits = 0;
    public static int misses = 0;
    [NonSerialized] public bool isHappening = false;


    void Start()
    {
        secondsPerEncounter = measuresInEncounter * LevelManager.Instance.measureDuration;
    }


    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            StartCoroutine(RunEncounter());
    }


    public IEnumerator RunEncounter()
    {
        StartEncounter();
        yield return new WaitForSecondsRealtime((float)secondsPerEncounter);
        yield return StopEncounter();
        SolveEncounter();
    }


    public void StartEncounter()
    {
        hits = 0;
        misses = 0;

        LevelManager.noteGeneration = true;
        LevelManager.isEncounterHappening = true;
        isHappening = true;

        Time.timeScale = 0;
    }


    public IEnumerator StopEncounter()
    {
        LevelManager.noteGeneration = false;

        yield return new WaitForSecondsRealtime(LevelManager.Instance.musicStartDelay);

        isHappening = false;
        LevelManager.isEncounterHappening = false;

        Time.timeScale = 1;
    }


    public void SolveEncounter()
    {
        float accuracy = (float)hits / (hits + misses);
        LevelManager.Instance.AcurracyConsequences(accuracy);

        SoundManager.Instance.StartSongLayer(songLayer);

        Destroy(this.gameObject);
    }


    public static Encounter GetCurrentEncounter()
    {
        Encounter encounter = null;
        foreach(Encounter currentEncounter in FindObjectsByType<Encounter>(FindObjectsSortMode.None))
            if(currentEncounter.isHappening)
                encounter = currentEncounter;
        return encounter;
    }
}
