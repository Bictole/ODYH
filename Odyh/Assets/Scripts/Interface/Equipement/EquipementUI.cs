using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private EquipementButton head, chest, feet, hand, onehand, bow, staff;
    
    public EquipementButton EquipementButton { get; set; }

    private static EquipementUI _equipementUi;

    public static EquipementUI EquipementUi
    {
        get
        {
            if (_equipementUi == null)
            {
                _equipementUi = FindObjectOfType<EquipementUI>();
            }

            return _equipementUi;
        }
    }

    public void OpenClose()
    {
        if (_canvasGroup.alpha == 1)
        {
            CloseButton();
        }
        else
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
        }
    }
    
    public void CloseButton()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }


    public void Equip(Equipement equipement)
    {
        switch (equipement.EquipementType)
        {
            case EquipementType.Casque:
                head.EquipEquipement(equipement);
                break;
            case EquipementType.Torse:
                chest.EquipEquipement(equipement);
                break;
            case EquipementType.Bottes:
                feet.EquipEquipement(equipement);
                break;
            case EquipementType.Gants:
                hand.EquipEquipement(equipement);
                break;
            case EquipementType.Epee:
                onehand.EquipEquipement(equipement);
                break;
            case EquipementType.Arc:
                bow.EquipEquipement(equipement);
                break;
            case EquipementType.Baton:
                staff.EquipEquipement(equipement);
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OpenClose();
        }
    }
}
