using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donjon : Room
{

    public bool roomclear;

    private int nbenemy;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Roomisclear()
    {
        nbenemy = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Enemy"))
            {
                nbenemy += 1;
            }
        }

        roomclear = nbenemy == 0;
        return roomclear;
    }
    
    
}
