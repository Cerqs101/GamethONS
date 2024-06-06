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
    [SerializeField] public Animator animator;

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
}
