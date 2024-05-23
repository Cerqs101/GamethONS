using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DialogInitializer : MonoBehaviour
{
    [SerializeField] private int dialogBlockIndex;
    [SerializeField] private bool destroyAfterActivated = true;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Texture video;
    private DialogBox dialogBox;

    void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        if(audioSource != null)
            SoundManager.Instance.addToSongLayers(audioSource);

        // if(audioSource != null)
        //     SoundManager.
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            InitializeDialog();
    }

    public void InitializeDialog(int dialogBlock = -1)
    {
        if(dialogBlock == -1)
            dialogBlock = dialogBlockIndex;

        if(video != null)
        {
            dialogBox.videoWindow.texture = video;
            dialogBox.videoWindow.color   = new Color32(255,255,255,255);
        }

        dialogBox.ActivateDialogBox();
        dialogBox.WriteDialogueBlock(dialogBlock);
        if (audioSource != null)
            audioSource.volume = 1;

        if (destroyAfterActivated)
            Destroy(this);
    }
}
