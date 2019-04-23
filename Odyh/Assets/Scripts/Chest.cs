using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chest : MonoBehaviour, Interactionnable, IPointerClickHandler
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Sprite ouvert, ferme;

    private bool Open;

    public void Interagir()
    {
        if (Open)
        {
            StopInteraction();
        }
        else
        {
            Open = true;
            _spriteRenderer.sprite = ouvert;
        }
    }

    public void StopInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Interagir();
        }
    }
}
