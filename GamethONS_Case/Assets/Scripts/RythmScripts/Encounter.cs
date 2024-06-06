using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Encounter : MonoBehaviour
{
    [SerializeField] private string songLayerName;
    private AudioSource songLayer;
    [SerializeField] public ScriptPortaisFases portalFimDeFase;
    [SerializeField] private LaneWindow laneWindow;
    // [SerializeField] public Animator animator;

    [SerializeField] public float measuresInEncounter = 4f;
    [NonSerialized] public double secondsInEncounter;
    public static int hits = 0;
    public static int misses = 0;
    
    [NonSerialized] public bool isHappening = false;

    public Player.Habilidades habilidadeQueDa = Player.Habilidades.None;
    private Player player;
    

    void Start()
    {
        secondsInEncounter = measuresInEncounter * LevelManager.Instance.measureDuration;

        foreach(AudioSource song in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
            if(song.gameObject.name.ToLower() == songLayerName.ToLower())
                songLayer = song;

        player = FindObjectOfType<Player>();
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
        yield return new WaitForSecondsRealtime((float)secondsInEncounter);
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
        ScoreManager.Instance.levelHits += hits;
        ScoreManager.Instance.levelCompletedEncounters++;

        float accuracy;
        if((hits + misses) != 0)
        {
            accuracy = (float)hits / (hits + misses);
            ScoreManager.Instance.UpdateOverallAccuray(accuracy);
        }
        else
            accuracy = LevelManager.Instance.midAccuracyThreshold;

        LevelManager.Instance.AcurracyConsequences(accuracy);

        if(songLayer != null && songLayer.volume < 0.99f)
        {
            SoundManager.Instance.StartSongLayer(songLayer);
            SoundManager.activeSongLayers.Add(songLayer);
            SoundManager.wasSongLayerAdded = true;
        }
        if(laneWindow != null && !laneWindow.gameObject.activeInHierarchy)
        {
            laneWindow.gameObject.SetActive(true);
            LaneContainer.activeLanes.Add(laneWindow.GetComponentInChildren<HitObject>().keyToPress);
            FindObjectOfType<LaneContainer>().wasLaneAdded = true;
        }

        if (portalFimDeFase != null && measuresInEncounter == 12f)
            portalFimDeFase.gameObject.SetActive(true);
        
        switch (habilidadeQueDa)
        {
            case Player.Habilidades.WallJump:
                player.GetWallJump();
                break;
            case Player.Habilidades.Dash:
                player.GetDash();
                break;
            case Player.Habilidades.None: default:
                break;
        }
        
        // animator.SetBool("Acabou", true);
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponentInChildren<BoxCollider2D>());
        Destroy(this);
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
