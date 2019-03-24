using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    //Référence au texte 
    public Text textmoney;

    //argent actuel du joueur
    public int money;
    
    // Start is called before the first frame update
    void Start()
    {
        //on doit vérifier que le playerpref contient bien money 
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        else                            //si la clé n'existe pas on l'initialise et on set l'argent a 0
        {
            money = 0;
            PlayerPrefs.SetInt("Money", 0);
        }

        textmoney.text = money + " Gold";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainMoney(int gaingold)    //on utilise pas Update car cette valeur n'a pas besoin d'être modifié a chaque frame
    {
        money += gaingold;
        PlayerPrefs.SetInt("Money", money);
        textmoney.text = money + " Gold";
    }
}
