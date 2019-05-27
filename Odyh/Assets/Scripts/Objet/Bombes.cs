using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bombes", menuName = "Items/Bombes", order = 1)]
public class Bombes : Item, Utilisable
{
    
    
    public Sprite Sprite { get; }
    //fontion d'utilisation
    public void Use()
    {
        Delete_the_Item();
    }

    
}
