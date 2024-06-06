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
        // Debug.Log("A");
        if(trigger == true && !hasPlayerEntered) 
        {
            // Debug.Log("B");
            switch (fase)
            {
                case 0:
                    PlayScene("HubCentral");
                    break;
                case 1:
                    Debug.Log("C");
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
