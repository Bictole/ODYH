using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{


    // Liste des items que le monstre peut drop
    [SerializeField]
    private GameObject[] itemsdrop;
    
    //Vie max du monstre 
    public int monsterMaxHealth;
    //vie actuelle du monstre
    public int monsterHealth;

    // Script avec l'xp du personnage
    private PlayerStats _playerStats;
    public int experience;
    
    // Slider correspond à la barre de vie du monstre
    public Slider healthbar;
    
    
    [SerializeField]
    private string type;

    public string Type
    {
        get => type;
    }

    private Questlog questlog;

    public Questlog Questlog
    {
        get => questlog;
    }
    
    public Signal roomSignal;
    
    public LootTable thisLoot;
    
    // Start is called before the first frame update
    void Start()
    {
        //set de la vie au max
        SetMaxHealth();
        _playerStats = FindObjectOfType<PlayerStats>();
        healthbar.maxValue = monsterMaxHealth;
    }

    private void OnEnable()
    {
        SetMaxHealth();
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = monsterHealth;
        if (monsterHealth <= 0)
        {
			if (Questlog.Log.In_Progress != null)
            {
                foreach (var obj in Questlog.Log.In_Progress.Killarray)
                {
                    obj.UpdateKillCount(this);
                }
            }
			
            _playerStats.GainExp(experience);

//            // Aléatoire qui choisi quel objet drop ou si ne rien drop
//            int rnd = Random.Range(0, itemsdrop.Length + 1);
//            if (rnd < itemsdrop.Length)
//            {
//                Instantiate(itemsdrop[rnd], transform.position, transform.rotation);
//            }
            

            MakeLoot();
            // Lance la procédure de réapparition et décrémente le nb d'ennemis vivant sur la carte
            RandomSpawn.nb_enemy -= 1;
            RandomSpawn.reloading = true;
            if (GetComponentInParent<Room>() != null)
            {
                roomSignal.Raise();
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }

    
    public void HurtEnemy(int damage)
    {
        monsterHealth -= damage;
    }

    public void SetMaxHealth()
    {
        monsterHealth = monsterMaxHealth;
    }
    
    
    
    private void MakeLoot()
    {
        if(thisLoot != null)
        {
            Pickitems current = thisLoot.LootPowerup();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
    
    
}