using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Items/Key", order = 1)]
public class Key : Item, Utilisable
{
    public Sprite Sprite { get; }
    
    //fontion d'utilisation
    public void Use()
    {
        Delete_the_Item();
    }
}
