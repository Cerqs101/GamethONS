using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptPortaisFases : MonoBehaviour
{
    [SerializeField] private Player player;
    
    public int fase = 0;
    public bool ehPortalFimDeFase = false;
    public bool trigger = false;
    public Player.Habilidades habilidadeQueDa = Player.Habilidades.None;
    
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
        Debug.Log("A");
        if(trigger == true) 
        {
            Debug.Log("B");
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
                    SceneManager.LoadScene("Score", LoadSceneMode.Additive);
                    break;
            }

            switch (habilidadeQueDa)
            {
                case Player.Habilidades.WallJump:
                    player.GetWallJump();
                    break;
                case Player.Habilidades.Dash:
                    player.GetDash();
                    break;
                case Player.Habilidades.None: default:
                    break;
            }

            trigger = false;
        }

    }

    private void PlayScene(string cena)
    {
        Debug.Log("D");
        UnityEngine.SceneManagement.SceneManager.LoadScene(cena, LoadSceneMode.Single);
    }
}
