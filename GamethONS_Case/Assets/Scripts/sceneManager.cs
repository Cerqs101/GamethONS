using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void EndGame(){
        Application.Quit();
    }
    public void PlayCredits(){
        SceneManager.LoadScene("Creditos", LoadSceneMode.Single);
    }
        public void GoToMainMenu(){
        SceneManager.LoadScene("Menu Principal", LoadSceneMode.Single);
    }
        public void Plays(){
        SceneManager.LoadScene("FaseWallJump", LoadSceneMode.Single);
    }

}
