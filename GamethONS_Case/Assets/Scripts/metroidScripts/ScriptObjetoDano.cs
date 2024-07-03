using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptObjetoDano : MonoBehaviour
{
    public Player player;
    public int dano = 2;
    [SerializeField] private bool waitCoolDown = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
            player.AplicaDano(dano, waitCoolDown);
    }
}
