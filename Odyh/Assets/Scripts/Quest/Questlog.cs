using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questlog : MonoBehaviour
{
    //on recupere la prefab quest
    [SerializeField]
    private GameObject prefab;
        
    //reference au quest area
    [SerializeField] 
    private Transform qarea;
   
    private static Questlog log;

    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private Text questCount;

    [SerializeField]
    private int maxQuestCount;

    private int currentQuestCount;
    
    //quete en cours
    private Quest in_progress;

    public Quest In_Progress
    {
        get { return in_progress; }
    }
    
    //description de la quete
    [SerializeField]
    private Text description;
    
    private List<Quest> quests = new List<Quest>();
    
    public static Questlog Log
    {
        get
        {
            if (log == null)
            {
                log = FindObjectOfType<Questlog>();
            }
            return log;    
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        questCount.text = currentQuestCount + "/" + maxQuestCount;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            OpenClose();
        }
        
    }

    public void Take_a_quest(Quest quest)
    {
        if (currentQuestCount < maxQuestCount)
        {
            currentQuestCount += 1;
            questCount.text = currentQuestCount + "/" + maxQuestCount;
            
            quests.Add(quest);
        
            GameObject q = Instantiate(prefab, qarea);  //on instancie l'objet quete 

            QuestScr scr = q.GetComponent<QuestScr>();    //on recupere son script
            quest.Qscript = scr;   //on donne la ref du script a la quete
            scr._quest = quest;    //on donne la ref de quete au script
           
        
            q.GetComponent<Text>().text = quest.Title;
        }    
    }

    public void Description(Quest quest)
    {
        if (quest != null)
        {
            if (in_progress != null && in_progress != quest)        //on deselectionne la quete s'il y en a une autre en cours mais pas si la quete est celle en cours
            {
                in_progress.Qscript.Deselect();
            }
            
            string goal = "\nProgress\n";

            foreach (var obj in quest.Collectarray)    //on affiche correctement les objectifs
            {
                goal += obj.Object_type + " : " + obj.Objnumber + " / " + obj.Totalnumber + "\n";
            }
            foreach (var obj in quest.Killarray)   
            {
                goal += obj.Object_type + " : " + obj.Objnumber + " / " + obj.Totalnumber + "\n";
            }    

            in_progress = quest;
            
            string title = quest.Title;

            description.text = string.Format("{0} : \n{1} \n{2}", title, quest.Description, goal);    //on set le format final
        }
    }

    public void UpdateProgress()
    {
        Description(in_progress);
    }


    public void Check_Finished()
    {
        foreach (Quest q in quests)
        {
            q.QuestPnj.QuestStatus();
            q.Qscript.Finished();
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

    public void AbandonButton()
    {
        if (in_progress != null)
        {
            RemoveQuest(in_progress);
        }  
    }

    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
        Destroy(quest.Qscript.gameObject);
        description.text = string.Empty;
        in_progress = null;
        currentQuestCount -= 1;
        questCount.text = currentQuestCount + "/" + maxQuestCount;
        quest.QuestPnj.QuestStatus();
        quest.Qscript = null;
    }

    public bool QuestAlreadyHere(Quest quest)
    {
        return quests.Exists(x => x.Title == quest.Title);
    }
    
}
