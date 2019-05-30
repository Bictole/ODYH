using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPnj : MonoBehaviour, Interactionnable
{
    [SerializeField] 
    private List<Quest> _quests = new List<Quest>();
    
    public List<Quest> Quests
    {
        get
        {
            if (_quests == null)
            {
                _quests = new List<Quest>();
            }

            return _quests;
        }
    }

    [SerializeField]
    private Sprite questionMark, quesionMarkGrey, exclamationMark;

    [SerializeField]
    private SpriteRenderer _renderer;
    
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
        }
    }

    private void Update()
    {
        QuestStatus();
    }
}
