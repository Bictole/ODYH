using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipementButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private EquipementType _equipementType;

    private Equipement myequipement;

    [SerializeField]
    private Image icon;

    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (MoveManager.TheMoveManager.Itembougeable is Equipement)
            {
                Equipement e = (Equipement) MoveManager.TheMoveManager.Itembougeable;
                
                if (e.EquipementType == _equipementType)
                {
                    EquipEquipement(e);
                }
                
                UI.UserInterface.RefreshInfos(e);
            }
            else if (MoveManager.TheMoveManager.Itembougeable == null && myequipement!= null)
            {
                MoveManager.TheMoveManager.PickBougeable(myequipement);
                EquipementUI.EquipementUi.EquipementButton = this;
                icon.color = Color.grey;
            }
        }
    }

    public void EquipEquipement(Equipement equipement)
    {
        equipement.Delete_the_Item();

        if (myequipement != null)
        {
            if (myequipement != equipement)
            {
                equipement.Slot.AddItem(myequipement);
            }
            
            UI.UserInterface.RefreshInfos(myequipement);
        }
        else
        {
            UI.UserInterface.HideInfos();
        }
        
        icon.enabled = true;
        icon.sprite = equipement.TheSprite;
        icon.color = Color.white;
        this.myequipement = equipement;

        if (MoveManager.TheMoveManager.Itembougeable == (equipement as IBougeable))
        {
            MoveManager.TheMoveManager.Drop(); 
        }       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (myequipement != null)
        {
            UI.UserInterface.ShowInfos(transform.position, myequipement);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.UserInterface.HideInfos();
    }

    public void Desequip()
    {
        icon.color = Color.white;
        icon.enabled = false;
        myequipement = null;
    }

    public void UpdateStats(Equipement equipement)
    {
        if (equipement.HealthBonus > 0)
        {
            _playerStats.PlayerHealth.playerMaxHealth += equipement.HealthBonus;
            _playerStats.PlayerHealth.playerHealth += equipement.HealthBonus;
        }
        if (equipement.AttackBonus > 0)
        {
            _playerStats.Playerattack += equipement.AttackBonus;
        }
        if (equipement.DefenseBonus > 0)
        {
            _playerStats.Playerdefence += equipement.DefenseBonus;
        }
        if (equipement.ManaBonus > 0)
        {
            _playerStats.PlayerMana.playerMaxMana += equipement.HealthBonus;
            _playerStats.PlayerMana.playerMana += equipement.HealthBonus;
        }
    }
}
