using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    //Référence au texte 
    public TextMeshProUGUI textmoney;

    //argent actuel du joueur
    public int money;
    
    // Start is called before the first frame update
    void Start()
    {
        money = 0;

        textmoney.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        textmoney.text = money.ToString();
    }

    public void GainMoney(int gaingold)    //on utilise pas Update car cette valeur n'a pas besoin d'être modifié a chaque frame
    {
        money += gaingold;
        PlayerPrefs.SetInt("Money", money);
        textmoney.text = money.ToString();
    }
}
