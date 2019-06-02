using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}
public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType doortype;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;

    private void Start()
    {
        doorSprite = GetComponentInParent<SpriteRenderer>();
        playerInventory = FindObjectOfType<Inventory>();
        canInteract = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerinrange && doortype == DoorType.key)
        {
            if (playerInventory.Keyavailable())
            {
                OpenwithKey();
            }
        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
        canInteract = false;
    }
    
    public void OpenwithKey()
    {
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
        context.Raise();
        canInteract = false;
    }

    public void Close()
    {
        //Turn off the door's sprite renderer
        doorSprite.enabled = true;
        //set open to true
        open = false;
        //turn off the door's box collider
        physicsCollider.enabled = true;
        
        canInteract = true;
    }
}
