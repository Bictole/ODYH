using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    //A attacher au joueur!
    
    
    //set des dégats que l'on doit infliger aux monstres
    public int player_damage;


    // Effet de dégats    
    public GameObject damageBurst;

    // Zone du point d'impact
    public Transform Hitpoint;

    // Nombre de dégats du joueur
    public GameObject damageNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie que le personnage est bien en train d'attaquer
        if (gameObject.GetComponentInParent<Character>().IsAttacking)
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                // On inflige des dégats à l'ennemi, et on affiche l'effet de dégats et le nombre de dégats au niveau du point d'impact.
                other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(player_damage);
                Instantiate(damageBurst, Hitpoint.position, Hitpoint.rotation);
                var clone = Instantiate(damageNumber, Hitpoint.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = player_damage;
            }
        }
        
    }
}
