using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // To attach to a map

    // Time to Respawn
    public float waitToRespawn;
    private float _waitToRespawnCounter;
    public GameObject thePlayer;
    
    // Coordinates of the spawn position
    public float spawnX;
    public float spawnY;

    // To know if the enemy respawn or not
    private bool reloading;
    private PlayerHealth _playerHealth;


    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = thePlayer.GetComponent<PlayerHealth>();
        _waitToRespawnCounter = waitToRespawn;
    }

    // Update is called once per frame
    void Update()
    {
        reloading = _playerHealth.reloading;
        if (reloading)
        {
            thePlayer.SetActive(false);
            _waitToRespawnCounter -= Time.deltaTime;
            if (_waitToRespawnCounter < 0)
            {
                thePlayer.transform.position = new Vector2(spawnX, spawnY);
                thePlayer.SetActive(true);
                _waitToRespawnCounter = waitToRespawn;
                reloading = false;
                _playerHealth.reloading = false;
            }
        }
    }
}
