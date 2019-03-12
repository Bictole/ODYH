using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Tiled2Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SlimeController : MonoBehaviour
{

    public float moveSpeed;

    private Rigidbody2D myRigidbody2D;

    private bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;

    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    public float waitToRespawn;
    private bool reloading;
    public GameObject thePlayer;

    public float spawn_x;
    public float spawn_y;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            
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

                moveDirection = new Vector3(Random.Range(-1f,1f) * moveSpeed,Random.Range(-1f,1f) * moveSpeed,0f);

            }
        }

        if (reloading)
        {
            waitToRespawn -= Time.deltaTime;
            if (waitToRespawn < 0)
            {
                thePlayer.transform.position = new Vector2(spawn_x, spawn_y);
                thePlayer.SetActive(true);
                waitToRespawn = 2;
                reloading = false;
            }
        }
        
        
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.SetActive(false);
            reloading = true;
            thePlayer = other.gameObject;
    
        }
    }*/
}
