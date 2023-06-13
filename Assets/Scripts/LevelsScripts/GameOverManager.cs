using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static bool lostGame = false;
    [SerializeField] protected Text FinalScore;
    [SerializeField] protected Text gameOverText;
    [SerializeField] protected Text explanationText;
    [SerializeField] protected string winningMessage;

    [SerializeField] protected string losingMessage;

    private ProgressManager progressManagerScript;

    // Start is called before the first frame update

    // Update is called once per frame

    void Start()
    {
        GameObject progressManager = GameObject.FindWithTag("ProgressManager");
        progressManagerScript = progressManager.GetComponent<ProgressManager>();
    }
    
    public void gameOverScreen(int score)
    {
        AudioManager.Instance.playBgm("GameOver");
        Debug.Log("Game Over screen called");
        explanationText.text = losingMessage;
        gameOverText.text = "Game Over!";
        progressManagerScript.setHighScore(score, SceneManager.GetActiveScene().name);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void winningScreen(int score)
    {
        AudioManager.Instance.playBgm("LevelDone");
        Debug.Log("Winning screen called");
        gameOverText.text = "You Win!";
        explanationText.text = winningMessage;
        progressManagerScript.setUnlock(SceneManager.GetActiveScene().name);
        progressManagerScript.setHighScore(score, SceneManager.GetActiveScene().name);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        AudioManager.Instance.playSfx("Click");
        Debug.Log("Load Menu");
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Retry()
    {
        AudioManager.Instance.playSfx("Click");
        Debug.Log("Retry");
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

//Show score function is called from game manager Update() to load current score.
    public void showScore(int score)
    {
        FinalScore.text = "Score: " + score.ToString();
    }
}
