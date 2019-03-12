using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider healthbar;

    public Text hp;
    
    // reference au script pour retrouver la vie du joueur 
    public PlayerHealth playerH;

    private static bool UIexists;

    //recuperation du playerstats
    private PlayerStats _playerStats;

    public Text Textlevel;
    
    // Start is called before the first frame update
    void Start()
    {
        // Permet de ne pas détruire le UI quand on charge une scene -> sans duplication
        if (!UIexists)
        {
            UIexists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //init du playerstats
        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.maxValue = playerH.playerMaxHealth;
        healthbar.value = playerH.playerHealth;
        hp.text = "HP : " + playerH.playerHealth + "/" + playerH.playerMaxHealth;
        Textlevel.text = "Lvl : " + _playerStats.playerLevel;
    }
}
