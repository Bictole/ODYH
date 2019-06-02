using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour, IPointerClickHandler
{
    //ref a l'item
    private BagItem bagitem;

    public BagItem Bag
    {
        get
        {
            return bagitem;
        }
        set
        {
            if (value != null)                    //on set le bon sprite si on a un sac ou non
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }

            bagitem = value;
        }
    }

    [SerializeField]
    private int index;

    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    //ref au sprite -> a voir si on a des sprites pour les deux
    [SerializeField]
    private Sprite full, empty;

    //implémentation de l'interface IPointerClickHandler
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.InventoryScr.TheSlot != null && MoveManager.TheMoveManager.Itembougeable != null && MoveManager.TheMoveManager.Itembougeable is BagItem)
            {
                if (Bag != null)
                {
                    Inventory.InventoryScr.SwapBags(Bag, MoveManager.TheMoveManager.Itembougeable as BagItem);
                }
                else
                {
                    BagItem b = (BagItem)MoveManager.TheMoveManager.Itembougeable;
                    b.BagButton = this;
                    b.Use();
                    Bag = b;
                    MoveManager.TheMoveManager.Drop();
                    Inventory.InventoryScr.TheSlot = null;
                }
            }
            //si on clique en appuyant sur caps lock, on prend le sac
            else if (Input.GetKey(KeyCode.CapsLock))
            {
                MoveManager.TheMoveManager.PickBougeable(Bag);
            }          
            //sinon ou l'ouvre/ferme
            else if (bagitem != null)
            {
                bagitem.BagScr.OpenOrClose();
            }
        }
        
    }

    //va delete le bag et son bouton
    public void Delete_bag()
    {
        Inventory.InventoryScr.Delete_bag_inventory(Bag);
        Bag.BagButton = null;

        foreach (Item item in Bag.BagScr.GetItems())
        {
            Inventory.InventoryScr.AddInventoryItem(item);
        }
        Bag = null;
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
