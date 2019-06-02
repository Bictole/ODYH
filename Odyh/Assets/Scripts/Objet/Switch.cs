using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public bool active;
    public Boolvalue storedvalue;

    public Sprite activeSprite;

    private SpriteRenderer mysprite;

    public Door thisdoor;
    // Start is called before the first frame update
    void Start()
    {
        
        mysprite = GetComponent<SpriteRenderer>();
        active = storedvalue.Runtimevalue;
        if (active)
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        active = true;
        thisdoor.Open();
        mysprite.sprite = activeSprite;
        storedvalue.initialvalue = active;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}
