using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasgroup;

    [SerializeField]
    private VendorButton[] _vendorButton;
    
    [SerializeField]
    private Text pagenumber;

    private List<List<VendorItem>> pages = new List<List<VendorItem>>();

    private int pageIndex;

    private Vendor vendor;

    public void Pages(VendorItem[] items)
    {
        pages.Clear();
        
        List<VendorItem> page = new List<VendorItem>();

        for (int i = 0; i < items.Length; i++)
        {
            page.Add(items[i]);

            if (page.Count == 10 || i == items.Length -1)
            {
                pages.Add(page);
                page = new List<VendorItem>();
            }
        }
        
        AddItems();
    }

    public void AddItems()
    {
        pagenumber.text = pageIndex + 1 + "/" + pages.Count;
        
        if (pages.Count > 0)
        {
            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    _vendorButton[i].AddItem(pages[pageIndex][i]);
                }
            }
        }
    }
    

    public void NextPage()
    {
        if (pageIndex < pages.Count -1)
        {
            ClearPage();
            pageIndex += 1;
            AddItems();
        }
    }

    public void PreviousPage()
    {
        if (pageIndex > 0)
        {
            ClearPage();
            pageIndex -= 1;
            AddItems();
        }
    }

    public void ClearPage()
    {
        foreach (var button in _vendorButton)
        {
            button.gameObject.SetActive(false);
        }
    }
    
    
    public void OpenUI(Vendor vendor)
    {
        this.vendor = vendor;
        canvasgroup.alpha = 1;
        canvasgroup.blocksRaycasts = true;
        Inventory.InventoryScr.OpenOrClose();
    }
    
    public void CloseUI()
    {
        vendor.IsOpen = false;
        canvasgroup.alpha = 0;
        canvasgroup.blocksRaycasts = false;
        vendor = null;
        Inventory.InventoryScr.OpenOrClose();
    }
    
}
