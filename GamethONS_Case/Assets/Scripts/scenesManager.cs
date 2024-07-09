using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    public GameObject inicio;
    // Start is called before the first frame update
    public static string previousScene;

    private void Start()
    {
        instance = this;
        
    }
    void Update(){
        
    }


    public void EndGame()
    {
        Application.Quit();
    }

    public void ActivateScreen(GameObject screen)
    {
        inicio.SetActive(false);
        screen.SetActive(true);
    }


    public void DeactivateScreen(GameObject screen)
    {
        screen.SetActive(false);
        inicio.SetActive(true);
    }


    public void GoToPreviousScene()
    {
        StartCoroutine(PlayScene(previousScene, true));

    }

    public void Plays()
    {
        StartCoroutine(PlayScene("FaseWallJump", true));
    }

    public void GoToTutorial(){
        if(SaveSystem.GetTutorial()!= 1){
            SaveSystem.ResetAll();
            StartCoroutine(PlayScene("Tutorial", true));
            StartCoroutine(MusicController.FadeOut(FindObjectOfType<AudioSource>()));
        }
        else
            GoToMainHub();
    }

    public void GoToMainHub()
    {
        SaveSystem.SetTutorial(1);
        LevelManager.hasLevelStarted = false;
        StartCoroutine(PlayScene("HubCentral", true));
    }

    public void goToMenu(){
        LevelManager.hasLevelStarted = false;
        SoundManager.Instance.FadeOutAllSongLayers();
        StartCoroutine(PlayScene("Menu Principal", true));
    }

    public void goToFinalScene(){
        StartCoroutine(PlayScene("Tela Final",true));
    }



    public IEnumerator PlayScene(string scene, bool fade=false, LoadSceneMode loadSceneMode=LoadSceneMode.Single)
    {
        if(fade)
        {
            SceneFadeObject sceneFadeObject = SceneFadeObject.instance;
            sceneFadeObject.Fade("in");
            yield return new WaitForSecondsRealtime(sceneFadeObject.waitTime);
        }

        if(FindObjectOfType<pauseManager>() != null)        
            FindObjectOfType<pauseManager>().UnpauseGame();

        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, loadSceneMode);
    }

}
