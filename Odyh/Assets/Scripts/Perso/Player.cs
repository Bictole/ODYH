using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Player : Character
{
    private SFXManager sfx;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        sfx = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    protected override void Update()
    {
         if (stopmove)
             return;
         
         Getinput();
         base.Update();
    }
    private void Getinput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.Z))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!IsAttacking)
            // Allow to put a delay in the method
            StartCoroutine(Attack());
        }
    }

    
    private IEnumerator Attack()
    {
        sfx.player_is_attacking.Play();
        IsAttacking = true;
        myAnimator.SetBool("attack", IsAttacking);

        // delay between each attack
        yield return new WaitForSeconds(0.4f);

        
        
        StopAttack();
    }


}
