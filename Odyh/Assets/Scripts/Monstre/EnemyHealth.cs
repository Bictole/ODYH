using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    
    
    
    
    //Vie max du monstre 
    public int monsterMaxHealth;
    //vie actuelle du monstre
    public int monsterHealth;

    // Script avec l'xp du personnage
    private PlayerStats _playerStats;
    public int experience;
    
    
    public Slider healthbar;
    
    // Start is called before the first frame update
    void Start()
    {
        //set de la vie au max
        SetMaxHealth();
        _playerStats = FindObjectOfType<PlayerStats>();
        healthbar.maxValue = monsterMaxHealth;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = monsterHealth;
        if (monsterHealth <= 0)
        {
            _playerStats.GainExp(experience);
            
            RandomSpawn.nb_enemy -= 1;
            RandomSpawn.reloading = true;
            Destroy(gameObject);
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