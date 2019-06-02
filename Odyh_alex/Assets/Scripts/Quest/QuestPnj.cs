using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPnj : MonoBehaviour, Interactionnable
{
    [SerializeField] 
    private Quest[] _quests;
    
    public Quest[] Quests
    {
        get { return _quests; }
    }
    
    private List<string> compltedQuests = new List<string>();

    public List<string> CompltedQuests
    {
        get => compltedQuests;
        set
        {
            compltedQuests = value;
            
            foreach (string title in compltedQuests)
            {
                for (int i = 0; i < _quests.Length; i++)
                {
                    if (_quests[i] != null && _quests[i].Title == title)
                    {
                        _quests[i] = null;
                    }
                }
            }
        }
    }

    [SerializeField]
    private Sprite questionMark, quesionMarkGrey, exclamationMark;

    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private int QuestGiverID;
    
    public int QuestGiverId
    {
        get => QuestGiverID;
        set => QuestGiverID = value;
    }
    
    [SerializeField]
    private QuestPnjUI questPnjUi;
    
    public bool IsOpen { get; set; }

    private void Start()
    {
        foreach (var q in _quests)
        {
            q.QuestPnj = this;
        }
    }

    public void Interagir()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            questPnjUi.OpenUI(this);
        }   
    }

    public void StopInteraction()
    {
        if (IsOpen)
        {
            IsOpen = false;
            questPnjUi.CloseUI();
        }
    }

    public void QuestStatus()
    {
        int count = 0;
        foreach (var q in _quests)
        {
            if (q != null)
            {
                if (q.QuestIsFinished && Questlog.Log.QuestAlreadyHere(q))
                {
                    _renderer.sprite = questionMark;
                    break;
                }
                else if (!Questlog.Log.QuestAlreadyHere(q))
                {
                    _renderer.sprite = exclamationMark;
                    break;
                }
                else if (!q.QuestIsFinished && Questlog.Log.QuestAlreadyHere(q))
                {
                    _renderer.sprite = quesionMarkGrey;
                }
            }
            else
            {
                count++;

                if (count == _quests.Length)
                {
                    _renderer.enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        QuestStatus();
    }
}
