using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptPortaisFases : MonoBehaviour
{
    public int fase = 0;
    public bool ehPortalFimDeFase = false;
    [NonSerialized] public bool trigger = false;
    [NonSerialized] private bool hasPlayerEntered = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if(ehPortalFimDeFase == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            trigger = true;
        }
    }

    private void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if(trigger == true && !hasPlayerEntered) 
        {
            if(ehPortalFimDeFase){
                if(SceneManager.GetActiveScene().name == "Fase1"){
                    SaveSystem.SetUpgrade("WallJump",true);
                }
                else if(SceneManager.GetActiveScene().name == "FaseWallJump"){
                    SaveSystem.SetUpgrade("Dash",true);
                }
                if(SceneManager.GetActiveScene().name =="Fase3"){
                    SaveSystem.SetUpgrade("Zerou",true);
                    PlayScene("Tela Final");
                    return;
                }
            }
            switch (fase)
            {
                case 0:
                    ScenesManager.instance.GoToMainHub();
                    break;
                case 1:
                    PlayScene("Fase1");
                    break;
                case 2:
                    PlayScene("FaseWallJump");
                    break;
                case 3:
                    PlayScene("Fase3");
                    break;
                case 4:
                    PlayScene("Score", loadSceneMode:LoadSceneMode.Additive);
                    break;
            }
            trigger = false;
        }

    }

    private void PlayScene(string cena, LoadSceneMode loadSceneMode=LoadSceneMode.Single)
    {
        hasPlayerEntered = true;
        StartCoroutine(ScenesManager.instance.PlayScene(cena, fade:true, loadSceneMode));
    }
}
