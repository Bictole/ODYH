using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCh : MonoBehaviour
{
    // Variable -> la scene ou on envoie le personnage
    public string scene_a_charger;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //On utilise le OnTrigger pour activer cette fonction des que le perso déclenche ce trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(scene_a_charger);
        }
    }
}
