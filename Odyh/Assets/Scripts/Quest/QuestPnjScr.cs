using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPnjScr : MonoBehaviour
{
    public Quest Quest { get; set; }

    public void SelectQuest()
    {
        QuestPnjUI.QuestPnjUi.QuestInfo(Quest);
    }
}
