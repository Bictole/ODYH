using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//class Mère item
public class Item : ScriptableObject  // = scripter un objet sans lui attacher de script dans unity
{
    //sprite attaché
    [SerializeField]
    private Sprite sprite;

    //getter
    public Sprite Sprite
    {
        get { return sprite; }
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

    protected Slot Slot
    {
        get => slot;
        set => slot = value;
    }
}
