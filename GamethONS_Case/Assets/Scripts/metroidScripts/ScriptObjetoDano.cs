using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptObjetoDano : MonoBehaviour
{
    public Player player;
    public int dano = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("a");
        if(other.gameObject.tag == "Player"){
            player.AplicaDano(dano);
            Debug.Log("Dano!!");
        }
    }
}
