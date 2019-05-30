using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable {

    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Boolvalue storedOpen;

    [Header("Signals and Dialog")]
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Animation")]
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.Runtimevalue;
        playerInventory = FindObjectOfType<Inventory>();
        if(isOpen)
        {
            anim.SetBool("opened", true);
            canInteract = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F) && playerinrange)
        {
            if (!isOpen)
            {
                // Open the chest
                OpenChest();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerinrange && isOpen)
        {
            ChestAlreadyOpen();
        }
    }

    public void OpenChest()
    {
        canInteract = false;
        // Dialog window on
        dialogBox.SetActive(true);
        // dialog text = contents text
        dialogText.text = contents.itemDescription;
        // add contents to the inventory
        playerInventory.AddInventoryItem(contents);
        //playerInventory.currentItem = contents;
        // raise the context clue
        context.Raise();
        // set the chest to opened
        isOpen = true;
        anim.SetBool("opened", true);
        storedOpen.initialvalue = isOpen;
    }

    public void ChestAlreadyOpen()
    {
        dialogBox.SetActive(false);
    }
}
