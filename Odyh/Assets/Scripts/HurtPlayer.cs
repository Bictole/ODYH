using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    //A attacher a des ennemis !
    
    
    //set des dégats de l'ennemi
    public int ennemy_damage;

    //degats avec malus de défence du joueur
    private int damage;
    
    // Effet de dégats
    public GameObject damageBurst;

    // Nombre de dégats
    public GameObject damageNumber;

    private PlayerStats thestats;
    
    // Start is called before the first frame update
    void Start()
    {
        thestats = FindObjectOfType<PlayerStats>();
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
            damage = ennemy_damage - thestats.playerdefence;

            if (damage <= 0 )
            {
                damage = 1; 
            }
            
            other.gameObject.GetComponent<PlayerHealth>().HurtPlayer(damage);
            Instantiate(damageBurst, other.transform.position, other.transform.rotation);
            var clone = Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = damage;
        }
    }
}
