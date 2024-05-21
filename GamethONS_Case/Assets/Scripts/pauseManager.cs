using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseCanvas;

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
                pauseCanvas.SetActive(false);

            }
            if(Time.timeScale == 0){
                Time.timeScale = 1;
                pauseCanvas.SetActive(true);
            }
        }
    }
}
