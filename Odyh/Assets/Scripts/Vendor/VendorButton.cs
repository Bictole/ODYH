using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VendorButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Text name;

    [SerializeField]
    private Text price;

    [SerializeField]
    private Text quantity;

    private VendorItem vendorItem;

    private MoneyManager _moneyManager;

    void Awake()
    {
        _moneyManager = FindObjectOfType<MoneyManager>();
    }

    public void AddItem(VendorItem vendoritem)
    {
        this.vendorItem = vendoritem;
        
        if (vendoritem.Quantity > 0 || (vendoritem.Quantity == 0 && vendoritem.Unlimited))
        {
            image.sprite = vendoritem.Item.TheSprite;
            name.text = vendoritem.Item.name;
            price.text = "Price : " + vendoritem.Item.Price.ToString();

            if (!vendoritem.Unlimited)
            {
                quantity.text = vendoritem.Quantity.ToString();
            }
            else
            {
                quantity.text = string.Empty;
            }

            if (vendoritem.Item.Price == 0)
            {
                price.text = "";
            }
        
            gameObject.SetActive(true);
        }
        
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((_moneyManager.money >= vendorItem.Item.Price) && Inventory.InventoryScr.AddInventoryItem(Instantiate(vendorItem.Item)))
        {
            SellItem();
        }
    }

    private void SellItem()
    {
        _moneyManager.GainMoney(-vendorItem.Item.Price);

        if (!vendorItem.Unlimited)
        {
            vendorItem.Quantity -= 1;
            quantity.text = vendorItem.Quantity.ToString();

            if (vendorItem.Quantity == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        UI.UserInterface.ShowInfos(transform.position, vendorItem.Item);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        UI.UserInterface.HideInfos();
    }
}
