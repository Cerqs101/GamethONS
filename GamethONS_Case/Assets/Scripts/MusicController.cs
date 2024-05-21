using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public Slider audioMaster;
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
        audioVolume.SetFloat("MasterAudio",audioMaster.value);
    }
}
