using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPnjUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasgroup;

    private QuestPnj _questPnj;

    private static QuestPnjUI _questPnjUi;

    private PlayerStats _playerStats;

    public static QuestPnjUI QuestPnjUi
    {
        get
        {
            if (_questPnjUi == null)
            {
                _questPnjUi = FindObjectOfType<QuestPnjUI>();
            }

            return _questPnjUi;
        }
    }

    [SerializeField] private GameObject backButton, acceptButton, completeButton, QuestDescritption;

    [SerializeField]
    private GameObject questgiverprefab;

    [SerializeField]
    private Transform QuestArea;
    
    private List<GameObject> quests = new List<GameObject>();

    private Quest selected;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
    }

    public void ShowQuests(QuestPnj questPnj)
    {
    
        foreach (GameObject gameObject in quests)
        {
            Destroy(gameObject);
        }
        
        QuestArea.gameObject.SetActive(true);
        QuestDescritption.SetActive(false);
        
        foreach (Quest quest in _questPnj.Quests)
        {
            if (quest != null)
            {
                GameObject q = Instantiate(questgiverprefab, QuestArea);
                q.GetComponent<Text>().text = "[" + quest.QuestLevel + "] " + quest.Title;
                q.GetComponent<QuestPnjScr>().Quest = quest;
                quests.Add(q);

                if (Questlog.Log.QuestAlreadyHere(quest) && quest.QuestIsFinished)
                {
                    q.GetComponent<Text>().text += "(Done)";
                }
                else if (Questlog.Log.QuestAlreadyHere(quest))
                {
                    Color color = q.GetComponent<Text>().color;
                    color.a = 0.5f;
                    q.GetComponent<Text>().color = color;
                }
            }
            
        }
    }

    public void QuestInfo(Quest quest)
    {
        this.selected = quest;

        if (Questlog.Log.QuestAlreadyHere(quest) && quest.QuestIsFinished)
        {
            acceptButton.SetActive(false);
            completeButton.SetActive(true);
        }
        else if (!Questlog.Log.QuestAlreadyHere(quest))
        {
            acceptButton.SetActive(true);
        }
 
        backButton.SetActive(true);
        QuestArea.gameObject.SetActive(false);
        QuestDescritption.SetActive(true);
        
        string title = quest.Title;
        string description = quest.Description;
        string obj = "\nObjectives\n";
        
        foreach (var o in quest.Collectarray)    //on affiche correctement les objectifs
        {
            obj += o.Object_type + " : " + o.Objnumber + " / " + o.Totalnumber + "\n";
        }

        QuestDescritption.GetComponent<Text>().text = string.Format("{0} : \n{1}", title, description);    //on set le format final
    }

    public void Back()
    {
        backButton.SetActive(false);
        acceptButton.SetActive(false);
        ShowQuests(_questPnj);
        completeButton.SetActive(false);
    }

    public void Accept()
    {
        Questlog.Log.Take_a_quest(selected);
        Back();
    }
    
    public void OpenUI(QuestPnj questPnj)
    {
        this._questPnj = questPnj;
        ShowQuests(questPnj);
        canvasgroup.alpha = 1;
        canvasgroup.blocksRaycasts = true;
    }
    
    public void CloseUI()
    {
        _questPnj.IsOpen = false;
        canvasgroup.alpha = 0;
        canvasgroup.blocksRaycasts = false;
        _questPnj = null;
    }

    public void CompleteQuest()
    {
        if (selected.QuestIsFinished)
        {
            for (int i = 0; i < _questPnj.Quests.Length; i++)
            {
                if (selected == _questPnj.Quests[i])
                {
                    _questPnj.Quests[i] = null;
                }
            }

            foreach (Collect o in selected.Collectarray)
            {
                Inventory.InventoryScr.itemCountChangedEvent -= new ItemCountChanged(o.UpdateItemCount);
                o.Complete();
            }

            _playerStats.GainExp(_playerStats.QuestXP(selected));
            Questlog.Log.RemoveQuest(selected);
            Back();
        }
    }
    
}
