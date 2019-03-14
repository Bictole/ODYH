using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pnjmovement : MonoBehaviour
{
    // Speed of the PNJ
    public float movespeed;

    // Attribute of the PNJ, Rigibody2D and Animator
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;

    // To know if the PNJ is walking or not
    public bool isWalking;

    // Time where the PNJ is walking
    public float walkTime;
    private float walkCounter;

    // Time where the PNJ is waiting
    public float waitTime;
    private float waitCounter;

    // To know if the PNJ will go up, down, left or right (number between 0 and 3)
    private int Walkdirection;

    // Determine if the PNJ has a walkArea and what is the max and min value of it
    public Collider2D walkArea;
    private bool hasWalkZone;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    
    // Direction where the PNJ will move
    private Vector3 moveDirection;

    // To stop the PNJ when the player is talking to him
    public bool stopmove;
    private DialogueManager dlgmanager;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        dlgmanager = FindObjectOfType<DialogueManager>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        stopmove = false;
        
        ChooseDirection();

        if (walkArea != null)
        {
            minWalkPoint = walkArea.bounds.min;
            maxWalkPoint = walkArea.bounds.max;
            hasWalkZone = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dlgmanager.dialogueActive)
        {
            stopmove = false;
        }
        if (stopmove)
        {
            myRigidbody2D.velocity = Vector2.zero;
            return;
        }
        
        
        
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;
            ActivateLayer("Walk Layer");
            myAnimator.SetFloat("x", myRigidbody2D.velocity.x);
            myAnimator.SetFloat("y", myRigidbody2D.velocity.y);

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }

            switch (Walkdirection)
            {
                case 0:
                {
                    myRigidbody2D.velocity = new Vector2(0, movespeed);
                    if (hasWalkZone && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                }
                case 1:
                {
                    myRigidbody2D.velocity = new Vector2(movespeed, 0);
                    if (hasWalkZone && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                }
                case 2:
                {
                    myRigidbody2D.velocity = new Vector2(0,-movespeed);
                    if (hasWalkZone && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                }
                case 3:
                {
                    myRigidbody2D.velocity = new Vector2(-movespeed,0);
                    if (hasWalkZone && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                }
            }
                
        }
        else
        {
            waitCounter -= Time.deltaTime;
            ActivateLayer("Idle Layer");
            myRigidbody2D.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    // Choose randomly a direction
    public void ChooseDirection()
    {
        Walkdirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
    
    // Allow to switch between the animation of walk and idle
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i,0);
        }
        
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName),1);
    }
}
