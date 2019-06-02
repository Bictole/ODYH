using System;
using UnityEngine;

[Serializable]
public class Block
{
    // Correspond aux deux block à activer
    [SerializeField]
    private GameObject first,second;


    // Active les deux blocks
    public void Deactivate()
    {
        first.SetActive(false);
        second.SetActive(false);
    }
    
    // Désactive les deux blocks
    public void Activate()
    {
        first.SetActive(true);
        second.SetActive(true);
    }
}

