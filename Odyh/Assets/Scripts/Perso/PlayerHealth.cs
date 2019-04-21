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

    private SFXManager sfx;  //reference au manager d'effets sonores

    
    private static PlayerHealth ph;

    public static PlayerHealth TheHealth
    {
        get
        {
            if (ph == null)
            {
                ph = FindObjectOfType<PlayerHealth>();
            }
            return ph; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //set de la vie au max
        SetMaxHealth();
        reloading = false;

        sfx = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // si la vie est <0 alors lancement de la séquence de réapparition et désactivation de l'objet + effets sonores
        if (playerHealth <= 0)
        {
            sfx.player_is_dead.Play();

            reloading = true;
            gameObject.SetActive(false);
            SetMaxHealth();
        }

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }

    public void HurtPlayer(int damage)
    {
        playerHealth -= damage;

        sfx.hurt_the_player.Play();
    }

    public void SetMaxHealth()
    {
        playerHealth = playerMaxHealth;
    }
}
