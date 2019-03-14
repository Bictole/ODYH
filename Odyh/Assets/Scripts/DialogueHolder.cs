using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If the collider of the Player and the PNJ are in contact and stay like this, then if we push the Space bar then a dialogue box will appear and the PNJ will stop move
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && Input.GetKeyUp(KeyCode.Space))
        {
            if (!dialogueManager.dialogueActive)
            {
                dialogueManager.ShowBox(dialogue);
                dialogueManager.dialogueLines = dialogueLines;
                dialogueManager.currentLine = 0;
                dialogueManager.ShowDialogue();
            }

            if (transform.parent.GetComponent<Pnjmovement>() != null)
            {
                transform.parent.GetComponent<Pnjmovement>().stopmove = true;
            }
        }
    }
}
