using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
public class HealthPotionItem : Item, Utilisable
{
    //nbre d'hp rendu par la potion
    [SerializeField] 
    private int healthgain;

    //Permet l'affichage du nombre de point de vie soigné
    public GameObject healnb;

    //Objet d'effet
    [SerializeField]
    private GameObject healburst;
    
    public Sprite Sprite { get; }

    //fontion d'utilisation
    public void Use()
    {
        if (PlayerHealth.TheHealth.playerHealth < PlayerHealth.TheHealth.playerMaxHealth)
        {
            Instantiate(healburst, FindObjectOfType<Player>().transform.position,
                FindObjectOfType<Player>().transform.rotation);
            Delete_the_Item();

            PlayerHealth.TheHealth.playerHealth += healthgain;
            
            var clone = Instantiate(healnb, PlayerHealth.TheHealth.transform.position, Quaternion.Euler(Vector3.zero));
            
            clone.GetComponent<FloatingNumbers>().damageNumber = healthgain;
            
        } 
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#d6d6d6>This Is A Fkin HealthPotion You Retard ??</color>");
    }
}
