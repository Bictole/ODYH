using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    //tableau contenant toute les musiques
    public AudioSource[] music;

    //int pour savoir quelle musique est jouée    
    public int current_music;

    //bool pour savoir si la musique peut être joué
    public bool playable;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playable)                        //on check si une musique peux etre joué sinon on l'arrête
        {
            if (!music[current_music].isPlaying)           //on la joue si elle ne joue pas encore
            {
                music[current_music].Play();
            }
        }
        else
        {
            music[current_music].Stop();    
        }
    }

    public void ChangeMusic(int nextMusic)        //fonction pour changer de musique
    {
        music[current_music].Stop();

        current_music = nextMusic;        //on arrete l'ancienne, on change la valeur et on play la nouvelle
        
        music[current_music].Play();
    }
}

    
