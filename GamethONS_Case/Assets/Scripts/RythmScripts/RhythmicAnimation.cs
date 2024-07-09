using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RhythmicAnimation : RhythmMonoBehaviour
{
    private Animation animationClip;
    [SerializeField] private AnimationClip Idle;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animationClip = GetComponent<Animation>();
        animator      = GetComponent<Animator>();

        animationClip.wrapMode = WrapMode.Once;
        
        SetTimeStamps(FindObjectOfType<LaneContainer>().GetDataFromMidi());
    }

    // Update is called once per frame
    void Update()
    {
        if(!timeStamps.Any() && LaneContainer.midiFile != null)
            SetTimeStamps(FindObjectOfType<LaneContainer>().GetDataFromMidi());
            
        PerformInEveryBeat(PlayAnimation);
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
