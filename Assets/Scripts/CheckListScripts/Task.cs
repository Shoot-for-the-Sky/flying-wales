using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represent the behaviour of a check task in a checklist and check if the current task is executed
public class Task
{
    // Game Manager
    GameManager gameManagerScript;

    // General
    private bool doneTask = false;
    public float time;
    public WhaleState currentState;
    public int currentScore;
    public bool canCreateMeteors;
    public bool canCreateAliens;
    public float createMeteorEachSec;
    public float createAlienEachSec;
    public bool canGatherScore;
    public bool requiredStateForTime;
    public bool disableShieldPower;
    public bool disableCallPower;

    // Data
    public int level;
    public string text;
    public string title;
    private Check currentCheck;
    WhaleState wantedState;

    // Constructor
    public Task(Check check)
    {
        currentCheck = check;
        level = currentCheck.level;
        text = currentCheck.text;
        title = currentCheck.title;
        canCreateMeteors = check.canCreateMeteors;
        canCreateAliens = check.canCreateAliens;
        createMeteorEachSec = check.createMeteorEachSec;
        createAlienEachSec = check.createAlienEachSec;
        canGatherScore = check.canGatherScore;
        requiredStateForTime = check.requiredStateForTime;
        time = check.time;
        disableShieldPower = check.disableShieldPower;
        disableCallPower = check.disableCallPower;
        wantedState = GetWhaleStateByName(currentCheck.state);
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    public void FixedUpdate()
    {
        bool inWantedState = IsWhalesInWantedState();
        bool isCollectedScore = IsCollectedWantedScore();
        bool isPassTime = IsPassWantedTime();

        // survive
        bool isSurviveMeteors = IsSurviveIfNeededMeteors();
        bool isSurviveAliens = IsSurviveIfNeededAliens();

        // destroy
        bool isDestroyMeteors = IsDestroyIfNeededMeteors();
        bool isDestroyAliens = IsDestroyIfNeededAliens();

        // powers
        bool isShieldPowers = IsShieldPowersIfNeeded();
        bool isCallPowers = IsCallPowersIfNeeded();
        // if (!inWantedState) {
        //     Debug.Log("Unfilled: inWantedState");
        // }
        // if (!isCollectedScore) {
        //     Debug.Log("Unfilled: isCollectedScore");
        // }
        // if (!isPassTime) {
        //     Debug.Log("Unfilled: isPassTime");
        // }
        // if (!isSurvive) {
        //     Debug.Log("Unfilled: isSurvive");
        // }
        // if (!isDestroy) {
        //     Debug.Log("Unfilled: isDestroy");
        // }
        // if (!isShieldPowers) {
        //     Debug.Log("Unfilled: isShieldPowers");
        // }
        // if (!isCallPowers) {
        //     Debug.Log("Unfilled: isCallPowers");
        // }
        bool isSurvive = isSurviveMeteors && isSurviveAliens;
        bool isDestroy = isDestroyMeteors && isDestroyAliens;
        doneTask = inWantedState && isCollectedScore && isPassTime && isSurvive && isDestroy && isShieldPowers && isCallPowers;
    }

    public bool IsDoneTask()
    {
        return doneTask;
    }

    private bool IsWhalesInWantedState()
    {
        bool inWantedState = false;
        if (currentCheck.state != null && currentCheck.state != "")
        {
            inWantedState = wantedState == currentState;
        }

        // State is not required
        else
        {
            inWantedState = true;
        }
        return inWantedState;
    }

    private bool IsCollectedWantedScore()
    {
        bool isCollectedWantedScore = false;
        if (currentCheck.score != 0)
        {
            isCollectedWantedScore = currentScore >= currentCheck.score;
        }

        // Collected points not required
        else
        {
            isCollectedWantedScore = true;
        }
        return isCollectedWantedScore;
    }

    private bool IsPassWantedTime()
    {
        bool isPassWantedTime = false;
        if (currentCheck.time != 0)
        {
            isPassWantedTime = time >= currentCheck.time;
        }

        // Pass time not required
        else
        {
            isPassWantedTime = true;
        }
        return isPassWantedTime;
    }

    private bool IsSurviveIfNeededMeteors()
    {
        bool isSurviveIfNeeded = false;
        if (currentCheck.surviveMeteorsCount != 0)
        {
            if (gameManagerScript.IsFilledSurvivedEnemies("Meteor", currentCheck.surviveMeteorsCount))
            {
                isSurviveIfNeeded = true;
            }
        }

        // Survive enemies not required
        else
        {
            isSurviveIfNeeded = true;
        }
        return isSurviveIfNeeded;
    }

    private bool IsSurviveIfNeededAliens()
    {
        bool isSurviveIfNeeded = false;
        if (currentCheck.surviveAliensCount != 0)
        {
            if (gameManagerScript.IsFilledSurvivedEnemies("Alien", currentCheck.surviveAliensCount))
            {
                isSurviveIfNeeded = true;
            }
        }

        // Survive enemies not required
        else
        {
            isSurviveIfNeeded = true;
        }
        return isSurviveIfNeeded;
    }

    private bool IsDestroyIfNeededMeteors()
    {
        bool isDestroyIfNeeded = false;
        if (currentCheck.destroyMeteorsCount != 0)
        {
            if (gameManagerScript.IsFilledDestroyedEnemies("Meteor", currentCheck.destroyMeteorsCount))
            {
                isDestroyIfNeeded = true;
            }
        }
        // Destroy enemies not required
        else
        {
            isDestroyIfNeeded = true;
        }
        return isDestroyIfNeeded;
    }

    private bool IsDestroyIfNeededAliens()
    {
        bool isDestroyIfNeeded = false;
        if (currentCheck.destroyAliensCount != 0)
        {
            if (gameManagerScript.IsFilledDestroyedEnemies("Alien", currentCheck.destroyAliensCount))
            {
                isDestroyIfNeeded = true;
            }
        }
        // Destroy enemies not required
        else
        {
            isDestroyIfNeeded = true;
        }
        return isDestroyIfNeeded;
    }

    private bool IsShieldPowersIfNeeded()
    {
        bool isShieldPowersIfNeeded = false;
        if (currentCheck.shieldPowersCount != 0)
        {
            if (gameManagerScript.IsFilledPlayerPowers("Shield", currentCheck.shieldPowersCount))
            {
                isShieldPowersIfNeeded = true;
            }
        }
        // Player powers not required
        else
        {
            isShieldPowersIfNeeded = true;
        }
        return isShieldPowersIfNeeded;
    }

    private bool IsCallPowersIfNeeded()
    {
        bool isCallPowersIfNeeded = false;
        if (currentCheck.callPowersCount != 0)
        {
            if (gameManagerScript.IsFilledPlayerPowers("Call", currentCheck.callPowersCount))
            {
                isCallPowersIfNeeded = true;
            }
        }
        // Player powers not required
        else
        {
            isCallPowersIfNeeded = true;
        }
        return isCallPowersIfNeeded;
    }

    private WhaleState GetWhaleStateByName(string stateName)
    {
        switch (stateName)
        {
            case ("track"):
                return WhaleState.Track;
            case ("attack"):
                return WhaleState.Attack;
            default:
                return WhaleState.Dynamic;
        }
    }

    public bool inRequiredStateForTimer()
    {
        if (requiredStateForTime)
        {
            return IsWhalesInWantedState();
        }
        return true;
    }
}