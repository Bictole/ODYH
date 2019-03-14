using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // To attach to a UI dialogue box
    
    // UI of the dialogue box and its text
    public GameObject dialogueBox;
    public Text dialogueText;

    // To know if the dialogue box is active
    public bool dialogueActive;

    // // List of the different text which will appears into the dialogue box
    public string[] dialogueLines;
    public int currentLine;

    
    private Character thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine += 1;

            if (currentLine >= dialogueLines.Length)
            {
                dialogueBox.SetActive(false);
                dialogueActive = false;

                currentLine = 0;
                thePlayer.stopmove = false;
            }

            dialogueText.text = dialogueLines[currentLine];

        }
    }


    // Open the dialogue Box and make the player stop moving
    public void ShowBox(string dialogue)
    {
        ShowDialogue();
        dialogueText.text = dialogue;

    }

    
    public void ShowDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        thePlayer.stopmove = true;
    }
}
