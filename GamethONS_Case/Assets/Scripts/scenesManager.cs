using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    public GameObject inicio,creditos,opcoes;
    // Start is called before the first frame update
    public static string lastSceneLoaded;

    private void Start()
    {
        instance = this;
        creditos.SetActive(false);
        opcoes.SetActive(false);
    }


    public void EndGame()
    {
        Application.Quit();
    }


    public void PlayCredits()
    {
        inicio.SetActive(false);
        creditos.SetActive(true);
    }


    public void EndCredits()
    {
        creditos.SetActive(false);
        inicio.SetActive(true);
    }


    public void Plays()
    {
        StartCoroutine(PlayScene("FaseWallJump", true));
    }
    public void GoToSetting(){
        inicio.SetActive(false);
        opcoes.SetActive(true);
    }
    public void EndSetting(){
        opcoes.SetActive(false);
        inicio.SetActive(true);
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
