using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public Slider audioSlider;
    public AudioMixer audioVolume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeVolume(){
        audioVolume.SetFloat("MasterAudio",audioSlider.value);
    }

    public static IEnumerator FadeOut(AudioSource songLayer, float waitTime=1f)
    {
        float intensity = 0.01f;
        float waitPerLoop = waitTime*intensity;
        while(songLayer.volume > 0)
        {
            songLayer.volume -= intensity;
            yield return new WaitForSecondsRealtime(waitPerLoop);
        }
    }
}
