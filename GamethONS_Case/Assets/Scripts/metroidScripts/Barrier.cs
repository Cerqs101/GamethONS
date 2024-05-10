using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private int initialEncounterAmount;
    [SerializeField] private int limitEncounterAmout;

    void Start()
    {
        initialEncounterAmount = FindObjectsByType<Encounter>(FindObjectsSortMode.None).Count();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
            if(FindObjectsByType<Encounter>(FindObjectsSortMode.None).Count() <= initialEncounterAmount - limitEncounterAmout)
                Destroy(this.gameObject);
            else
                if(GetComponent<DialogInitializer>() != null)
                    GetComponent<DialogInitializer>().InitializeDialog();

    }
}
