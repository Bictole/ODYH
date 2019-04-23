using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawn : MonoBehaviour
{

    // Time to Respawn
    public float waitToRespawn;
    public float _waitToRespawnCounter;
    
    [SerializeField]
    private GameObject[] monster;

    private float minbounds_x;

    private float maxbounds_x;

    private float minbounds_y;

    private float maxbounds_y;

    [SerializeField] private float nb_enemy_max = 0;

    public static float nb_enemy;

    private GameObject enemy;
    [SerializeField] private Transform[] sp;
    
    
    // Permet de savoir si l'ennemi est mort ou non pour lancer la séquence de réapparition
    public static bool reloading;
    
    private static bool spawnerexists;

    // Start is called before the first frame update
    void Start()
    {

        _waitToRespawnCounter = waitToRespawn;
        nb_enemy = 0;

        sp = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            sp[i] = transform.GetChild(i);
        }

        reloading = false;
        
        if (!spawnerexists)
        {
            spawnerexists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!reloading && nb_enemy < nb_enemy_max)
        {
            int i = Random.Range(0, transform.childCount);
            int type = Random.Range(0, monster.Length);

            minbounds_x = transform.GetChild(i).GetComponent<BoxCollider2D>().bounds.min.x;
            maxbounds_x = transform.GetChild(i).GetComponent<BoxCollider2D>().bounds.max.x;

            minbounds_y = transform.GetChild(i).GetComponent<BoxCollider2D>().bounds.min.y;
            maxbounds_y = transform.GetChild(i).GetComponent<BoxCollider2D>().bounds.max.y;

            GameObject enemy = monster[type];

            Instantiate(enemy,
                new Vector3(Random.Range(minbounds_x, maxbounds_x), Random.Range(minbounds_y, maxbounds_y), 0f),
                Quaternion.identity);
            nb_enemy += 1;
        }


        if(reloading)
        {
            _waitToRespawnCounter -= Time.deltaTime;
            if (_waitToRespawnCounter <= 0)
            {
                reloading = false;
                _waitToRespawnCounter = waitToRespawn;
            }
        }
        
    }
}
