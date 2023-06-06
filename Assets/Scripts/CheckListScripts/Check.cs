using System.Collections.Generic;
[System.Serializable]

// This class represents a data unit of a checklist
public class Check
{
    public string scenario;
    public int level;
    public string text;
    public string state;
    public int score;
    public int time;
    public int surviveMeteorsCount;
    public int destroyMeteorsCount;
    public int shieldPowersCount;
    public int callPowersCount;
    public bool createMeteors;
}
