using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Vie max du joueur 
    public int playerMaxHealth;
    
    //vie actuelle du joueur
    public int playerHealth;
    
    // Permet de savoir si le personnage est mort ou pas (afin de lancer la séquence de réapparition)
    public bool reloading;

   

    // Start is called before the first frame update
    void Start()
    {
        //set de la vie au max
        SetMaxHealth();
        reloading = false;

    }

    // Update is called once per frame
    void Update()
    {
        // si la vie est <0 alors lancement de la séquence de réapparition et désactivation de l'objet
        if (playerHealth <= 0)
        {
            reloading = true;
            gameObject.SetActive(false);
            SetMaxHealth();
        }
        
    }

    public void HurtPlayer(int damage)
    {
        playerHealth -= damage;
    }

    public void SetMaxHealth()
    {
        playerHealth = playerMaxHealth;
    }
}
