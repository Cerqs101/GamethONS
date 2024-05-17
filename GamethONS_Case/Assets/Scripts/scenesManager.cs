using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    // Start is called before the first frame update
    public static string lastSceneLoaded;

    private void Start()
    {
        instance = this;
    }


    public void EndGame()
    {
        Application.Quit();
    }


    public void PlayCredits()
    {
        StartCoroutine(PlayScene("Creditos"));
    }


    public void GoToMainMenu(bool fade=true)
    {
        StartCoroutine(PlayScene("Menu Principal", fade));
    }


    public void Plays()
    {
        StartCoroutine(PlayScene("FaseWallJump", true));
    }
    public void GoToSetting(){
        StartCoroutine(PlayScene("Opcoes",true));
    }

    public void GoToMainHub()
    {
        StartCoroutine(PlayScene("HubCentral", true));
    }


    public IEnumerator PlayScene(string scene, bool fade=false, LoadSceneMode loadSceneMode=LoadSceneMode.Single)
    {
        if(fade)
        {
            SceneFadeObject sceneFadeObject = SceneFadeObject.instance;
            StartCoroutine(sceneFadeObject.FadeIn());
            yield return new WaitForSeconds(sceneFadeObject.waitTime);
        }
        lastSceneLoaded = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, loadSceneMode);
    }

}
