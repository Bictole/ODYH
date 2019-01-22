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

    private Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    /// <summary>
    ///  Moves the player
    /// </summary>
    public void Move()
    {
        //Makes sure that the player moves
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {
            //Animate's the Player's movement
            AnimateMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }


    /// <summary>
    /// Makes the player animate in the correct direction
    /// </summary>
    /// <param name="direction"></param>

    public void AnimateMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);

        //Sets the animation parameter so that he faces the correct direction
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}
