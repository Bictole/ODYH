using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Flèche", menuName = "Items/Flèche", order = 1)]
public class Flèche : Item, Utilisable
{
    
    
    public Sprite Sprite { get; }
    
    //fontion d'utilisation
    public void Use()
    {
            Delete_the_Item();
    }
    
    
}
