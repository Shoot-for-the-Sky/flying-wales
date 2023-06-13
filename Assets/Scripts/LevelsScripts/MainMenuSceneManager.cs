using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    private bool intro_locked = false;
    private int intro_score;
    private bool sky_locked = true;
    private int sky_score;

    private bool space_locked = true;

    private int space_score;

    private int forest_score;

    private bool forest_locked = true;

    private int ocean_score;

    private bool ocean_locked = true;
    [SerializeField] protected GameObject progressManager;

    private ProgressManager progressManagerScript;

    [SerializeField] protected Text introScoreText; 
    [SerializeField] protected Text skyScoreText; 

    [SerializeField] protected Text spaceScoreText; 

    [SerializeField] protected Text forestScoreText; 

    [SerializeField] protected Text oceanScoreText; 

    [SerializeField] GameObject levelsCanvas;


    //Levels buttons
    [SerializeField] GameObject skyButton;

    [SerializeField] GameObject spaceButton;

    [SerializeField] GameObject forestButton;

    [SerializeField] GameObject oceanButton;


    //Locked sprites
    [SerializeField] Sprite locked_sky;

    [SerializeField] Sprite locked_space;

    [SerializeField] Sprite locked_forest;
    [SerializeField] Sprite locked_ocean;

    


    void Start()
    {
        progressManagerScript = progressManager.GetComponent<ProgressManager>();
        // progressManagerScript.readProgress();
        Debug.Log("Start of MainMenuScene");
        readHighestScore();
        readUnlockedLevels();
        setLockedSprites();
        AudioManager.Instance.playBgm("MainMenuTheme");

    }
    
    private void readUnlockedLevels()
    {
        intro_locked = progressManagerScript.getLockedLevels("IntroLevelScene");
        sky_locked = progressManagerScript.getLockedLevels("SkyLevelScene");
        space_locked = progressManagerScript.getLockedLevels("SpaceLevelScene");
        forest_locked = progressManagerScript.getLockedLevels("ForestLevelScene");
        ocean_locked = progressManagerScript.getLockedLevels("OceanLevelScene");

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
        if(forest_locked)
        {
            forestButton.GetComponent<Image>().sprite = locked_forest;
        }
        if(ocean_locked)
        {
            
            oceanButton.GetComponent<Image>().sprite = locked_ocean;
        }
        

    
    }
    public void PlayLevel(string sceneName)
    {   
        
        if(!progressManagerScript.getLockedLevels(sceneName))
        {   
            Debug.Log("Loading level: "+sceneName);
            AudioManager.Instance.playSfx("GameStart");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log(sceneName+" ,Level is locked");
        }
    }

    
}