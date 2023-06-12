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

    // Start is called before the first frame update

    // Update is called once per frame

    
    public void gameOverScreen()
    {
        Debug.Log("Game Over screen called");
        explanationText.text = losingMessage;
        gameOverText.text = "Game Over!";
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void winningScreen()
    {
        Debug.Log("Winning screen called");
        gameOverText.text = "You Win!";
        explanationText.text = winningMessage;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        Debug.Log("Load Menu");
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Retry()
    {
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
