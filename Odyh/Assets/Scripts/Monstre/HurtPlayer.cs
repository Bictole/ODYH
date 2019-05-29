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

    // Stats du joueur
    private PlayerStats thestats;

    private Player player;
   
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        thestats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Détecte si le joueur entre en collision avec un ennemi et affiche l'effet de dégat, le nombre de dégat et enlève de la vie au joueur selon les dégats de l'ennemi
    
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.name == "Player" && gameObject.GetComponent<CircleCollider2D>().IsTouching(other.collider))
        {
            // Réduit les dégats subits en fonctions des stats du joueur
            damage = ennemy_damage - thestats.playerdefence;

            if (damage <= 0)
            {
                damage = 1;
            }


            other.gameObject.GetComponent<PlayerHealth>().HurtPlayer(damage);
            Push(other.gameObject.GetComponent<Collider2D>());
            Instantiate(damageBurst, other.transform.position, other.transform.rotation);
            var clone = Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = damage;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Enemy Projectile") && other.CompareTag("Player"))
        {
            // Réduit les dégats subits en fonctions des stats du joueur
            damage = ennemy_damage - thestats.playerdefence;

            if (damage <= 0)
            {
                damage = 1;
            }


            other.gameObject.GetComponent<PlayerHealth>().HurtPlayer(damage);
            Push(other);
            Instantiate(damageBurst, other.transform.position, other.transform.rotation);
            var clone = Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = damage;
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Fonction qui permet le recul du joueur lorsqu'il subit des dégâts
    /// </summary>
    /// <param name="other"></param>
    void Push(Collider2D other)
    {
        player.IsPush = true;
        Vector3 otherposition = new Vector3();
        
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

        other.gameObject.GetComponent<Rigidbody2D>().velocity = otherposition.normalized * player.speed;
        
    }
}
