using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Character : MonoBehaviour
{
    /// <summary>
    ///  The Player's movement speed
    /// </summary>
    public float speed;

    /// <summary>
    /// The Player's direction
    /// </summary>
    protected Vector3 direction;

    public Vector3 nextdirection;

    private Camera camera;

    // Attribute of a character, Rigidbody2D and Animator
    private Rigidbody2D myRigidbody;
    protected Animator myAnimator;

    // To know if the character is attacking
    public bool IsAttacking;
    public bool IsAttackingrangewithbow;
    public bool IsAttackingrangewithstaff;

    
    // To know if the player exists
    private static bool playerExists;
    
    private Interactionnable interactable;

    // To stop the player when he is talking to a PNJ for example
    public bool stopmove;

    public bool IsMoving
    {
        get { return direction.x != 0 || direction.y != 0; }
    }

    // To know if the inventory is open or not
    public bool InInventory;

    //To know if the player is push and the duration
    public bool IsPush;
    private float timepush;

    // Start is called before the first frame update
    public virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        camera = FindObjectOfType<Camera>();

        stopmove = false;
        IsPush = false;
        
//        // Permet de ne pas détruire le perso quand on charge une scene -> sans duplication
//        if (!playerExists)
//        {
//            playerExists = true;
//            DontDestroyOnLoad(transform.gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        HandleLayers();
        ClickTarget();
    }

    private void FixedUpdate()
    {
        if (IsPush)
        {
            IsPush = false;
            timepush = 0.2f;
        }
        if(timepush > 0)
            timepush -= Time.deltaTime;  
        if(timepush <= 0 && !IsPush && !stopmove)
        {
            Move();
        }
    }

    /// <summary>
    ///  Moves the player
    /// </summary>
    public void Move()
    {
        // If player is pressing mouse button 2, double the speed
        if (Input.GetMouseButton(4))
        {
            myRigidbody.velocity = direction.normalized * speed * 4;
        }
        else
        {
            myRigidbody.velocity = direction.normalized * speed;
        }

        // Makes sure that the player moves
        // myRigidbody.velocity = direction.normalized * speed;
    }

    // Allow to switch between the different layers, to play the good animation for the correct action.
    public void HandleLayers()
    {
        if (stopmove)
        {
            direction.x = 0;
            direction.y = 0;
            myRigidbody.velocity = Vector2.zero;
        }
        else
        {
            if (IsAttacking && !InInventory)
            {
                ActivateLayer("Attack Layer");
            }

            else if (IsAttackingrangewithbow && !InInventory)
            {
                if (IsMoving)
                {
                    direction.x = 0;
                    direction.y = 0;
                }

                ActivateLayer("Bow Layer");
            }
            else if (IsAttackingrangewithstaff && !InInventory)
            {
                if (IsMoving)
                {
                    direction.x = 0;
                    direction.y = 0;
                }

                ActivateLayer("Spell Layer");
            }
            //Checks if we are moving or standing still, if we are moving then we need to play the movement
            else if (IsMoving)
            {
                ActivateLayer("Walk Layer");
            
                //Sets the animation parameter so that he faces the correct direction
                myAnimator.SetFloat("x", direction.x);
                myAnimator.SetFloat("y", direction.y);
            
                StopAttack();
            }
            else
            {
                //Makes sur that we will go back to idle when we aren't pressing any keys.
                ActivateLayer("Idle Layer");
            }
        }
    }

    // Method which switch the weight of a layer to 1 
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i,0);
        }
        
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName),1);
    }

    
    public void StopAttack()
    {
        IsAttacking = false;
        myAnimator.SetBool("attack",IsAttacking);
    }
    
    public void StopAttackrangewithbow()
    {
        IsAttackingrangewithbow = false;
        myAnimator.SetBool("attackrangebow",IsAttackingrangewithbow);
    }
    
    public void StopAttackrangewithstaff()
    {
        IsAttackingrangewithstaff = false;
        myAnimator.SetBool("attackrangestaff",IsAttackingrangewithstaff);
    }


    public void Resetanim()
    {
        myRigidbody.velocity = Vector2.zero;
        ActivateLayer("Idle Layer");
    }
	
	public void Interagir()
    {
        if (interactable != null)
        {
            interactable.Interagir();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactionnable"))
        {
            interactable = other.GetComponent<Interactionnable>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactionnable"))
        {
            if (interactable != null)
            {
                interactable.StopInteraction();
                interactable = null;
            }
        }
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
                Mathf.Infinity, 2048);

            if (hit.collider != null && hit.collider.CompareTag("Interactionnable") && hit.collider.gameObject.GetComponent<Interactionnable>() == interactable)
            {
                Interagir();
            }
        }
    }
}
