using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //ref au script attaché a l'inventaire
    private static Inventory inventory;

    //getter
    public static Inventory InventoryScr
    {
        get
        {
            if (inventory == null)
            {
                inventory = FindObjectOfType<Inventory>();
            }

            return inventory;
        }
    }

    //liste des boutons d'emplacement des sacs
    [SerializeField]
    private BagButton[] bagbuttons;
    

    //liste d'item
    [SerializeField]
    private Item[] items;
    
    //liste de sacs pour vérifier ensuite qu'on ne dépasse pas le nombre max
    private List<BagItem> bags = new List<BagItem>();
    
    //bool pour savoir si l'on peux ajouter un sac selon le nombre max, ici 5
    public bool AddBag
    {
        get { return bags.Count < 5; }
    }

   /* private void Awake()
    {
        BagItem bag = (BagItem)Instantiate(items[0]);
        bag.Init(20);
        bag.Use();
        BagItem b = (BagItem)Instantiate(items[0]);
        bag.Init(20);
        bag.Use();    
    }*/


    //init le premier bag null dans la liste
    public void InitBag(BagItem bag)
    {
        foreach (var buttons in bagbuttons)
        {
            if (buttons.Bag == null)
            {
                buttons.Bag = bag;
                bags.Add(bag);
                break;
            }
        }
    }
    
    
    //fonction d'ouverture/fermeture des sacs en groupe
    public void OpenOrClose()
    {
        bool bag_is_close = bags.Find(x => !x.BagScr.Open); //on check si un ou plusieurs sac(s) est/sont fermé(s)

        foreach (var sac in bags) //si un sac est fermé, on ouvre les sacs fermés, sinon on ferme tout
        {
            if (sac.BagScr.Open != bag_is_close)
            {
                sac.BagScr.OpenOrClose();
            }
        }
    }

    //on check le premier sac avec un emplacement pour mettre l'item
    public void AddInventoryItem(Item item)
    {
        foreach (var sac in bags)
        {
            if (sac.BagScr.AddBagItem(item))
            {
                return;
            }
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // POur les test
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            BagItem bag = (BagItem)Instantiate(items[0]);
            bag.Init(20);
            bag.Use();
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            BagItem bag = (BagItem)Instantiate(items[0]);
            bag.Init(20);
            AddInventoryItem(bag);
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            HealthPotionItem pot = (HealthPotionItem)Instantiate(items[1]);
            
            AddInventoryItem(pot);
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            Inventory.InventoryScr.OpenOrClose();
        }
    }
}
