using System;
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

    private bool dialogueboxexists;
    
    private Character thePlayer;
    
    public float vitesse_écriture;

    private bool stopforeach;

    public string Holdername;
    
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();

        //gameObject.GetComponentInChildren<Image>().gameObject.SetActive(true);
            
        //dialogueBox = gameObject.GetComponentInChildren<Image>().gameObject;
        dialogueText = dialogueBox.GetComponentInChildren<Text>();
        //dialogueBox.SetActive(!dialogueBox.activeSelf);
        
//        if (!dialogueboxexists)
//        {
//            dialogueboxexists = true;
//            DontDestroyOnLoad(transform.gameObject);
//            DontDestroyOnLoad(dialogueBox.transform.gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//            Destroy(dialogueBox.gameObject);
//        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text.Length + Holdername.Length != dialogueLines[currentLine].Length + Holdername.Length)
            {
                dialogueText.fontStyle = FontStyle.Bold;
                dialogueText.text = Holdername;
                dialogueText.fontStyle = FontStyle.Normal;
                dialogueText.text += dialogueLines[currentLine];
                stopforeach = true;
            }
            else
            {
                stopforeach = false;
                currentLine += 1;

                if (currentLine >= dialogueLines.Length)
                {
                    dialogueBox.SetActive(false);
                    dialogueActive = false;

                    currentLine = 0;
                    thePlayer.stopmove = false;
                }
                else
                {
                    StartCoroutine(Type());
                }
            }
        }
    }


    // Open the dialogue Box and make the player stop moving
    public void ShowBox()
    {
        currentLine = 0;
        ShowDialogue();
//        dialogueText.text = dialogue;
        StartCoroutine(Type());
    }
    
    
    IEnumerator Type()
    {
        dialogueText.text = "";
        foreach(char lettre in dialogueLines[currentLine])
        {
            if (stopforeach)
            {
                yield break;
            }
            else
            {
                dialogueText.text += lettre;
                yield return new WaitForSeconds(vitesse_écriture);
            }
            
        }
    }

    
    public void ShowDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        thePlayer.stopmove = true;
        thePlayer.Resetanim();
        currentLine = 0;
    }
}
