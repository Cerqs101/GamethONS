using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class BeatObject : MonoBehaviour
{
    public float noteSpeed;
    private bool hittable = false;
    // private int id = 0;

    private HitController hit;
    private SoundManager musicPlayer;


    void Start()
    {
        noteSpeed = FindObjectOfType<LevelManager>().bpm * 4 / 60f;
        hit = FindObjectOfType<HitController>();
        musicPlayer = FindObjectOfType<SoundManager>();
    }


    void Update()
    {
        if(LevelManager.hasLevelStarted)
            MoveBeat(-noteSpeed*Time.unscaledDeltaTime, 0);

        hittable = determinateHittability();
            
        if(hittable && (Input.GetKeyDown(hit.keyToPress) || Input.GetKeyDown(hit.keyToPress2)))
        {
            Destroy(this.gameObject);
            musicPlayer.PlayNoteHitSfx();
        }
    }


    void MoveBeat(float x, float y){
        transform.position += new Vector3(x, y, 0f);
    }


    bool determinateHittability()
    {
        Vector3 beatPosition = transform.position;
        Vector3 hitPosition  = hit.transform.position;

        float distanceDifference = Mathf.Abs(beatPosition.x - hitPosition.x);

        if(distanceDifference <= 1)
            return true;
        else
            return false;
    }


    // void OnTriggerEnter2D(Collider2D other) {
    //     hittable = true;
    //     //id+=1;
    //     Debug.Log(id);
    // }


    // void OnTriggerExit2D(Collider2D other) {
    //     hittable = false;
    //     musicPlayer.PlayNoteMissSfx();
    // }

}
