using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private TextMeshProUGUI textObject;

    [SerializeField] private TextAsset textFile;
    private List<string[]> textMatrix = new List<string[]>();

    private int currentLine = 0;
    [SerializeField] private bool startOnAwakening = false;
    private int currentDialogBlock = 0;


    private Player player;
    [SerializeField] public RawImage videoWindow;

    void Start()
    {
        player = FindObjectOfType<Player>();
        // textObject = textBox.GetComponentInChildren<TextMeshPro>();

        // if(textFile != null)
        string[] textBlocks = textFile.text.Split("\n\r\n");
        foreach(string textBlock in textBlocks)
            textMatrix.Add(textBlock.Split("\n"));

        if(startOnAwakening)
        {
            ActivateDialogBox();
            WriteDialogueBlock(0);
        }
        else
            DeactivateDialogBox();
    }

    // Update is called once per frame
    void Update()
    {

        if(!textBox.activeInHierarchy)
            return;

        if(currentLine >= textMatrix[currentDialogBlock].Count())
            DeactivateDialogBox();

        WriteDialogueBlock(currentDialogBlock);
        if(Input.GetKeyDown(KeyCode.X))
            currentLine++;
        else if(Input.GetKeyDown(KeyCode.Z))
            currentLine--;
        else if(Input.GetKeyDown(KeyCode.C))
            DeactivateDialogBox();

        if(currentLine < 0)
            currentLine = 0;
    }


    public void DeactivateDialogBox()
    {
        player._canMove = true;
        textBox.SetActive(false);
        videoWindow.color = new Color32(255,255,255,0);
        currentLine = 0;
    }


    public void ActivateDialogBox()
    {
        player.StopMovemnt();
        player._canMove = false;
        textBox.SetActive(true);
    }


    public void WriteDialogueBlock(int dialogBlockIndex)
    {
        currentDialogBlock = dialogBlockIndex;
        if(textBox.activeInHierarchy)
            textObject.text = textMatrix[dialogBlockIndex][currentLine];
    }
}
