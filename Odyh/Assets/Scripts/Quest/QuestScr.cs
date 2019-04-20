using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScr : MonoBehaviour
{
    
    public Quest _quest { get; set;  }

    private bool Text_Finished = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Track()    //Change la couleur de la quete select puis appelle Description()
    {
        GetComponent<Text>().color = Color.red;
        Questlog.Log.Description(_quest);
    }

    public void Deselect()    //Change la couleur de la quete deselectionnée
    {
        GetComponent<Text>().color = Color.white;
    }


    public void Finished()
    {
        if (_quest.QuestIsFinished && !Text_Finished)
        {
            Text_Finished = true;
            GetComponent<Text>().text += "(Done)";
        }
        else if (!_quest.QuestIsFinished)
        {
            Text_Finished = false;
            GetComponent<Text>().text = _quest.Title;
        }
    }
    
    
    
    
}
