using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Echapmenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public Player player;
    public GameObject OptionMenu;
    public CanvasGroup SaveMenu;
    public GameObject pauseMenu;

    public SaveManager SaveManager;
    

    void Start()
    {
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
        player.gameObject.SetActive(true);
        SaveMenu.alpha = 0;
        SaveMenu.blocksRaycasts = false;
        player.Resetanim();
        player.InInventory = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SaveManager.InInterface = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.gameObject.SetActive(false);
        SaveMenu.alpha = 0;
        SaveMenu.blocksRaycasts = false;
        pauseMenu.SetActive(true);
        GameIsPaused = true;
        SaveManager.InInterface = false;
    }

    public void OptionMenuIG()
    {
        pauseMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }
    
    public void SaveMenuIG()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        SaveMenu.alpha = 1;
        SaveMenu.blocksRaycasts = true;
        SaveManager.InInterface = true;
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