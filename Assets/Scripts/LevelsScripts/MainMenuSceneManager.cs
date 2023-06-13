using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    // Arrow
    public Texture2D cursorArrow;

    private bool intro_locked = false;
    private int intro_score;
    private bool sky_locked = true;
    private int sky_score;

    private bool space_locked = true;

    private int space_score;

    private int forest_score;

    private int ocean_score;
    [SerializeField] protected GameObject progressManager;

    private ProgressManager progressManagerScript;

    [SerializeField] protected Text introScoreText; 
    [SerializeField] protected Text skyScoreText; 

    [SerializeField] protected Text spaceScoreText; 

    [SerializeField] protected Text forestScoreText; 

    [SerializeField] protected Text oceanScoreText; 

    [SerializeField] GameObject levelsCanvas;

    [SerializeField] GameObject skyButton;

    [SerializeField] GameObject spaceButton;

    [SerializeField] Sprite locked_sky;

    [SerializeField] Sprite locked_space;

    void Start()
    {
        // Set default cursor
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);

        progressManagerScript = progressManager.GetComponent<ProgressManager>();
        // progressManagerScript.readProgress();
        Debug.Log("Start of MainMenuScene");
        readHighestScore();
        readUnlockedLevels();
        setLockedSprites();

    }
    
    private void readUnlockedLevels()
    {
        intro_locked = progressManagerScript.getLockedLevels("IntroLevelScene");
        sky_locked = progressManagerScript.getLockedLevels("SkyLevelScene");
        space_locked = progressManagerScript.getLockedLevels("SpaceLevelScene");

    }

    private void readHighestScore()
    {
        intro_score = progressManagerScript.getHighScore("IntroLevelScene");
        sky_score = progressManagerScript.getHighScore("SkyLevelScene");
        space_score = progressManagerScript.getHighScore("SpaceLevelScene");
        forest_score = progressManagerScript.getHighScore("SpaceLevelScene");
        ocean_score = progressManagerScript.getHighScore("OceanLevelScene");


        if(intro_score > 0) 
        {
            introScoreText.text = "High Score: "+intro_score;
        }
        else 
        {
            introScoreText.text = "";
        }


        if(sky_score > 0)
        { 
            skyScoreText.text = "High Score: "+sky_score;
        }
        else 
        {
            skyScoreText.text = "";
        }
        if(space_score > 0)
        { 
            spaceScoreText.text = "High Score: "+space_score;
        }
        else 
        {
            spaceScoreText.text = "";
        }

        if(forest_score > 0)
        { 
            forestScoreText.text = "High Score: "+forest_score;
        }
        else 
        {
            forestScoreText.text = "";
        }
        if(ocean_score > 0)
        { 
            oceanScoreText.text = "High Score: "+ocean_score;
        }
        else 
        {
            oceanScoreText.text = "";
        }
    }

    
    private void setLockedSprites()
    {
        if(sky_locked)
        {
            skyButton.GetComponent<Image>().sprite = locked_sky;
        }
        
        if(space_locked)
        {
            spaceButton.GetComponent<Image>().sprite = locked_space;
        }
        

    
    }
    public void PlayLevel(string sceneName)
    {   
        if(!progressManagerScript.getLockedLevels(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log(sceneName+" ,Level is locked");
        }
    }

    
}
