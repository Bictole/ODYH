using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Slider pour la barre de vie
    public Slider healthbar;
    
    public Slider xpbar;

    public Text hp;
    
    // reference au script pour retrouver la vie du joueur 
    public PlayerHealth playerH;

    private static bool UIexists;

    //recuperation du playerstats
    private PlayerStats _playerStats;

    public Text Textlevel;

    //UI script et son gettter 
    private static UI ui;
    public static UI UserInterface
    {
        get
        {
            if (ui == null)
            {
                ui = FindObjectOfType<UI>();
            }
            return ui;    
        }
    }
    
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
        _playerStats = playerH.GetComponent<PlayerStats>();
    }

    // A chaque update, on met à jour la valeur de la barre de vie ainsi que l'affichage du nombre de point de vie et du niveau du joueur.
    void Update()
    {
        healthbar.maxValue = playerH.playerMaxHealth;
        healthbar.value = playerH.playerHealth;
        xpbar.maxValue = _playerStats.expforLevelUp[_playerStats.playerLevel];
        xpbar.value = _playerStats.playerexp;
        hp.text = "HP : " + playerH.playerHealth + "/" + playerH.playerMaxHealth;
        Textlevel.text = "Lvl : " + _playerStats.playerLevel;
    }

    
}
