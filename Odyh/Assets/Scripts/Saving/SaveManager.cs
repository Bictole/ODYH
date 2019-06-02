using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private Item[] items;
    
    private StockageChest[] chests;
    
    private EquipementButton[] equipment;
    
    [SerializeField]
    private SavedGame[] saveSlots;
    
    [SerializeField]
    private CanvasGroup dialogue;
    
    [SerializeField]
    private Text dialogueText;
    
    [SerializeField]
    private SavedGame current;

    [SerializeField]
    private string[] actions;
    
    private string action;
    
    [SerializeField]
    private int saveSlotsindex;
    [SerializeField]
    private int actionIndex;
    
    private Player Player;

    private PlayerStats _playerStats;

    public bool InInterface =false;
    // Start is called before the first frame update
    void Awake()
    {
        chests = FindObjectsOfType<StockageChest>();
        Player = FindObjectOfType<Player>();
        equipment = FindObjectsOfType<EquipementButton>();
        
        foreach (SavedGame saved in saveSlots)
        {
            //We need to show the saved files here
            ShowSavedFiles(saved);
        }
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Load"))
        {
            Load(saveSlots[PlayerPrefs.GetInt("Load")]);
            PlayerPrefs.DeleteKey("Load");
        }
    }

    public void ShowDialogue(GameObject clickButton)
    {
        action = clickButton.name;
        switch (action)
        {
            case "Load":
                dialogueText.text = "Charger ?";
                actionIndex = 0;
                break;
            case "Save":
                dialogueText.text = "Sauvegarder ?";
                actionIndex = 1;
                break;
            case "Delete":
                dialogueText.text = "Supprimer ?";
                actionIndex = 2;
                break;
        }

        current = clickButton.GetComponentInParent<SavedGame>();
        saveSlotsindex = current.MyIndex;
        dialogue.alpha = 1;
        dialogue.blocksRaycasts = true;
    }

    public void ExecuteAction()
    {
        switch (actions[actionIndex])
        {
            case "Load":
                LoadScene(current);
                break;
            case "Save":
                Save(current);
                break;
            case "Delete":
                Delete(current);
                break;
        }

        CloseDialogue();

    }

    private void LoadScene(SavedGame savedGame)
    {
        savedGame = saveSlots[saveSlotsindex];
        Debug.Log(savedGame);
        if (File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            PlayerPrefs.SetInt("Load", savedGame.MyIndex);
            SceneManager.LoadScene(data.MyScene);
        }
    }

    public void CloseDialogue()
    {
        dialogue.blocksRaycasts = false;
        dialogue.alpha = 0;
    }

    private void Delete(SavedGame savedGame)
    {
        File.Delete(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat");
        savedGame.HideVisuals();
    }

    public void ShowSavedFiles(SavedGame savedGame)
    {
   
        if (File.Exists(Application.persistentDataPath + "/"+savedGame.gameObject.name+".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            savedGame.ShowInfo(data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InInterface)
        {
            foreach (SavedGame saved in saveSlots)
            {
                //We need to show the saved files here
                ShowSavedFiles(saved);
            }
        }
    }

    public void Save(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat", FileMode.Create);
            
            SaveData data = new SaveData();
            
            data.MyScene = SceneManager.GetActiveScene().name;

            SaveEquipment(data);
            SaveBags(data);
            SaveInventory(data);
            
            SavePlayer(data);
            SaveChests(data);
            
            SaveQuest(data);
            SaveQuestGivers(data);

            SaveMoneyData(data);

            SaveCameraData(data);
            
            bf.Serialize(file,data);
            
            file.Close();
            
            ShowSavedFiles(savedGame);
            
            Debug.Log("Sauvegarder");
        }
        catch (Exception e)
        {
            //This is for handling errors*
            Delete(savedGame);
            PlayerPrefs.DeleteKey("Load");
            throw e;
        }
    }

    private void SavePlayer(SaveData data)
    {
        _playerStats = Player.GetComponent<PlayerStats>();
        data.MyPlayerData = new PlayerData(_playerStats.playerLevel, _playerStats.playerexp,
            _playerStats.PlayerHealth.playerHealth,
            _playerStats.PlayerHealth.playerMaxHealth, _playerStats.PlayerMana.playerMana,_playerStats.PlayerMana.playerMaxMana,Player.transform.position);
    }

    private void SaveChests(SaveData data)
    {
        for (int i = 0; i < chests.Length; i++)
        {
            data.MyChestData.Add(new ChestData(chests[i].name));

            foreach (Item item in chests[i].Itemlist)
            {
                if (chests[i].Itemlist.Count > 0)
                {
                    data.MyChestData[i].MyItems.Add(new ItemData(item.Title, item.Slot.Itemscount, item.Slot.Index));
                }
            }
        }
    }

    private void SaveBags(SaveData data)
    {
        for (int i = 1; i < Inventory.InventoryScr.bags.Count; i++)
        {
            data.MyInventoryData.Mybags.Add(new BagData(Inventory.InventoryScr.bags[i].Slotnumber,Inventory.InventoryScr.bags[i].BagButton.Index));
        }
    }

    private void SaveEquipment(SaveData data)
    {
        foreach (EquipementButton equipementButton in equipment)
        {
            if (equipementButton.Myequipement != null)
            {
                data.MyEquipmentData.Add(new EquipmentData(equipementButton.Myequipement.Title,equipementButton.name));
            }
        }
    }

    private void SaveInventory(SaveData data)
    {
        List<Slot> slots = Inventory.InventoryScr.GetAllItems();
        foreach (Slot slot in slots)
        {
            data.MyInventoryData.MyItems.Add(new ItemData(slot.TheItem.Title,slot.itemStack.Count,slot.Index, slot.SlotBagScr.MyBagIndex));
        }
    }

    private void SaveQuest(SaveData data)
    {
        foreach (Quest quest in Questlog.Log.Quests)
        {
            data.MyQuestData.Add(new QuestData(quest.Title, quest.Description, quest.Collectarray,quest.Killarray,quest.QuestPnj.QuestGiverId));
        }
    }
    
    private void SaveQuestGivers(SaveData data)
    {
        QuestPnj[] questGivers = FindObjectsOfType<QuestPnj>();

        foreach (QuestPnj questGiver in questGivers)
        {
            data.MyQuestGiverData.Add(new QuestGiverData(questGiver.QuestGiverId, questGiver.CompltedQuests));
        }
    }

    private void SaveMoneyData(SaveData data)
    {
        MoneyManager moneyManager = FindObjectOfType<MoneyManager>();
        data.MyMoneyData = new MoneyData(moneyManager.money);
    }
    
    private void SaveCameraData(SaveData data)
    {
        CameraController camera = FindObjectOfType<CameraController>();
        data.MyCameraData = new CameraData(camera.minPosition.x, camera.minPosition.y, camera.maxPosition.x,
            camera.maxPosition.y);
    }
    
    private void Load(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);

            SaveData data = (SaveData) bf.Deserialize(file);
            
            file.Close();
            
            LoadEquipment(data);
            LoadBags(data);
            LoadInventory(data);
            
            LoadPlayer(data);
            LoadChests(data);
            
            LoadQuests(data);
            LoadQuestGiver(data);

            LoadMoney(data);

            LoadCamera(data);
            Debug.Log("Charger");
        }
        catch (Exception e)
        {
            //This is for handling errors
            Delete(savedGame);
            PlayerPrefs.DeleteKey("Load");
            SceneManager.LoadScene(0);
            throw e;
        }
    }

    private void LoadPlayer(SaveData data)
    {
        PlayerStats.PlayerStatsScr.playerLevel = data.MyPlayerData.Mylevel;
        UI.UserInterface.UpdateLevel();
        PlayerStats.PlayerStatsScr.playerexp = data.MyPlayerData.MyXp;
        PlayerStats.PlayerStatsScr.PlayerHealth.playerHealth = data.MyPlayerData.MyHealth;
        PlayerStats.PlayerStatsScr.PlayerHealth.playerMaxHealth = data.MyPlayerData.MyMaxHealth;
        PlayerStats.PlayerStatsScr.PlayerMana.playerMana = data.MyPlayerData.MyMana;
        PlayerStats.PlayerStatsScr.PlayerMana.playerMaxMana = data.MyPlayerData.MyMaxMana;
        Player.transform.position = new Vector3(data.MyPlayerData.MyX,data.MyPlayerData.MyY,data.MyPlayerData.MyZ);
    }


    private void LoadChests(SaveData data)
    {
        foreach (ChestData chest in data.MyChestData)
        {
            StockageChest c = Array.Find(chests, x => x.name == chest.MyName);
            foreach (ItemData itemData in chest.MyItems)
            {
                Item item = Array.Find(items, x => x.Title == itemData.MyTitel);
                item.Slot = c.Bagscr.slotscrList.Find(x => x.Index == itemData.MySlotIndex);
                c.Itemlist.Add(item);
            }
        }
    }


    private void LoadBags(SaveData data)
    {
        foreach (BagData bagdata in data.MyInventoryData.Mybags)
        {
            BagItem newBag = (BagItem) Instantiate(items[0]);
            
            newBag.Init(bagdata.MySlotCount);
            
            Inventory.InventoryScr.InitBag3(newBag, bagdata.MyBagIndex);
        }
    }

    private void LoadEquipment(SaveData data)
    {
        foreach (EquipmentData equipmentdata in data.MyEquipmentData)
        {
            EquipementButton eB = Array.Find(equipment, x => x.name == equipmentdata.MyType);
            eB.EquipEquipement(Array.Find(items, x => x.Title == equipmentdata.MyTitle) as Equipement);
            
            
        }
    }

    private void LoadInventory(SaveData data)
    {
        foreach (ItemData itemData in data.MyInventoryData.MyItems)
        {
            Item item = Array.Find(items, x => x.Title == itemData.MyTitel);
            for (int i = 0; i < itemData.MyStackCount; i++)
            {
                Inventory.InventoryScr.PlaceInSpecific(item,itemData.MySlotIndex,itemData.MyBagIndex);
            }
        }
    }

    private void LoadQuests(SaveData data)
    {
        QuestPnj[] questPnjs = FindObjectsOfType<QuestPnj>();
        foreach (QuestData questData in data.MyQuestData)
        {
            QuestPnj qg = Array.Find(questPnjs, x => x.QuestGiverId == questData.MyQuestGiverID);
            Quest[] tab = qg.Quests;
            Quest q = Array.Find(tab, x => x.Title == questData.MyTitle);
            q.QuestPnj = qg;
            
            Questlog.Log.Take_a_quest(q);
        }
        
    }
    
    private void LoadQuestGiver(SaveData data)
    {
        QuestPnj[] questGivers = FindObjectsOfType<QuestPnj>();

        foreach (QuestGiverData questGiverData in data.MyQuestGiverData)
        {
            QuestPnj questGiver = Array.Find(questGivers, x => x.QuestGiverId == questGiverData.MyQuestGiverID);
            questGiver.CompltedQuests = questGiverData.MyCompletedQuests;
            questGiver.QuestStatus();
        }
    }

    private void LoadMoney(SaveData data)
    {
        MoneyManager moneyManager = FindObjectOfType<MoneyManager>();
        moneyManager.money = data.MyMoneyData.MyMoney;
    }
    
    private void LoadCamera(SaveData data)
    {
        CameraController camera = FindObjectOfType<CameraController>();
        camera.minPositionSave = new Vector2(data.MyCameraData.MinX,data.MyCameraData.MinY);
        camera.maxPositionSave = new Vector2(data.MyCameraData.MaxX,data.MyCameraData.MaxY);
    }
}
