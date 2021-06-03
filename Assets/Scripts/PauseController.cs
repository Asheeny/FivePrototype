using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool isPaused = false;
    private Scene main;

    [SerializeField]
     private GameObject pausePanel = null;

    private void Awake()
    {
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                AudioListener.volume = 1f;
                Resume();
            }
            else
            {
                AudioListener.volume = 0.5f;
                Pause();
            }
        }
    }

    public void Resume()
    {
        AudioListener.volume = 1f;
        pausePanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void SelectLevel()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;
        Loader.Load(Loader.Scene.Level_Select);
    }

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;
        Loader.Load(Loader.Scene.Main_Menu);
    }
}
