using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class pauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseCanvas;
    [System.NonSerialized] public bool paused = false;
    public GameObject volumeCanvas;
    public GameObject optionsCanvas;
    public Slider volumeSlider;
    public AudioMixer pauseAudio;

    void Start()
    {
        pauseCanvas.SetActive(false);
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        bool isEncounterHappening = LevelManager.isEncounterHappening;
        if(Input.GetKeyDown(KeyCode.Escape) && !isEncounterHappening){
            if(Time.timeScale == 1)
                PauseGame();
            else if(Time.timeScale == 0)
                UnpauseGame();
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


    public void PauseGame(){
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        optionsCanvas.SetActive(true);
        volumeCanvas.SetActive(false);
        paused = true;
    }


    public void UnpauseGame(){
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        paused = true;
    }
}
