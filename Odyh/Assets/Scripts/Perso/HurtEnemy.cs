using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    //A attacher au joueur!


    //set des dégats du joueur 
    public int player_damage;

    //dégats avec bonus de niveau ajouté
    private int damage;

    // Effet de dégats    
    public GameObject damageBurst;

    // Zone du point d'impact
    public Transform Hitpoint;

    // Nombre de dégats du joueur
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

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie que le personnage est bien en train d'attaquer
        if (gameObject.GetComponentInParent<Character>().IsAttacking)
        {
            if(other.gameObject.CompareTag("Enemy"))
            {

                damage = player_damage + thestats.playerattack;
                

                // On inflige des dégats à l'ennemi, et on affiche l'effet de dégats et le nombre de dégats au niveau du point d'impact.
                other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(damage);
                Push(other.gameObject);
                Instantiate(damageBurst, Hitpoint.position, Hitpoint.rotation);
                var clone = Instantiate(damageNumber, Hitpoint.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = damage;
            }
        }
        
    }
    
    void Push(GameObject other)
    {
        //Vector3 playerdirection = (other.transform.position - transform.position).normalized;
            
        //Vector3 moveDirection = new Vector3(playerdirection.x, playerdirection.y, 0f);

        MonsterController enemy;
        Vector3 otherposition = new Vector3();

        other.GetComponent<MonsterController>().Ispush = true;
        if (gameObject.transform.position.x > other.gameObject.transform.position.x)
        {
            otherposition += Vector3.left;
            
        }
        if (gameObject.transform.position.x < other.gameObject.transform.position.x)
        {
            otherposition += Vector3.right;
        }
        if (gameObject.transform.position.y > other.gameObject.transform.position.y)
        {
            otherposition += Vector3.down;
            
        }
        if (gameObject.transform.position.y < other.gameObject.transform.position.y)
        {
            otherposition += Vector3.up;
        }
        
        other.gameObject.GetComponent<Rigidbody2D>().velocity = otherposition.normalized * 3;
    }
}
