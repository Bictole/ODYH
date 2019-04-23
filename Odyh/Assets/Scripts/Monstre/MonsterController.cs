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

    [SerializeField]
    private Block[] blocks;

    //To know if the enemy is push or not and how many time
    public bool Ispush;
    [SerializeField]
    private float timepush = 0.2f;

    [SerializeField]
    private float aggrodistance;

    public bool Isaggro;
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
            if (timepush > 0)
                timepush -= Time.deltaTime;
            else
                Ispush = false;
        }

        else if (Isaggro)
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

        
        
        else if (timepush <= 0 && !Ispush)
        {
            if (InlineofSight() && Vector2.Distance(transform.position,player.transform.position) < aggrodistance)
            {
                healthGroup.alpha = 1;
                
           
                Vector3 playerdirection = (player.transform.position - transform.position).normalized;
            
                moveDirection = new Vector3(playerdirection.x * moveSpeed, playerdirection.y * moveSpeed, 0f);
            
                myAnimator.SetFloat("x",  moveDirection.x);
                myAnimator.SetFloat("y",  moveDirection.y);
            
                myRigidbody2D.velocity = moveDirection;
                Block();
                
            }
            

            else
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
    }

    public void Settimer()
    {
        timewithouttakingdmgCounter = timewithouttakingdmg;
    }

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
    
}
