using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioManager.Instance.playSfx("Click");
        AudioManager.Instance.bgmSrc.UnPause();
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        AudioManager.Instance.playSfx("Click");
        AudioManager.Instance.bgmSrc.Pause();
    }

    public void QuitGame()
    {
        AudioManager.Instance.playSfx("Click");
        Debug.Log("Quit Game");
        LoadMenu();
    }

    public void LoadMenu()
    {
        AudioManager.Instance.playSfx("Click");
        Debug.Log("Load Menu");
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }


}   
