using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Interaction;
using UnityEditor.PackageManager;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField] private Color32 pressedColor = new Color32(200, 200, 200, 255);
    [SerializeField] private Color32 unpressedColor = new Color32(255, 255, 255, 255);
    [SerializeField] public KeyCode keyToPress  = KeyCode.Z; 
    [SerializeField] public KeyCode keyToPress2 = KeyCode.X; 

    private BeatObject[] beats;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

        if(Input.GetKeyDown(keyToPress)||Input.GetKeyDown(keyToPress2))
            spriteRenderer.color = pressedColor;
        else if(Input.GetKeyUp(keyToPress)||Input.GetKeyUp(keyToPress2))
            spriteRenderer.color = unpressedColor;
        
        if(LevelManager.isEncounterHappening)
        {   
            spriteRenderer.enabled = true;

            beats = FindObjectsByType<BeatObject>(FindObjectsSortMode.None);
            if(beats.Count() != 0)
            {
                BeatObject closerBeat = beats[0];
                foreach(BeatObject beat in beats)
                    if(Mathf.Abs(closerBeat.distanceDifference) > Mathf.Abs(beat.distanceDifference))
                        closerBeat = beat;

                if(closerBeat.hittable && (Input.GetKeyDown(keyToPress) || Input.GetKeyDown(keyToPress2)))
                {
                    Destroy(closerBeat.gameObject);
                    SoundManager.instance.PlayNoteHitSfx();
                }
            }
        }
        else
            spriteRenderer.enabled = false;
            

        // Debug.Log(CompareArray());
    }
float CompareArray()
    {
        
        float position = Mathf.Abs(transform.position.x - beats[0].transform.position.x);
        foreach(BeatObject beat in beats)
        {
            float actualDiference = Mathf.Abs(transform.position.x - beat.transform.position.x);

            if(position > actualDiference)
            {
                position = actualDiference;
            }  
        }
        return position;
    }
}
