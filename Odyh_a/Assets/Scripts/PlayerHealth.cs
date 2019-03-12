using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Vie max du joueur 
    public int playerMaxHealth;
    //vie actuelle du joueur
    public int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        //set de la vie au max
        playerHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
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
