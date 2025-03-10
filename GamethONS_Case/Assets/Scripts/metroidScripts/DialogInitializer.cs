using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Video;

public class DialogInitializer : MonoBehaviour
{
    [SerializeField] private string songLayerName;
    [SerializeField] private int dialogBlockIndex;
    [SerializeField] private bool destroyAfterActivated = true;
    [SerializeField] private bool triggerWhenEnter = true;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Texture video;
    private DialogBox dialogBox;

    void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        if(audioSource != null)
            SoundManager.Instance.addToSongLayers(audioSource);

        foreach(AudioSource song in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        if(song.gameObject.name.ToLower() == songLayerName.ToLower())
            audioSource = song;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerWhenEnter && other.tag == "Player")
            InitializeDialog();
    }

    public void InitializeDialog(int dialogBlock = -1)
    {
        Debug.Log("Inittializer");
        if(dialogBlock == -1)
            dialogBlock = dialogBlockIndex;

        if(video != null)
        {
            dialogBox.videoWindow.texture = video;
            dialogBox.videoWindow.color   = new Color32(255,255,255,255);
        }

        dialogBox.ActivateDialogBox();
        dialogBox.WriteDialogueBlock(dialogBlock);
        if (audioSource != null){
            audioSource.volume = 1;
            Debug.Log("Volume = 1");
        }

        if (destroyAfterActivated)
            Destroy(this);
    }
}
