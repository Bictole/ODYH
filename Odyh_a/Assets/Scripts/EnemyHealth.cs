using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    //Vie max du monstre 
    public int monsterMaxHealth;
    //vie actuelle du monstre
    public int monsterHealth;

    public bool reloading;
    

    // Start is called before the first frame update
    void Start()
    {
        //set de la vie au max
        monsterHealth = monsterMaxHealth;
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterHealth <= 0)
        {
            reloading = true;
            gameObject.SetActive(false);
            monsterHealth = monsterMaxHealth;
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