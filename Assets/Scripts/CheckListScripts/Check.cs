using System.Collections.Generic;
[System.Serializable]

// This class represents a data unit of a checklist
public class Check
{
    // Scenario name
    public string scenario;

    // Number of task
    public int level;

    // Checklist task text
    public string text;

    // Required state
    public string state;

    // Required score
    public int score;

    // Required minimum time for task
    public int time;

    // Number of required times needed to survive meteors
    public int surviveMeteorsCount;

    // Number of required times needed to destroy meteors
    public int destroyMeteorsCount;

    // Number of required times to use shield powers
    public int shieldPowersCount;

    // Number of required times to use call powers
    public int callPowersCount;

    // Meteors created
    public bool canCreateMeteors;

    // Gather score in Dynamic state and other Operations
    public bool canGatherScore;

    // Time pass only when in required state
    public bool requiredStateForTime;

    // Disable use of shield powers
    public bool disableShieldPower;

    // Disable use of call whale powers
    public bool disableCallPower;
}
