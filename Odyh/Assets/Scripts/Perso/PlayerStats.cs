using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //niveau du joueur
    public int playerLevel;

    //xp actuel du joueur
    public int playerexp;

    //variable d'exp nécessaire au prochain level
    public int[] expforLevelUp;

    public int[] lvlhp;
    public int[] lvlattack;
    public int[] lvldefence;

    public int playerhp;
    public int playerattack;
    public int playerdefence;

    private PlayerHealth manager;
    
    public GameObject Xpburst;

    // Start is called before the first frame update
    void Start()
    {
        playerhp = lvlhp[1];
        playerattack = lvlattack[1];
        playerdefence = lvldefence[1];

        manager = FindObjectOfType<PlayerHealth>();
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
        Instantiate(Xpburst, transform.position, transform.rotation);
        playerLevel += 1;
        playerexp = 0;

        playerhp = lvlhp[playerLevel];        //on prend la valeure associée dans le tableau 
        manager.playerMaxHealth = playerhp;                            // on change les hp max 
        manager.playerHealth += playerhp - lvlhp[playerLevel - 1];     //on augmente la vie actuelle avec la différence de hp entre niveau actuel et précédent

        playerattack = lvlattack[playerLevel];

        playerdefence = lvldefence[playerLevel];
    }
}

