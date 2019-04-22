using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField]
    private GameObject audio;

    public static bool audioexists;
    // Start is called before the first frame update
    void Start()
    {
        if (!audioexists)
        {
            audioexists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
