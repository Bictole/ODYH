using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickitems : MonoBehaviour
{
    //L'objet ramassable
    [SerializeField]
    private Item item;
    
    //La référence à l'inventaire
    private Inventory inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();    //on cherche l'inventaire
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D other)    //on utilise ici la fonction associée au Collider sur l'item
    {
        if (other.gameObject.name == "Player")
        {
            inventory.AddInventoryItem(item);
            Destroy(gameObject);
        }
    }
}
