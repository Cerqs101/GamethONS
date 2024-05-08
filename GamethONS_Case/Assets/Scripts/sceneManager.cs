using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    // Start is called before the first frame update

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


    public IEnumerator PlayScene(string scene, bool fade=false)
    {
        if(fade)
        {
            SceneFadeObject sceneFadeObject = SceneFadeObject.instance;
            StartCoroutine(sceneFadeObject.FadeIn());
            yield return new WaitForSeconds(sceneFadeObject.waitTime);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

}
