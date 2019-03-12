using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    // A ajouter à une map.


    public float waitToRespawn;
    private float _waitToRespawnCounter;
    public GameObject theMonster;
    
    public float spawnX;
    public float spawnY;

    private bool reloading;
    
    // Start is called before the first frame update
    void Start()
    {
        _waitToRespawnCounter = waitToRespawn;
    }

    // Update is called once per frame
    void Update()
    {
        reloading = theMonster.GetComponent<EnemyHealth>().reloading;
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
                theMonster.GetComponent<EnemyHealth>().reloading = false;
            }
        }
    }
}
