using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInitializer : MonoBehaviour
{
    [SerializeField] private int dialogBlockIndex;
    [SerializeField] private bool destroyAfterActivated = true;


    private DialogBox dialogBox;

    void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            dialogBox.ActivateDialogBox();
            dialogBox.WriteDialogueBlock(1);

            if(destroyAfterActivated)
                Destroy(this);
        }
    }
}
