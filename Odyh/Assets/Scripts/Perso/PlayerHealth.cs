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
        // si la vie est <0 alors lancement de la séquence de réapparition et désactivation de l'objet
        if (playerHealth <= 0)
        {
            sfx.player_is_dead.Play();

            reloading = true;
            gameObject.SetActive(false);
            SetMaxHealth();
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
