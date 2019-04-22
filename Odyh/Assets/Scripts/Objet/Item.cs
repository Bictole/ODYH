using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//class Mère item
public class Item : ScriptableObject, Bougeable  // = scripter un objet sans lui attacher de script dans unity
{
    //sprite attaché
    [SerializeField]
    private Sprite sprite;

    //getter
    public Sprite TheSprite
    {
        get { return sprite; }
        set { sprite = value; }
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

    //fontion de supreesion d'un item
    public void Delete_the_Item()
    {
        if (Slot != null)
        {
            Slot.Delete_Item(this);
        }
    }

   
}
