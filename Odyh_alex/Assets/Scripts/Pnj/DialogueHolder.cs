using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : Interactable
{

    // To attach to a PNJ's dialogue box
    
    public string dialogue;

    private DialogueManager dialogueManager;

    // List of the different text which will appears into the dialogue box
    public string[] dialogueLines;

    
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        canInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        
        // If the collider of the Player and the holder are in contact, then if we push the F keycode then a dialogue box will appear (and the PNJ will stop move)
        if (Input.GetKeyUp(KeyCode.F) && !dialogueManager.dialogueActive && playerinrange)
        {
            dialogueManager.dialogueLines = dialogueLines;
            dialogueManager.ShowBox();
            dialogueManager.dialogueLines = dialogueLines;
            //dialogueManager.ShowDialogue();
            
            if (transform.parent != null)
            {
                if (transform.parent.GetComponent<Pnjmovement>() != null)
                {
                    transform.parent.GetComponent<Pnjmovement>().stopmove = true;
                }
            }
        }
    }

}
