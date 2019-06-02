using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StockageChest : MonoBehaviour, Interactionnable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite ouvert, ferme;

    private bool Open;

    [SerializeField] private CanvasGroup canvasgroup;

    private List<Item> itemlist;

    private void Awake()
    {
       itemlist = new List<Item>();
    }

    public List<Item> Itemlist
    {
        get => itemlist;
        set => itemlist = value;
    }

    [SerializeField]
    private Bag bagscr;

    public Bag Bagscr
    {
        get => bagscr;
        set => bagscr = value;
    }

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
        Debug.Log("bou");
        itemlist = bagscr.GetItems();
    }
}
