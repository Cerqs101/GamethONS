using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButaoPontos : MonoBehaviour
{
    public GameObject buttonPontos;
    // Start is called before the first frame update
    void Start()
    {
        if(!SaveSystem.CheckUpgrade("Zerou"))
            buttonPontos.SetActive(false);
    }
}
