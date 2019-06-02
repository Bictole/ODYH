using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool playerinrange;
    public Signal context;

    public bool canInteract;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canInteract)
        {
            context.Raise();
            playerinrange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canInteract)
        {
            context.Raise();
            playerinrange = false;
        }
        
    }
}
