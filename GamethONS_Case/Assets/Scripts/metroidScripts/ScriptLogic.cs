using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptLogic : MonoBehaviour
{
    public int vidaPlayer;
    public Slider sliderVida;
    public Player player;

    public void subtraiVida()
    {
        sliderVida.value = player.vidaAtual;
    }
    void Update(){
        subtraiVida();
    }
}
