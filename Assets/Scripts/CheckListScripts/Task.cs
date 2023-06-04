using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represent the behaviour of a check task in a checklist and check if the current task is executed
public class Task
{
    // General
    private bool doneTask = false;
    public float time;
    public WhaleState currentState;
    public int currentPoints;

    // Data
    public int level;
    public string text;
    private Check currentCheck;
    WhaleState wantedState;
    
    // Constructor
    public Task(Check check)
    {
        currentCheck = check;
        level = currentCheck.level;
        text = currentCheck.text;
        wantedState = GetWhaleStateByName(currentCheck.state);
    }

    public void FixedUpdate()
    {
        bool inWantedState = IsWhalesInWantedState();
        bool inWantedPoints = IsCollectedWantedPoints();
        bool inWantedTime = IsPassWantedTime();
        doneTask = inWantedState && inWantedPoints && inWantedTime;
    }

    public bool IsDoneTask()
    {
        return doneTask;
    }

    private bool IsWhalesInWantedState()
    {
        bool inWantedState = false;
        if (currentCheck.state != null)
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

    private bool IsCollectedWantedPoints()
    {
        bool isCollectedWantedPoints = false;
        if (currentCheck.points != -1)
        {
            isCollectedWantedPoints = currentCheck.points >= currentPoints;
        }

        // Collected points not required
        else
        {
            isCollectedWantedPoints = true;
        }
        return isCollectedWantedPoints;
    }

    private bool IsPassWantedTime()
    {
        bool isPassWantedTime = false;
        if (currentCheck.time != -1)
        {
            isPassWantedTime = time >= currentCheck.time;
        }
        
        // Pass time not required
        else {
            isPassWantedTime = true;
        }
        return isPassWantedTime;
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
}