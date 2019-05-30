﻿using     System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "Items/Bag", order = 1)]
public class BagItem : Item, Utilisable
{
    //nb de slots de notre bag
    private int slotnumber;

    //getter
    public int Slotnumber
    {
        get { return slotnumber; }
    }
    
    [SerializeField] 
    private GameObject bagprefab;
    
    //sert a set le script du bag associé
    public Bag BagScr { get; set; }


    public void Init(int nbslots)
    {
        this.slotnumber = nbslots;
    }
    
    public Sprite Sprite { get; }
    
    public BagButton BagButton { get; set; }

    //fonction d'utilisation
    public void Use()
    {
        if (Inventory.InventoryScr.AddBag)
        {
            Delete_the_Item();
            BagScr = Instantiate(bagprefab, Inventory.InventoryScr.transform).GetComponent<Bag>();
            BagScr.Initslots(slotnumber);

            if (BagButton == null)
            {
                Inventory.InventoryScr.InitBag(this);
            }
            else
            {
                Inventory.InventoryScr.InitBag2(this, BagButton);
            }
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n<color=#d6d6d6>A Bag is a Bag</color>");
    }
}
