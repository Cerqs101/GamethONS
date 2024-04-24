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

    public bool isHappening = false;


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
        {
            Debug.Log("Colisao foi com o player");
            StartCoroutine(MakeEncounter(measuresInEncounter));
        }
    }

    public IEnumerator MakeEncounter(float durationInMeasures = 4f)
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

        // yield return new WaitForSecondsRealtime((float)(measureDuration*durationInMeasures));
        // StartCoroutine(StopEncounter(obj));
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

}
