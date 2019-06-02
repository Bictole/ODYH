using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedGame : MonoBehaviour
{
    [SerializeField]
    private Text dateTime;

    [SerializeField]
    private Slider health;

    [SerializeField]
    private Slider mana;

    [SerializeField]
    private Slider xp;

    [SerializeField]
    private Text xpText;

    [SerializeField]
    private GameObject visuals;

    [SerializeField]
    private int index;

    public int MyIndex
    {
        get
        {
            return index;
        }
    }

    private void Awake()
    {
        visuals.SetActive(false);
    }

    public void ShowInfo(SaveData saveData)
    {

        visuals.SetActive(true);

        dateTime.text = "Date: " + saveData.MyDateTime.ToString("dd/MM/yyy") + " - Time: " + saveData.MyDateTime.ToString("H:mm");
        health.value = saveData.MyPlayerData.MyHealth;
        health.maxValue = saveData.MyPlayerData.MyMaxHealth;

        mana.value = saveData.MyPlayerData.MyMana / saveData.MyPlayerData.MyMaxMana;
        mana.maxValue = saveData.MyPlayerData.MyMaxMana;
        
        xp.value = saveData.MyPlayerData.MyXp;
        xpText.text = "Level " + saveData.MyPlayerData.Mylevel;
        
        
    }

    public void HideVisuals()
    {
        visuals.SetActive(false);
    }

}
