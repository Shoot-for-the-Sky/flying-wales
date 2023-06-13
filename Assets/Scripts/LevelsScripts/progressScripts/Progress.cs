using System.Collections.Generic;
[System.Serializable]

// This class represents the progress of a certail scenario
public class Progress
{
    public int scenario;
    
    public string scenarioName;

    public bool isLocked;

    public int highscore;

    public Progress(Progress progress)
    {
        scenario = progress.scenario;
        scenarioName = progress.scenarioName;
        isLocked = progress.isLocked;
        highscore = progress.highscore;
    }
}
