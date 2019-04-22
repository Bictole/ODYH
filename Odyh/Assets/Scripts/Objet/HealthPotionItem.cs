using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
public class HealthPotionItem : Item, Utilisable
{
    //nbre d'hp rendu par la potion
    [SerializeField] 
    private int healthgain;
    
    
    public Sprite Sprite { get; }

    //fontion d'utilisation
    public void Use()
    {
        if (PlayerHealth.TheHealth.playerHealth < PlayerHealth.TheHealth.playerMaxHealth)
        {
            Delete_the_Item();

            PlayerHealth.TheHealth.playerHealth += healthgain;
            
        } 
    }
}
