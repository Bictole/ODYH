using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chargement : MonoBehaviour
{

    public float timeToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToLoad -= Time.deltaTime;
        if (timeToLoad <= 0)
        {
            // Load the next Scene in the build order
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1f;
        }
        
    }
    
    
    
}
