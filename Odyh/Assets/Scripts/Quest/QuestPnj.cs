using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPnj : MonoBehaviour
{
    [SerializeField]
    private Quest[] quest;

    [SerializeField]
    private Questlog log;

    private void Awake()
    {
        log.Take_a_quest(quest[0]);
        log.Take_a_quest(quest[1]);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
