using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public enum Quality
{
    Commun, 
    Rare, 
    Epique, 
    Legendaire
}

//class Mère item
public class Item : ScriptableObject, IBougeable, IDescribable // = scripter un objet sans lui attacher de script dans unity
{
    //sprite attaché
    [SerializeField]
    private Sprite sprite;

    public string itemDescription;
    //getter
    public Sprite TheSprite
    {
        get { return sprite; }
        set { sprite = value; }
    }

    [FormerlySerializedAs("name")] [SerializeField]
    private string title;

    public string Title
    {
        get { return title;  }
    }
    
    //nombre de fois que l'item est stackable
    [SerializeField] 
    private int stacksize;

    //getter
    public int Stacksize
    {
        get { return stacksize; }
    }


    //ref au script du slot
    private Slot slot;

    public Slot Slot
    {
        get => slot;
        set => slot = value;
    }

    [SerializeField]
    private int price;

    public int Price
    {
        get => price;
    }

    [SerializeField]
    private Quality quality;

    //fontion de supreesion d'un item
    public void Delete_the_Item()
    {
        if (Slot != null)
        {
            Slot.Delete_Item(this);
        }
    }


    public virtual string GetDescription()
    {
        string color = string.Empty;

        switch (quality)
        {
            case Quality.Commun:
                color = "#d6d6d6";
                break;
            case Quality.Rare:
                color = "#00ff00ff";
                break;
            case Quality.Epique:
                color = "#0000ffff";
                break;
            case Quality.Legendaire:
                color = "#800080ff";
                break;
        }

        return string.Format("<color={0}>{1}</color>", color, title);
    }
}
