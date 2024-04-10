using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptGalinha : MonoBehaviour
{
    public int dano = 2;
    public int vidaMax = 6;
    public int vidaAtual;
    public Player player;
    public ScriptLogic logic;
    public Rigidbody2D corpoRigido;
    public float velocidade = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMax;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScriptLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisao aconteceu");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Colisao foi com o player");
            player.AplicaDano(dano);
            logic.subtraiVida();
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        Debug.Log("Colisao GALINHA");
        StartCoroutine(LevelManager.Instance.StartEncounter());
    }
}
