using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    //A attacher a des ennemis !
    
    
    //set des dégats que l'on doit infliger au joueur
    public int ennemy_damage;
    
    // Effet de dégats
    public GameObject damageBurst;

    // Nombre de dégats
    public GameObject damageNumber;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Détecte si le joueur entre en collision avec un ennemi et affiche l'effet de dégat, le nombre de dégat et enlève de la vie au joueur selon les dégats de l'ennemi
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().HurtPlayer(ennemy_damage);
            Instantiate(damageBurst, other.transform.position, other.transform.rotation);
            var clone = Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = ennemy_damage;
        }
    }
}
