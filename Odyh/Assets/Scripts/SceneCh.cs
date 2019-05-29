using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCh : MonoBehaviour
{
    // Variable -> la scene ou on envoie le personnage
    public string scene_a_charger;

    public Vector2 playerPosition;
    public VectorValue playerStorage;

    

    //On utilise le OnTrigger pour activer cette fonction dès que le perso déclenche ce trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            playerStorage.initialvalue = playerPosition;
            SceneManager.LoadScene(scene_a_charger);
        }
    }
}
