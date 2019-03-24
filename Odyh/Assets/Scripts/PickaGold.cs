using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaGold : MonoBehaviour
{
    //La valeur a ajouter
    public int goldvalue;
    //La référence au manager
    public MoneyManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<MoneyManager>();    //on cherche le manager 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)    //on utilise ici la fonction associée au Collider sur le coin
    {
        if (other.gameObject.name == "Player")
        {
            manager.GainMoney(goldvalue);
            Destroy(gameObject);
        }
    }
}
