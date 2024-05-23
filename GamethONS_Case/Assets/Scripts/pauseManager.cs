using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class pauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseCanvas;
    [System.NonSerialized]public bool paused = false;
    public GameObject volumeCanvas;
    public GameObject optionsCanvas;
    public Slider volumeSlider;
    public AudioMixer pauseAudio;
    public GameObject uiCanvas;

    void Start()
    {
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }
    public void Pause(){ 
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Time.timeScale == 1){
                Time.timeScale = 0;
                //Time.unscaledTime = 0;
                pauseCanvas.SetActive(true);
                optionsCanvas.SetActive(true);
                volumeCanvas.SetActive(false);
                paused = true;


            }
            else if(Time.timeScale == 0){
                Time.timeScale = 1;
                //Time.unscaledTime = 1;
                pauseCanvas.SetActive(false);
                paused = true;
            }
        }
    }
    
    public void EnterSoundManager(){
        optionsCanvas.SetActive(false);
        volumeCanvas.SetActive(true);
    }   
    public void ExitSoundManager(){
        volumeCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }
    public void ControlVolume(){
        pauseAudio.SetFloat("MasterAudio",volumeSlider.value);
    }
    public void Resume(){
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void UnactivateUi(){
        uiCanvas.SetActive(false);
    }
}
