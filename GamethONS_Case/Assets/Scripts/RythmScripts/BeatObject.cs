using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class BeatObject : MonoBehaviour
{
    public float noteSpeed;
    public bool hittable = false;
    public int id;
    private static int currentSpawnId = 0;
    public float distanceDifference;

    private HitController hit;
    private SoundManager musicPlayer;


    void Start()
    {
        noteSpeed = FindObjectOfType<LevelManager>().bpm * 4 / 60f;
        hit = FindObjectOfType<HitController>();
        musicPlayer = FindObjectOfType<SoundManager>();
        id = currentSpawnId;
        currentSpawnId++;
    }


    void Update()
    {
        Vector3 beatPosition = transform.position;
        Vector3 hitPosition  = hit.transform.position;

        distanceDifference = beatPosition.x - hitPosition.x;

        if(Mathf.Abs(distanceDifference) <=1)
            hittable = true;
        
        if(distanceDifference < -5)
            Destroy(this.gameObject);

        if(LevelManager.hasLevelStarted)
            MoveBeat(-noteSpeed*Time.unscaledDeltaTime, 0);

    }


    void MoveBeat(float x, float y){
        transform.position += new Vector3(x, y, 0f);
    }


    // bool determinateHittability(float distanceLimit=1f, float distanceDifference=0f)
    // {
    //     if(distanceDifference == 0f){
    //         Vector3 beatPosition = transform.position;
    //         Vector3 hitPosition  = hit.transform.position;

    //         distanceDifference = Mathf.Abs(beatPosition.x - hitPosition.x);
    //     }

    //     if(distanceDifference <= 1)
    //         return true;
    //     else
    //         return false;
    // }

}
