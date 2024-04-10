using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Player.PowerUps powerUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTROU AWHN");
        
        if (other.gameObject != player.gameObject) return;

        player.GetPowerUp(powerUp);

        Destroy(this.gameObject);
    }
}
