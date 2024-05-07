using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScriptObjetoQueQuebra : MonoBehaviour
{

    public float timer = 0f;
    public float tempoPraQuebrar;
    public float tempoPraRespawn;
    public float tempoMax = 1.5f;
    public float tempoMaxRespawn = 5.0f;
    public bool colisao = false;
    public bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (colisao == true)
        {
            if (timer >= tempoPraQuebrar)
            {
                colisao = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                calculaRespawn();
                Debug.Log("Destruiu!!");
            }
        }

        if(isActive == false)
        {
            if(timer >= tempoPraRespawn)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                isActive = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && colisao != true)
        {
            colisao = true;
            tempoPraQuebrar = timer + tempoMax;
        }
    }

    private void calculaRespawn()
    {
        tempoPraRespawn = timer + tempoMaxRespawn;
        isActive = false;
        Debug.Log("Inativo");
    }
}
