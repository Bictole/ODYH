using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class DestroyOvertime : MonoBehaviour
{
    
    // Time to destroy an object
    public float timeToDestroy;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
