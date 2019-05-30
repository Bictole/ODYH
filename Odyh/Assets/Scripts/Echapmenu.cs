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
        player.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
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