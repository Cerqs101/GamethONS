using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptLogic : MonoBehaviour
{
    public int vidaPlayer;
    public Text textoVida;
    public Player player;

    public void subtraiVida()
    {
        textoVida.text = player.vidaAtual.ToString();
    }
}
