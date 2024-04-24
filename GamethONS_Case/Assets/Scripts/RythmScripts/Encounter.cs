using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    [SerializeField] public AudioSource songLayer;
    [SerializeField] private float measuresInEncounter = 4f;
    public double secondsPerEncounter;  

    public bool isHappening = false;


    void Start()
    {
        secondsPerEncounter = measuresInEncounter * LevelManager.Instance.measureDuration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Colisao foi com o player");
            StartCoroutine(LevelManager.Instance.Encounter(this.gameObject, measuresInEncounter));
        }
    }
}
