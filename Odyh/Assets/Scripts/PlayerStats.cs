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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si l'expérience est supérieure à l'expérience nécéssaire pour lvl up, augmente le niveau du joueur
        if (playerexp >= expforLevelUp[playerLevel])
        {
            playerLevel += 1;
        }
    }

    // Ajoute de l'expérience au personnage
    public void GainExp(int gain_exp)
    {
        playerexp += gain_exp;
    }
}
