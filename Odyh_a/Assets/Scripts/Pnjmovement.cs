using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pnjmovement : MonoBehaviour
{
    public float movespeed;

    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;

    public bool isWalking;

    public float walkTime;
    private float walkCounter;

    public float waitTime;
    private float waitCounter;

    private int Walkdirection;

    public Collider2D walkArea;
    private bool hasWalkZone;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    
    private Vector3 moveDirection;

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
                    if (hasWalkZone && transform.position.x < maxWalkPoint.x)
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
            ActivateLayer("IdleLayer");
            myRigidbody2D.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        Walkdirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
    
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i,0);
        }
        
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName),1);
    }
}
