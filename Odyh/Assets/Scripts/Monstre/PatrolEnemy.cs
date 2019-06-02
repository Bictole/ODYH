using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class PatrolEnemy : MonsterController
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;

    public float roundingDistance;
    
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        IsPatrol = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        healthGroup = gameObject.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>();
        ChangeGoal();
    }

    public void LateUpdate()
    {
        if (!Isaggro)
        {
            if (Vector3.Distance(transform.position, currentGoal.position) <= roundingDistance)
            {
                ChangeGoal();
            }
        }
        
    }

    public void ResetPos()
    {
        healthGroup.alpha = 0;
        Vector3 pointdirection = (path[currentPoint].position - transform.position).normalized;

        moveDirection = new Vector3(pointdirection.x * moveSpeed, pointdirection.y * moveSpeed, 0f);

        myAnimator.SetFloat("x", moveDirection.x);
        myAnimator.SetFloat("y", moveDirection.y);

        myRigidbody2D.velocity = moveDirection;
        Block();
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
            
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
