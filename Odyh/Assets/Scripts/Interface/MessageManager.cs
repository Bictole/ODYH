using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject messagePrefab;

    private static MessageManager messageManager;

    public static MessageManager TheMessageManager
    {
        get
        {
            if (messageManager == null)
            {
                messageManager = FindObjectOfType<MessageManager>();
            }

            return messageManager;
        }
    }

    public void Message(string message)
    {
        GameObject q = Instantiate(messagePrefab, transform);
        q.GetComponent<Text>().text = message;
        
        q.transform.SetAsFirstSibling();
        
        Destroy(q, 2);
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
