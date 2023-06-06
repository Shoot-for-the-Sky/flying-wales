using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckListManager : MonoBehaviour
{
    // Game Manager
    [SerializeField] public GameManager gameManagerScript;

    // User interface
    [SerializeField] Text currentCheckText;
    [SerializeField] Text nextCheckText;

    // Json file
    public TextAsset jsonFileCheckList;
    private CheckList checkListJson;

    // Checklist
    private int checkIndex = 0;
    private int checkListLength;

    // Task
    private List<Task> tasks = new List<Task>();
    private List<Task> currentLevelTasks = new List<Task>();
    private int taskLevel = 0;

    // Task Timer
    private const string timerStringFormat = "#.0#";
    private float levelTime;
    private float timer = .0f;
    private bool activeTimer = true;

    private IEnumerator RunTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.10f);
            timer += 0.10f;
            if (activeTimer)
            {
                nextCheckText.text = "Task Time: " + timer.ToString(timerStringFormat);
            }
            else
            {
                nextCheckText.text = "";
            }
            levelTime = timer;
        }
    }

    void Start()
    {
        checkListJson = JsonUtility.FromJson<CheckList>(jsonFileCheckList.text);
        checkListLength = checkListJson.checkList.Length;

        // Tasks
        for (int taskIndex = 0; taskIndex < checkListLength; taskIndex++)
        {        
            Task task = new Task(checkListJson.checkList[taskIndex]);
            tasks.Add(task);
        }
        SetCurrentLevelTasks();
        StartCoroutine(RunTimer());
        SetLevelTask();
    }

    private void FixedUpdate()
    {
        foreach (Task task in currentLevelTasks)
            task.FixedUpdate();

        bool doneLevelTasks = IsDoneLevelTasks();
        if (doneLevelTasks)
        {
            Debug.Log("Done level task: " + taskLevel);
            taskLevel++;
            SetCurrentLevelTasks();
            timer = .0f;
            SetLevelTask();
        }

        SetCurrentLevelTasksData();
    }

    private void SetCurrentLevelTasksData()
    {
        foreach (Task task in currentLevelTasks)
        {
            task.time = levelTime;
            task.currentState = gameManagerScript.currentWhalesState;
            task.currentScore = gameManagerScript.score;
        }
    }

    private void SetCurrentLevelTasks()
    {
        currentLevelTasks.Clear();
        foreach (Task task in tasks)
        {
            if (task.level == taskLevel)
            {
                currentLevelTasks.Add(task);
            }
        }
    }

    private bool IsDoneLevelTasks()
    {
        foreach (Task task in currentLevelTasks)
        {
            if (!task.IsDoneTask())
            {
                return false;
            }
        }
        return true;
    }

    private void SetLevelTask() {
        // set level to new UI
        string text = "";
        bool activeTimerNeeded = false;
        foreach (Task task in tasks)
        {
            if (task.level == taskLevel)
            {
                text += task.text + "\n";
                // Need timer in task
                if (task.time != 0)
                {
                    activeTimerNeeded = true;
                }

                // Need to create enemies in task
                if (task.createMeteors)
                {
                    gameManagerScript.createMeteors = true;
                }
            }
        }
        activeTimer = activeTimerNeeded;
        currentCheckText.text = text;
    }
}
