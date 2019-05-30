using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonsterController
{
    public GameObject projectile;

    public float fireDelay;
    private float fireDelayCounter;

    public bool canfire;
    public void LateUpdate()
    {
        fireDelayCounter -= Time.deltaTime;
        if (fireDelayCounter <= 0)
        {
            canfire = true;
            fireDelayCounter = fireDelay;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        IsTurret = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        healthGroup = gameObject.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>();
        fireDelayCounter = fireDelay;
    }


    public void Fire()
    {
        if (canfire)
        {
            Vector3 tempvector = player.transform.position - transform.position ;
            GameObject current = Instantiate(projectile, transform.position, transform.rotation);
            current.GetComponent<Enemyprojectile>().Launch(tempvector);
            canfire = false;
        }
        
    }
}
