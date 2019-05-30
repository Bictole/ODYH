﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipementType
{
    Casque,
    Torse, 
    Bottes, 
    Gants, 
    Epee, 
    Arc, 
    Baton
}

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Equipement : Item
{
    [SerializeField]
    private EquipementType _equipementType;
    
    public EquipementType EquipementType
    {
        get => _equipementType;
    }
    
    [SerializeField]
    private int healthbonus;

    [SerializeField]
    private int manabonus;

    [SerializeField]
    private int attackbonus;

    [SerializeField]
    private int defensebonus;

    
    public override string GetDescription()
    {
        string stats = string.Empty;

        if (healthbonus > 0)
        {
            stats += string.Format("\n +{0} Health", healthbonus);
        }
        if (manabonus > 0)
        {
            stats += string.Format("\n +{0} Mana", manabonus);
        }
        if (attackbonus > 0)
        {
            stats += string.Format("\n +{0} Attack", attackbonus);
        }
        if (defensebonus > 0)
        {
            stats += string.Format("\n +{0} Armor", defensebonus);
        }
        
        
        return base.GetDescription() + stats;
    }

    public void Equip()
    {
        EquipementUI.EquipementUi.Equip(this);
    }
}
