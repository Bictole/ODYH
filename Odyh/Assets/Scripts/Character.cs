using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    /// <summary>
    ///  The Player's movement speed
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// The Player's direction
    /// </summary>
    protected Vector2 direction;

    private Rigidbody2D myRigidbody;

    protected Animator myAnimator;

    public bool IsAttacking = false;

    private static bool playerExists;

    public bool stopmove;

    public bool IsMoving
    {
        get { return direction.x != 0 || direction.y != 0; }
    }
        

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        stopmove = false;
        
        // Permet de ne pas détruire le perso quand on charge une scene -> sans duplication
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    ///  Moves the player
    /// </summary>
    public void Move()
    {
        //Makes sure that the player moves
        myRigidbody.velocity = direction.normalized * speed;
    }

    public void HandleLayers()
    {
        if (stopmove)
        {
            myRigidbody.velocity = Vector2.zero;
            return;
        }
        if (IsAttacking)
        {
            if (IsMoving)
            {
                direction.x = 0;
                direction.y = 0;
            }

            ActivateLayer("Attack Layer");
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
}
