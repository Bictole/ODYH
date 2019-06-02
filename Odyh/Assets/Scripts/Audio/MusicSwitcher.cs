using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    //reference au MusicManager
    private MusicManager musicmanager;

    public int nextmusic;

    public bool switch_;
    
    // Start is called before the first frame update
    void Start()
    {
        musicmanager = FindObjectOfType<MusicManager>();

        if (switch_)
        {
            musicmanager.ChangeMusic(nextmusic);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (musicmanager.current_music != nextmusic)
            {
                musicmanager.ChangeMusic(nextmusic);
            }
            //gameObject.SetActive(false);
        }
    }
}
