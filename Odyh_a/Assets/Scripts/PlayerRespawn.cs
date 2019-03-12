using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // A ajouter à une map
    
    public float waitToRespawn;
    private float _waitToRespawnCounter;
    public GameObject thePlayer;
    
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
        reloading = thePlayer.GetComponent<PlayerHealth>().reloading;
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
                thePlayer.GetComponent<PlayerHealth>().reloading = false;
            }
        }
    }
}
