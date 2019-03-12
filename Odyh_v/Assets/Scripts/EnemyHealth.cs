using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Vie max du monstre 
    public int monsterMaxHealth;
    //vie actuelle du monstre
    public int monsterHealth;

    //recupération du script stats
    private PlayerStats _playerStats;

    public int experience;
    
    public float waitToRespawn;
    public GameObject theMonster;
    
    public float spawnX;
    public float spawnY;

    // Start is called before the first frame update
    void Start()
    {
        //set de la vie au max
        monsterHealth = monsterMaxHealth;

        _playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterHealth <= 0)
        {
            gameObject.SetActive(false);
            waitToRespawn -= Time.deltaTime;
            if (waitToRespawn < 0)
            {
                _playerStats.GainExp(experience);
                theMonster.transform.position = new Vector2(spawnX, spawnY);
                theMonster.SetActive(true);
                waitToRespawn = 2;
            }
            
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
