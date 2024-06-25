using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmicAnimation : RhythmMonoBehaviour
{
    private Animation animationClip;
    [SerializeField] private AnimationClip Idle;
    private Animator animator;
    private int beatsInSong = 0;

    // Start is called before the first frame update
    void Start()
    {
        animationClip = GetComponent<Animation>();
        animator      = GetComponent<Animator>();

        Debug.Log(noteRestriction);

        animationClip.wrapMode = WrapMode.Once;
        
        SetTimeStamps(FindObjectOfType<LaneContainer>().GetDataFromMidi());

        beatsInSong = beatsInInterval(0, SoundManager.GetAudioLenght());
    }

    // Update is called once per frame
    void Update()
    {
        PerformInEveryBeat(PlayAnimation);
        // if(spawnIndex >= beatsInSong-1)
        //     spawnIndex = 0;
    }

    void PlayAnimation(){
        StartCoroutine(PlayAnimationCoroutine());
    }

    IEnumerator PlayAnimationCoroutine(){
        yield return new WaitForSecondsRealtime(LevelManager.Instance.musicStartDelay);
        if(animationClip.isPlaying)
        {
            animationClip.Stop(animationClip.clip.name);
            animationClip.Rewind(animationClip.clip.name);
            animator.Play(Idle.name, 0, 0.0f);
        }
        animator.Play(animationClip.clip.name, 0, 0.0f);
    }
}
