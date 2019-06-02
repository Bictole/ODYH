﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //ref au script attaché a l'inventaire
    private static PlayerStats playerStats;
    
    public static PlayerStats PlayerStatsScr
    {
        get
        {
            if (playerStats == null)
            {
                playerStats = FindObjectOfType<PlayerStats>();
            }

            return playerStats;
        }
    }
    
    
    //niveau du joueur
    public int playerLevel;

    //xp actuel du joueur
    public int playerexp;

    //variable d'exp nécessaire au prochain level
    public int[] expforLevelUp;

    public int[] lvlhp;
    public int[] lvlattack;
    public int[] lvldefence;

    private int playerhp;

    public int Playerhp
    {
        get => playerhp;
        set => playerhp = value;
    }

    private int playerattack;

    public int Playerattack
    {
        get => playerattack;
        set => playerattack = value;
    }

    private int playerdefence;

    public int Playerdefence
    {
        get => playerdefence;
        set => playerdefence = value;
    }


    private PlayerHealth _playerhealth;

    public PlayerHealth PlayerHealth
    {
        get => _playerhealth;
    }

    private PlayerMana _playerMana;

    public PlayerMana PlayerMana
    {
        get => _playerMana;
    }

    public GameObject Xpburst;

    // Start is called before the first frame update
    void Start()
    {
        playerhp = lvlhp[1];
        playerattack = lvlattack[1];
        playerdefence = lvldefence[1];

        _playerhealth = FindObjectOfType<PlayerHealth>();
        _playerMana = FindObjectOfType<PlayerMana>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si l'expérience est supérieure à l'expérience nécéssaire pour lvl up, augmente le niveau du joueur
        if (playerexp >= expforLevelUp[playerLevel])
        {
            levelup();
        }
    }

    // Ajoute de l'expérience au personnage
    public void GainExp(int gain_exp)
    {
        playerexp += gain_exp;
    }

    public void levelup()            //fontion de changement de niveau
    {
        Instantiate(Xpburst, transform.position, Xpburst.transform.rotation);
        playerLevel += 1;

        playerhp += lvlhp[playerLevel];        //on prend la valeure associée dans le tableau 
        _playerhealth.playerMaxHealth = playerhp;                            // on change les hp max 
        _playerhealth.playerHealth += playerhp - lvlhp[playerLevel - 1];     //on augmente la vie actuelle avec la différence de hp entre niveau actuel et précédent

        playerattack += lvlattack[playerLevel];

        playerdefence += lvldefence[playerLevel];
    }

    public int QuestXP(Quest q)
    {
        if (playerLevel <= q.QuestLevel + 5)
        {
            return q.ExperienceGiven;
        }        
        if (playerLevel == q.QuestLevel + 6)
        {
            return (int)(q.ExperienceGiven * 0.8/5)*5;
        }
        if (playerLevel == q.QuestLevel + 7)
        {
            return (int)(q.ExperienceGiven * 0.6/5)*5;
        }
        if (playerLevel == q.QuestLevel + 8)
        {
            return (int)(q.ExperienceGiven * 0.4/5)*5;
        }
        if (playerLevel == q.QuestLevel + 9)
        {
            return (int)(q.ExperienceGiven * 0.2/5)*5;
        }
        if (playerLevel >= q.QuestLevel + 10)
        {
            return (int)(q.ExperienceGiven * 0.1/5)*5;
        }
        else
        {
            return 0;
        }
    }

}

