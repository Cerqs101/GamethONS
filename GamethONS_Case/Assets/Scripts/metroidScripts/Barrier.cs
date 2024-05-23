using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [NonSerialized] public int initialEncounterAmount;
    [SerializeField] public int minimunEncounterAmount;

    [SerializeField] private int minimunLaneAmount = 0;


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
        {
            if(FindObjectsByType<Encounter>(FindObjectsSortMode.None).Count() <= initialEncounterAmount - minimunEncounterAmount
            && FindObjectsByType<LaneWindow>(FindObjectsSortMode.None).Count() >= minimunLaneAmount)
                Destroy(this.gameObject);

            else if(GetComponent<DialogInitializer>() != null)
                if(FindObjectsByType<LaneWindow>(FindObjectsSortMode.None).Count() < minimunLaneAmount)
                    GetComponent<DialogInitializer>().InitializeDialog(1);
                else if(FindObjectsByType<Encounter>(FindObjectsSortMode.None).Count() > initialEncounterAmount - minimunEncounterAmount)
                    GetComponent<DialogInitializer>().InitializeDialog();
        }
    }
}
