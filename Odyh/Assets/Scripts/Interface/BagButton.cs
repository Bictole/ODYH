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

    //ref au sprite -> a voir si on a des sprites pour les deux
    [SerializeField]
    private Sprite full, empty;

    //implémentation de l'interface IPointerClickHandler
    public void OnPointerClick(PointerEventData eventData)
    {
        if (bagitem != null)
        {
            bagitem.BagScr.OpenOrClose();
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
}
