using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class BeatObject : MonoBehaviour
{
    public float noteSpeed;
    public bool hittable = false;
    public float distanceDifference;
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteName;

    private HitObject hit;


    void Start()
    {
        noteSpeed = FindObjectOfType<LevelManager>().bpm * 4 / 60f;
        foreach(HitObject currentHit in FindObjectsByType<HitObject>(FindObjectsSortMode.None))
            if(currentHit.noteRestriction == noteName)
                hit = currentHit;
    }


    void Update()
    {
        Vector3 beatPosition = transform.position;
        Vector3 hitPosition  = hit.transform.position;
        distanceDifference = beatPosition.x - hitPosition.x;

        if(LevelManager.hasLevelStarted)
            MoveBeat(-noteSpeed*Time.unscaledDeltaTime, 0);

        if(Mathf.Abs(distanceDifference) <= 1)
            hittable = true;
        
        if(hittable && distanceDifference < -1)
        {
            hittable=false;
            hit.Miss(true);
        }
        if(distanceDifference < -3)
            Destroy(this.gameObject);

    }


    void MoveBeat(float x, float y){
        transform.position += new Vector3(x, y, 0f);
    }
}
