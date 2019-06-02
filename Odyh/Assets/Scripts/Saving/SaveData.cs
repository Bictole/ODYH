using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveData
{
    public PlayerData MyPlayerData { get; set; }
    
    public List<ChestData> MyChestData { get; set; }

    public InventoryData MyInventoryData { get; set; }
    
    public List<EquipmentData> MyEquipmentData { get; set; }

    public List<QuestData> MyQuestData { get; set; }
    
    public List<QuestGiverData> MyQuestGiverData { get; set; }
    
    public MoneyData MyMoneyData { get; set; }
    
    public CameraData MyCameraData { get; set; }

    public DateTime MyDateTime { get; set; }

    public string MyScene { get; set; }
    public SaveData()
    {
        MyChestData = new List<ChestData>();
        MyInventoryData = new InventoryData();
        MyEquipmentData = new List<EquipmentData>();
        MyQuestData = new List<QuestData>();
        MyQuestGiverData = new List<QuestGiverData>();
        MyDateTime = DateTime.Now;
    }
}

[Serializable]
public class PlayerData
{
    public int Mylevel { get; set; }
    public int MyXp { get; set; }
    public int MyHealth { get; set; }
    public int MyMaxHealth { get; set; }
    public float MyMana { get; set; }
    public int MyMaxMana { get; set; }
    public float MyX { get; set; }
    public float MyY { get; set; }
    public float MyZ { get; set; }
    public PlayerData(int level, int xp,  int health, int maxHealth, float mana, int maxMana, Vector3 position)
    {
        Mylevel = level;
        MyXp = xp;
        MyHealth = health;
        MyMaxHealth = maxHealth;
        MyMana = mana;
        MyMaxMana = maxMana;
        MyX = position.x;
        MyY = position.y;
        MyZ = position.z;
    }
}

[Serializable]
public class ItemData
{
    public string MyTitel { get; set; }
    
    public int MyStackCount { get; set; }
    
    public int MySlotIndex { get; set; }
    
    public int MyBagIndex { get; set; }

    public ItemData(string titel, int stackCount = 0, int slotIndex = 0, int bagIndex = 0)
    {
        MyBagIndex = bagIndex;
        MyTitel = titel;
        MyStackCount = stackCount;
        MySlotIndex = slotIndex;
    }
}
[Serializable]
public class ChestData
{
    public string MyName { get; set; }
    public List<ItemData> MyItems { get; set; }

    public ChestData(string name)
    {
        MyName = name;
        MyItems = new List<ItemData>();
    }
}
[Serializable]
public class InventoryData
{
    public List<BagData> Mybags { get; set; }
    
    public List<ItemData> MyItems { get; set; }

    public InventoryData()
    {
        Mybags = new List<BagData>();
        MyItems = new List<ItemData>();
    }
}
[Serializable]
public class BagData
{
    public int MySlotCount { get; set; }
    public int MyBagIndex { get; set; }

    public BagData(int count, int index)
    {
        MySlotCount = count;
        MyBagIndex = index;
    }
}

[Serializable]
public class EquipmentData
{
    public string MyTitle { get; set; }
    
    public string MyType { get; set; }

    public EquipmentData(string title, string type)
    {
        MyTitle = title;
        MyType = type;
    }
}

[Serializable]
public class QuestData
{
    public string MyTitle { get; set; }
    public string MyDescription { get; set; }
    public Collect[] MyCollectObjectives { get; set; }
    public Kill[] MyKillObjectives { get; set; }
    public int MyQuestGiverID { get; set; }

    public QuestData(string title, string description, Collect[] collectObjectives, Kill[] killObjectives, int questGiveID)
    {
        MyTitle = title;
        MyDescription = description;
        MyCollectObjectives = collectObjectives;
        MyKillObjectives = killObjectives;
        MyQuestGiverID = questGiveID;
    }
}

[Serializable]
public class QuestGiverData
{
    public List<string> MyCompletedQuests { get; set; }

    public int MyQuestGiverID { get; set; }

    public QuestGiverData(int questGiverID, List<string> completedQuests)
    {
        this.MyQuestGiverID = questGiverID;
        MyCompletedQuests = completedQuests;
    }
}

[Serializable]
public class MoneyData
{
    public int MyMoney;

    public MoneyData(int money)
    {
        MyMoney = money;
    }
}

[Serializable]
public class CameraData
{
    public float MinX;
    public float MinY;
    public float MaxX;
    public float MaxY;

    public CameraData(float minX, float minY, float maxX, float maxY)
    {
        MinX = minX;
        MinY = minY;
        MaxX = maxX;
        MaxY = maxY;
    }
}
