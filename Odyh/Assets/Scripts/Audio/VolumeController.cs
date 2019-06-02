using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{

    private AudioSource audio;

    private float _level;
    public float defaultlevel;
    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSet(float volume)
    {
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
        }
        
        _level = defaultlevel * volume;
        audio.volume = _level;
    }
}
