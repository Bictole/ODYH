using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{

    public VolumeController[] controller;

    public float volume;

    public float maxvolume = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (volume > maxvolume)
        {
            volume = maxvolume;
        }

        foreach (var sound in controller)
        {
            sound.LevelSet(volume);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
