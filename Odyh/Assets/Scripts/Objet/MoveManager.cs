using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
    //getter du Bougeable
    public Bougeable Itembougeable { get; set; }

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
    }
}
