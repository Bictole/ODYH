using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    // To attach to a map

    // Time to Respawn
    public float waitToRespawn;
    private float _waitToRespawnCounter;
    public GameObject theMonster;
    
    // Coordinates of the spawn position
    public float spawnX;
    public float spawnY;

    // To know if the enemy respawn or not
    private bool reloading;
    private EnemyHealth _enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        _enemyHealth = theMonster.GetComponent<EnemyHealth>();
        _waitToRespawnCounter = waitToRespawn;
    }

    // Update is called once per frame
    void Update()
    {
        reloading = _enemyHealth.reloading;
        if (reloading)
        {
            theMonster.SetActive(false);
            _waitToRespawnCounter -= Time.deltaTime;
            if (_waitToRespawnCounter < 0)
            {
                theMonster.transform.position = new Vector2(spawnX, spawnY);
                theMonster.SetActive(true);
                _waitToRespawnCounter = waitToRespawn;
                reloading = false;
                _enemyHealth.reloading = false;
            }
        }
    }
}
