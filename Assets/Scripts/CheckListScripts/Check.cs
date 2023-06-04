using System.Collections.Generic;
[System.Serializable]

// This class represents a data unit of a checklist
public class Check
{
    public string scenario;
    public int level;
    public string text;
    public string state;
    public int points;
    public int time;
    public Dictionary<string, int> survive = new Dictionary<string, int>();
    public Dictionary<string, int> destroy = new Dictionary<string, int>();
    public Dictionary<string, int> powers = new Dictionary<string, int>();
}
