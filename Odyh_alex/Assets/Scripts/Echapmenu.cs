using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Echapmenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private Player player;
    public GameObject OptionMenu;
    public CanvasGroup SaveMenu;
    public GameObject pauseMenu;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        SaveMenu.alpha = 0;
        SaveMenu.blocksRaycasts = false;
        player.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        SaveMenu.alpha = 0;
        SaveMenu.blocksRaycasts = false;
        player.gameObject.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OptionMenuIG()
    {
        pauseMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }
    
    public void SaveMenuIG()
    {
        
        pauseMenu.SetActive(false);
        SaveMenu.alpha = 1;
        SaveMenu.blocksRaycasts = true;
    }
    

    public void Menu()
    {
        Resume();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}