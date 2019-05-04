using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
    //getter du Bougeable
    public Bougeable Itembougeable{get; set; }

    private Image sprite;

    //getter du MoveManager
    private static MoveManager _moveManager;

    public static MoveManager TheMoveManager
    {
        get
        {
            if (_moveManager == null)
            {
                _moveManager = FindObjectOfType<MoveManager>();
            }

            return _moveManager;
        }
    }


    public void PickBougeable(Bougeable bougeable)
    {
        this.Itembougeable = bougeable;
        sprite.sprite = bougeable.TheSprite;
        sprite.color = Color.white;
    }


  /*  public Bougeable Put()
    {
        Bougeable tmp = Itembougeable;
        Itembougeable = null;
        sprite.color = new Color(0, 0, 0, 0);
        return tmp;
    }*/

    //Fonction lorsqu'on lache l'item
    public void Drop()
    {
        Itembougeable = null;
        sprite.color = new Color(0, 0, 0, 0);
    }

    //fontion de suppression d'un objet en le jetant hors de l'inventaire
    private void Delete()
    {
        //on check si on clique avec un item dans la main hors de l'inventaire
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && Itembougeable != null)
        {
            if (Itembougeable is Item && Inventory.InventoryScr.TheSlot != null)
            {
                (Itembougeable as Item).Slot.Clear_slot();
            }
            
            Drop();
            Inventory.InventoryScr.TheSlot = null;
        }
    }
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //on suit la position de la souris
        sprite.transform.position = Input.mousePosition;
        
        Delete();
    }
}
