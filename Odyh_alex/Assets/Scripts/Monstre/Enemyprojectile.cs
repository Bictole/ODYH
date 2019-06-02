using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyprojectile : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 directionToMove;
    public float lifetime;
    private float lifetimeSeconds;
    [SerializeField]
    private Rigidbody2D myRigidbody2D;
    
    
    // Start is called before the first frame update
    void Start()
    {
        lifetimeSeconds = lifetime;
        myRigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector3 initialvelocity)
    {
        myRigidbody2D.velocity = initialvelocity * moveSpeed;
        
        float angle = Mathf.Atan2(initialvelocity.y, initialvelocity.x) * Mathf.Rad2Deg;
        
        //transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
    }
    
    
    // Stop le déplacement de l'objet si il rencontre l'environnement
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Environnement") || other.gameObject.CompareTag("Breakable"))
        {
            if (gameObject.GetComponent<HurtPlayer>().damageBurst.name is "Explosion")
            {
                Instantiate(gameObject.GetComponent<HurtPlayer>().damageBurst, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
                
            
        }
    }

}
