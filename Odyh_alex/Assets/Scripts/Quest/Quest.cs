using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Analytics;

[System.Serializable]
public class Quest 
{
    [SerializeField]
    private string quest_title;
    
    public string Title
    {
        get { return quest_title; }
        set { quest_title = value; }
    }
        
    
    [SerializeField]
    private string quest_description;

    public string Description
    {
        get { return quest_description; }
        set { quest_description = value; }
    }
       
    
    //array stockant les objectis de collecte pour la quête
    [SerializeField]
    private Collect[] collectarray;

    public Collect[] Collectarray
    {
        get { return collectarray; }
    }

    //array stockant les objectis de kill pour la quête
    [SerializeField]
    private Kill[] killarray;

    public Kill[] Killarray
    {
        get => killarray;
    }

    public QuestPnj QuestPnj { get; set; }

    
    [SerializeField]
    private int questLevel;
    
    public int QuestLevel
    {
        get => questLevel;
        set => questLevel = value;
    }

    [SerializeField]
    private int experiencegiven;
    
   public int ExperienceGiven
    {
        get => experiencegiven;
    }

    public bool QuestIsFinished
    {
        get
        {
            foreach (var obj in collectarray)
            {
                if (!obj.Finished)
                {
                    return false;
                }
            }
            foreach (var obj in killarray)
            {
                if (!obj.Finished)
                {
                    return false;
                }
            }

            return true;
        }
    }
    
    public QuestScr Qscript { get; set; }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}


//Classe objectif
[System.Serializable]
public abstract class Objective
{
    //Nombre d'objets possédés
    private int objnumber;

    public int Objnumber
    {
        get { return objnumber; }
        set { objnumber = value; }
    }

//    [SerializeField]
//    private Item itemrequis;
//
//    public Item Itemrequis
//    {
//        get { return itemrequis; }
//    }
    
    //Nombre d'objets requis pour la quete
    [SerializeField]
    private int totalnumber;
    
    public int Totalnumber
    {
        get { return totalnumber; }
    }
    
    
    
    //type de l'objet requis 
    [SerializeField]    
    private string object_type;

    public string Object_type
    {
        get { return object_type; }
    }


    public bool Finished
    {
        get
        {
            return Objnumber >= Totalnumber;
        }
    }
}

[System.Serializable]
public class Collect : Objective
{
    public void UpdateItemCount(Item item)
    {
        if (Object_type.ToLower() == item.Title.ToLower())
        {
            Objnumber = Inventory.InventoryScr.ItemCount(item.Title);

            if (Objnumber <= Totalnumber)
            {
                MessageManager.TheMessageManager.Message(string.Format("{0} : {1}/{2}", item.Title, Objnumber, Totalnumber));
            }

            Questlog.Log.UpdateProgress();
            Questlog.Log.Check_Finished();   
        }
         
    }
   
    public void UpdateItemCount()
    {
        Objnumber = Inventory.InventoryScr.ItemCount(Object_type);

        Questlog.Log.UpdateProgress();
        Questlog.Log.Check_Finished();
        
    }

    public void Complete()
    {
        Stack<Item> items = Inventory.InventoryScr.GetItems(Object_type, Totalnumber);

        foreach (var item in items)
        {
            item.Delete_the_Item();
        }
    }
        
}

[System.Serializable]
public class Kill : Objective
{

    public void UpdateKillCount(EnemyHealth aled)
    {
        if (Object_type == aled.Type)
        { 
            Objnumber += 1;
            
            MessageManager.TheMessageManager.Message(string.Format("{0} : {1}/{2}", Object_type, Objnumber, Totalnumber));
            
            Questlog.Log.UpdateProgress();
            Questlog.Log.Check_Finished(); 
        }
    }
}