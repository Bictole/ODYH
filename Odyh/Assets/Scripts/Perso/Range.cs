﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{

    public float speed;

    public Rigidbody2D MyRigidbody2D;

    public bool Isattacking;

    //Durée de vie de l'objet
    [SerializeField]
    private float lifetime;

    private bool canmove = true;

    public float offset;
    [SerializeField]
    protected GameObject startposition;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    // fait bouger l'objet dans la bonne drection si il peut
    public void Setup(Vector2 velocity)
    {
        if (canmove)
        {
            MyRigidbody2D.velocity = velocity.normalized * speed;
        
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        
            //transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);
        }
        
        
    }

    // Stop le déplacement de l'objet si il rencontre l'environnement
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Environnement") || other.gameObject.CompareTag("Breakable"))
        {
            if (gameObject.GetComponent<HurtEnemy>().damageBurst.name is "Explosion")
            {
                Instantiate(gameObject.GetComponent<HurtEnemy>().damageBurst, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
                
            
        }
    }
    
}
