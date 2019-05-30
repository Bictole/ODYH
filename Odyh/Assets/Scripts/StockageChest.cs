using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StockageChest : MonoBehaviour, Interactionnable
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Sprite ouvert, ferme;

    private bool Open;

    [SerializeField]
    private CanvasGroup canvasgroup;

    private List<Item> itemlist;

    [SerializeField]
    private Bag bagscr;

    public void Interagir()
    {
        if (Open)
        {
            StopInteraction();
        }
        else
        {
            AddItems();
            Open = true;
            _spriteRenderer.sprite = ouvert;
            canvasgroup.alpha = 1;
            canvasgroup.blocksRaycasts = true;
        }
    }

    public void StopInteraction()
    {
        if (Open)
        {
            SaveItems();
            bagscr.Clear();
            Open = false;
            _spriteRenderer.sprite = ferme;
            canvasgroup.alpha = 0;
            canvasgroup.blocksRaycasts = false;
        }
        
    }


    public void AddItems()
    {
        if (itemlist != null)
        {
            foreach (Item item in itemlist)
            {
                item.Slot.AddItem(item);
            }
        }
    }

    public void SaveItems()
    {
        itemlist = bagscr.GetItems();
    }
}
