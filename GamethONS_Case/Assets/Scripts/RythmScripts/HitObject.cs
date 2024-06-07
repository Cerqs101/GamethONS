using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Interaction;
using Unity.VisualScripting;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    [SerializeField] private Sprite hitSprite;
    [SerializeField] private Sprite missSprite;
    [SerializeField] private Sprite passSprite;
    [SerializeField] private Color32 pressedColor = new Color32(200, 200, 200, 255);
    [SerializeField] private Color32 unpressedColor = new Color32(255, 255, 255, 255);
    [SerializeField] public KeyCode keyToPress = KeyCode.Z;

    [SerializeField] public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;

    private SpriteRenderer spriteRenderer;
    private HitDisplay hitDisplay;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitDisplay = transform.GetComponentInChildren<HitDisplay>();
    }


    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
            spriteRenderer.color = pressedColor;
        else if (Input.GetKeyUp(keyToPress))
            spriteRenderer.color = unpressedColor;

        if (LevelManager.isEncounterHappening)
            TryToHitBeat();
    }

    private void TryToHitBeat()
    {
        List<BeatObject> beats = FindAllBeats();
        if (beats.Count() != 0)
        {
            BeatObject closestBeat = GetClosestBeat(beats.ToArray());

            if (Input.GetKeyDown(keyToPress) && closestBeat.hittable)
                SolveHitAttempt(closestBeat);
        }
    }


    public void SolveHitAttempt(BeatObject beat)
    {
        Destroy(beat.gameObject);
        if (beat.distanceDifference <= 0.5)
            Hit();
        else
            Miss();
    }


    public List<BeatObject> FindAllBeats(){
        List<BeatObject> beats = new List<BeatObject>();
        foreach(BeatObject beat in FindObjectsByType<BeatObject>(FindObjectsSortMode.None))
            if(beat.noteName == noteRestriction)
                beats.Add(beat);
        return beats;
    }


    public void Hit()
    {
        SoundManager.Instance.PlayNoteHitSfx();
        Encounter.hits++;
        hitDisplay.SetTemporarySprite(hitSprite);
    }


    public void Miss(bool unclicked=false)
    {
        Sprite sprite;
        if(unclicked)
            sprite = passSprite;
        else
            sprite = missSprite;

        SoundManager.Instance.PlayNoteMissSfx();
        Encounter.misses++;
        hitDisplay.SetTemporarySprite(sprite);
    }


    public BeatObject GetClosestBeat(BeatObject[] beats)
    {
        BeatObject closestBeat = beats[0];
        foreach (BeatObject beat in beats)
            if (Mathf.Abs(closestBeat.distanceDifference) > Mathf.Abs(beat.distanceDifference))
                closestBeat = beat;
        return closestBeat;

    }
}
