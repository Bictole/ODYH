using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    //Mana max du joueur 
    public int playerMaxMana;
    
    //Mana actuelle du joueur
    public float playerMana;

    public bool manaempty;

    private SFXManager sfx;  //reference au manager d'effets sonores

    private static PlayerHealth pm;

    public static PlayerHealth TheMana
    {
        get
        {
            if (pm == null)
            {
                pm = FindObjectOfType<PlayerHealth>();
            }
            return pm; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //set du mana au max
        SetMaxMana();
    }

    // Update is called once per frame
    void Update()
    {
        playerMana += 0.05f;
        if (playerMana <= 0)
        {
            manaempty = true;
        }
        else
        {
            manaempty = false;
        }
        if (playerMana > playerMaxMana)
        {
            SetMaxMana();
        }
    }

    public void Usespell(int cost)
    {
        float test = playerMana;
        if (test - cost >= 0)
            playerMana -= cost;
        else
        {
            manaempty = true;
        }
    }

    public void SetMaxMana()
    {
        playerMana = playerMaxMana;
    }
}
