using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptObjetoQueQuebra : MonoBehaviour
{
    public float tempoPraQuebrar;
    public float timer = 0f;
    public float tempoMax = 1.5f;
    public bool colisao = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(colisao == true)
        {
            if(timer >= tempoPraQuebrar)
            {
                Destroy(gameObject);
                Debug.Log("Destruiu!!");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisao");
        if (collision.gameObject.tag == "Player") 
        {
            float timerLimite = timer + tempoPraQuebrar;
            Debug.Log("Colisao foi com o player");
            colisao = true;
            tempoPraQuebrar = timer + tempoMax;
        }
    }
}
