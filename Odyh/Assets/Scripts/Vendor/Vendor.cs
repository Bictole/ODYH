using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour, Interactionnable
{
    [SerializeField]
    private VendorItem[] vendorItemlist;
    
    [SerializeField]
    private VendorUI vendorUi; 
    
    public bool IsOpen { get; set; }
    
    public void Interagir()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            vendorUi.Pages(vendorItemlist);
            vendorUi.OpenUI(this);
        }    
    }

    public void StopInteraction()
    {
        if (IsOpen)
        {
            IsOpen = false;
           vendorUi.CloseUI();
        }   
    }
}
