using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ProgressManager : MonoBehaviour
{
    public TextAsset progressJsonFile;

    private ProgressList progressListJson;

    private static List<Progress> progress_list = new List<Progress>();


    // Start is called before the first frame update
    void Start()
    {
        progressListJson = JsonUtility.FromJson<ProgressList>(progressJsonFile.text);
        int length = progressListJson.progressList.Length;
        for(int i = 0; i < length; i++)
        {
            Progress progress = new Progress(progressListJson.progressList[i]);
            progress_list.Add(progress);
        }
    }

    public bool getLockedLevels(string level)
    {
        foreach (Progress progress in progress_list)
        {
            if (progress.scenarioName.Equals(level))
            {
                return progress.isLocked;
            }
        }
        return false;
    }

    public int getHighScore(string level)
    {
        foreach (Progress progress in progress_list)
        {
            if (progress.scenarioName.Equals(level))
            {
                return progress.highscore;
            }
        }
        return 0;
    }

    public void setUnlock(string level)
    {
        foreach (Progress progress in progress_list)
        {
            if (progress.scenarioName.Equals(level))
            {
                int nextLevelIndex = progress_list.IndexOf(progress) + 1;
                if(nextLevelIndex > progress_list.Count)
                {
                    return;
                }
                progress_list[nextLevelIndex].isLocked = false;
            }
        }
    }

    public void setHighScore(int score, string level)
    {
        foreach (Progress progress in progress_list)
        {
            if (progress.scenarioName.Equals(level))
            {
                if(score > progress.highscore)
                {
                    progress.highscore = score;
                }
            }
        }
    }

    

    
}
