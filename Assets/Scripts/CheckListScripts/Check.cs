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
    //Checklist task title
    public string title;

    // Required state
    public string state;

    // Required score
    public int score;

    // Required minimum time for task
    public int time;

    // Number of required times needed to survive meteors
    public int surviveMeteorsCount;
    // Number of required times needed to survive aliens
    public int surviveAliensCount;

    // Number of required times needed to destroy meteors
    public int destroyMeteorsCount;
    // Number of required times needed to destroy aliens
    public int destroyAliensCount;

    // Number of required times to use shield powers
    public int shieldPowersCount;

    // Number of required times to use call powers
    public int callPowersCount;

    // Create meteors
    public bool canCreateMeteors;

    // Create aliens
    public bool canCreateAliens;

    // Create meteor each time second given
    public float createMeteorEachSec;

    // Create alien each time second given
    public float createAlienEachSec;

    // Gather score in Dynamic state and other Operations
    public bool canGatherScore;

    // Time pass only when in required state
    public bool requiredStateForTime;

    // Disable use of shield powers
    public bool disableShieldPower;

    // Disable use of call whale powers
    public bool disableCallPower;
}
