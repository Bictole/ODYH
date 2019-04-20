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
    
    
    
    //array stockant les objectis pour la quête
    [SerializeField]
    private Collect[] collectarray;

    public Collect[] Collectarray
    {
        get { return collectarray; }
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
    
    
    
    //Nombre d'objets requis pour la quete
    [SerializeField]
    private int totalnumber;
    
    public int Totalnumber
    {
        get { return totalnumber; }
    }
    
    
    
    //type de l'objet requis 
    [SerializeField]    
    private string objet;

    public string Objet
    {
        get { return objet; }
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
    /*public void ItemCount(Item item)
    {
        if (Objet.ToLower() == item.title.ToLower())
        {
            Objnumber = InventoryScr.Myinstance.GetItemCount
            Questlog.Log.UpdateProgress();
            Questlog.Log.Check_Finished()
        }
    }*/
}