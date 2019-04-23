using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //Reference au effets sonores
    public AudioSource hurt_the_player;

    public AudioSource player_is_dead; 

    public AudioSource player_is_attacking;

    public static bool SFxExist;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!SFxExist)
        {
            SFxExist = true;
            DontDestroyOnLoad(transform.gameObject);    //Condition pour éviter les duplications lors des changements de scène
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
