using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;

    public bool dialogueActive;

    public string[] dialogLines;
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

            if (currentLine >= dialogLines.Length)
            {
                dialogueBox.SetActive(false);
                dialogueActive = false;

                currentLine = 0;
                thePlayer.stopmove = false;
            }

            dialogueText.text = dialogLines[currentLine];

        }
    }


    public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
        thePlayer.stopmove = true;

    }

    public void ShowDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        thePlayer.stopmove = true;
    }
}
