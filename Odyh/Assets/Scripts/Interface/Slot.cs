using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, Cliquable
{
    //ref du sprite
    [SerializeField]
    private Image itemsprite;

    //stack servant a empiler/dépiler plusieurs objets identiques 
    private Stack<Item> itemStack = new Stack<Item>();

    //text fils de slot du nombre d'objets empilés
    [SerializeField]
    private Text stacktext;

    //bool pour savoir si le stack d'item est vide
    public bool Empty
    {
        get { return itemStack.Count == 0; }
    }

    //getter de l'item au dessu de la pile
    public Item TheItem
    {
        get
        {
            if (!Empty)
            {
                return itemStack.Peek();
                
            }

            return null;
        }
    }
    
    
    //ajout et suppression d'un item 
    public bool AddItem(Item item)
    {
        itemStack.Push(item);
        itemsprite.sprite = item.Sprite;
        itemsprite.color = Color.white;
        item.Slot = this;
        UI.UserInterface.StackSlotManage(this);
        return true;
    }

    public void Delete_Item(Item item)
    {
        if (!Empty)
        {
            itemStack.Pop();
            UI.UserInterface.StackSlotManage(this);
        }
    }

    


    //interface IPointerClickHandler avec le clic droit 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Using_Item();
        }
    }

    //on check si l'item est bien Utilisable avant de le Use()
    public void Using_Item()
    {
        if (TheItem is Utilisable)
        {
            (TheItem as Utilisable).Use();
        }
    }
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //implémentation de l'interface Cliquable
    public Image Image
    {
        get { return itemsprite; }
        set { itemsprite = value; }
    }

    public int Itemscount
    {
        get { return itemStack.Count; }
    }

    public Text StackText
    {
        get { return stacktext; }
    }
}
