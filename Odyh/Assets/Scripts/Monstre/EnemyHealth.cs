using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    //Vie max du monstre 
    public int monsterMaxHealth;
    //vie actuelle du monstre
    public int monsterHealth;

    // Script avec l'xp du personnage
    private PlayerStats _playerStats;
    public int experience;
    
    // Permet de savoir si l'ennemi est mort ou non pour lancer la séquence de réapparition
    public bool reloading;
    

    // Start is called before the first frame update
    void Start()
    {
        //set de la vie au max
        SetMaxHealth();
        _playerStats = FindObjectOfType<PlayerStats>();
        
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterHealth <= 0)
        {
            reloading = true;
            gameObject.SetActive(false);
            SetMaxHealth();         
            _playerStats.GainExp(experience);
        }
    }

    public void HurtEnemy(int damage)
    {
        monsterHealth -= damage;
    }

    public void SetMaxHealth()
    {
        monsterHealth = monsterMaxHealth;
    }
}