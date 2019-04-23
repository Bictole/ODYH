using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Tiled2Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MonsterController : MonoBehaviour
{
    // Animator of the enemy
    protected Animator myAnimator;

    // Speed of the enemy
    public float moveSpeed;

    // Rigidbody of the enemy
    private Rigidbody2D myRigidbody2D;

    // To know if the enemy is moving or not
    private bool moving;

    // timeBetweenMove est une valeur fixe modifiable et timeBetweenMoveCounter est un compteur variant entre +/- 25% de timeBetweenMove et correspond à la durée entre chaque déplacement
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;

    // timeToMove est une valeur fixe modifiable et timeToMoveCounter est un compteur variant entre +/- 25% de timeToMove et correspond à la durée du déplacement
    
    public float timeToMove;
    private float timeToMoveCounter;

    // Vector of the direction where the enemy will move
    private Vector3 moveDirection;


    private CircleCollider2D myCircleColider2D;
    
    public GameObject player;

    //Tableau correspondant aux différentes orientations du monstre et les blocks a activer en conséquence
    [SerializeField]
    private Block[] blocks;

    //To know if the enemy is push or not and how many time
    public bool Ispush;

    private float timepush = 0.2f;
    private float timepushCounter;

    //La distance d'aggression
    [SerializeField]
    private float aggrodistance;

    //Permet de savoir si le monstre a aggresser le joueur
    public bool Isaggro;
    
    //Temps pendant lequel le monstre ne subit pas de dégâts
    [SerializeField]
    private float timewithouttakingdmg;
    public float timewithouttakingdmgCounter;
    
    
    
    public CanvasGroup healthGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

        player = GameObject.FindWithTag("Player");
        healthGroup = gameObject.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Ispush)
        {
            if (timepushCounter > 0)
                timepushCounter -= Time.deltaTime;
            else
                Ispush = false;
        }

        
        else if (Isaggro)        //Si le monstre aggresse le joueur, alors il se déplace dans sa direction tant que le temps sans prendre de dégâts n'est pas inférieur à 0
        {
            healthGroup.alpha = 1;
            
            Vector3 playerdirection = (player.transform.position - transform.position).normalized;
            
            moveDirection = new Vector3(playerdirection.x * moveSpeed, playerdirection.y * moveSpeed, 0f);
            
            myAnimator.SetFloat("x",  moveDirection.x);
            myAnimator.SetFloat("y",  moveDirection.y);
            
            myRigidbody2D.velocity = moveDirection;
            
            Block();

            timewithouttakingdmgCounter -= Time.deltaTime;
            if (timewithouttakingdmgCounter < 0)
            {
                Isaggro = false;
            }
        }

        //Si le monstre est à la bonne distance et à le joueur en ligne de vue, il se met à le suivre
        
        else if (InlineofSight() && Vector2.Distance(transform.position,player.transform.position) < aggrodistance)
            {
                healthGroup.alpha = 1;
                
           
                Vector3 playerdirection = (player.transform.position - transform.position).normalized;
            
                moveDirection = new Vector3(playerdirection.x * moveSpeed, playerdirection.y * moveSpeed, 0f);
            
                myAnimator.SetFloat("x",  moveDirection.x);
                myAnimator.SetFloat("y",  moveDirection.y);
            
                myRigidbody2D.velocity = moveDirection;
                Block();
                
            }
            

            else //Sinon il se déplace aléatoirement sur la carte
            {
                healthGroup.alpha = 0;
                if (moving)
                {
                    //Sets the animation parameter so that he faces the correct direction
                    myAnimator.SetFloat("x", moveDirection.x);
                    myAnimator.SetFloat("y", moveDirection.y);

                    timeToMoveCounter -= Time.deltaTime;
                    myRigidbody2D.velocity = moveDirection;

                    if (timeToMoveCounter < 0f)
                    {
                        moving = false;
                        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
                    }
                }
                else
                {
                    timeBetweenMoveCounter -= Time.deltaTime;
                    myRigidbody2D.velocity = Vector2.zero;

                    if (timeBetweenMoveCounter < 0f)
                    {
                        moving = true;
                        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                        moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed,
                            0f);

                        Block();
                    }
                }
                
            }
            
        }
    

    public void Settimewithouttakingdmg()
    {
        timewithouttakingdmgCounter = timewithouttakingdmg;
    }

    //Vérifie si le joueur est dans la ligne de vue du monstre
    private bool InlineofSight()
    {

        Vector3 playerdirection = (player.transform.position - transform.position).normalized;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerdirection,
            Vector2.Distance(transform.position, player.transform.position),256);

        if (!hit.collider)
        {
            return true;
        }
        return false;
        
    }

    //Active les différents blocks selon la direction du monstre
    private void Block()
    {
        foreach (Block block in blocks)
        {
            block.Deactivate();
        }
        if(moveDirection.x < 0 && moveDirection.y < 0.80 && moveDirection.y > -0.80)
            blocks[3].Activate();
        else if(moveDirection.x > 0 && moveDirection.y < 0.80 && moveDirection.y > -0.80)
            blocks[1].Activate();
        else if(moveDirection.x == 0  && moveDirection.y < 0)
            blocks[2].Activate();
        else if(moveDirection.x == 0 && moveDirection.y > 0)
            blocks[0].Activate();
        else if(moveDirection.x > 0 && moveDirection.y > 0)
            blocks[0].Activate();
        else if(moveDirection.x < 0 && moveDirection.y > 0)
            blocks[0].Activate();
        else if(moveDirection.x < 0 && moveDirection.y < 0)
            blocks[2].Activate();
        else if(moveDirection.x > 0 && moveDirection.y < 0)
            blocks[2].Activate();
    }

    public void Settimepush()
    {
        timepushCounter = timepush;
    }

}
