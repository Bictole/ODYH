using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    //on recupere la prefab du slot
    [SerializeField] 
    private GameObject slotprefab;


    //ref au component present canvasgroup
    private CanvasGroup canvasgr;

    
    //list de script Slot qui aide a ajouter un item
    public List<Slot> slotscrList  = new List<Slot>();
    
    
    //bool pour savoir si notre sac est ouvert ou non
    public bool Open
    {
        get
        {
            return canvasgr.alpha > 0;
        }
    }
    
    
    //on cree les slots correspondant au bag et on add leur script a la liste
    public void Initslots(int nbslots)
    {
        for (int i = 0; i < nbslots; i++)
        {
            Slot slotscr = Instantiate(slotprefab, transform).GetComponent<Slot>();
            slotscrList.Add(slotscr);
        }
    }

    //fonction d'ouverture/fermeture d'interface
    public void OpenOrClose()
    {
        canvasgr.alpha = canvasgr.alpha > 0 ? 0 : 1;        //set le canvasgr a 0 ou 1 selon l'ouverture

        canvasgr.blocksRaycasts = canvasgr.blocksRaycasts == true ? false : true;
    }

    
    private void Awake() //on doit recuperer le component canvasgroup des le debut 
    {
        canvasgr = GetComponent<CanvasGroup>();
    }

    
    //fonction d'ajouts d'un item, on regarde le premier slot vide.
    public bool AddBagItem(Item item)
    {
        foreach (var scr in slotscrList)
        {
            if (scr.TheItem != null)
            {
                
                if (scr.TheItem.TheSprite == item.TheSprite && !scr.Full)
                {
                    scr.AddItem(item);
                    return true;
                }
            }
            
            if (scr.Empty)
            {
                scr.AddItem(item);
                return true;
            }
        }

        return false;
    }
    
    
}
