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
    [SerializeField] Text taskCounterText;
    [SerializeField] SpriteRenderer shieldPowerIcon;
    [SerializeField] SpriteRenderer callPowerIcon;
    Color powerIconRegularColor;
    Color powerIconDisableColor;

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
            if (activeTimer)
            {
                if (inRequiredStateForTimer())
                {
                    timer += 0.10f;
                    nextCheckText.text = "Task Time: " + timer.ToString(timerStringFormat);
                }
            }
            else
            {
                nextCheckText.text = "";
            }
            levelTime = timer;
        }
    }

    private bool inRequiredStateForTimer()
    {
        foreach (Task task in tasks)
        {
            if (task.level == taskLevel)
            {
                if (task.inRequiredStateForTimer())
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Start()
    {
        // Power icons colors
        powerIconRegularColor = new Color(1f, 1f, 1f);
        powerIconDisableColor = new Color(1f, 1f, 1f, 0.1f);


        // Json
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
        SetTaskCounterText();
        DisablePowersIfNeeded();
    }

    private void DisablePowersIfNeeded()
    {
        bool needToDisableShield = false;
        bool needToDisableCall = false;
        foreach (Task task in tasks)
        {
            if (task.level == taskLevel)
            {
                if (task.disableShieldPower)
                {
                    needToDisableShield = true;
                }
                if (task.disableCallPower)
                {
                    needToDisableCall = true;
                }
            }
        }

        // Disable shield
        if (needToDisableShield)
        {
            shieldPowerIcon.color = powerIconDisableColor;
            gameManagerScript.isShieldDisableToUse = true;
        }

        // Enable shield
        else
        {
            shieldPowerIcon.color = powerIconRegularColor;
            gameManagerScript.isShieldDisableToUse = false;
        }

        // Disable call
        if (needToDisableCall)
        {
            callPowerIcon.color = powerIconDisableColor;
            gameManagerScript.isCallDisableToUse = true;
        }

        // Enable call
        else
        {
            callPowerIcon.color = powerIconRegularColor;
            gameManagerScript.isCallDisableToUse = false;
        }
        
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
            DisablePowersIfNeeded();
        }

        SetCurrentLevelTasksData();
        SetTaskCounterText();
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
                if (task.canCreateMeteors)
                {
                    gameManagerScript.canCreateMeteors = true;
                    gameManagerScript.createMeteorEachSec = task.createMeteorEachSec;
                }

                if (task.canGatherScore)
                {
                    gameManagerScript.canGatherScore = true;
                }
                if (task.requiredStateForTime)
                {
                    gameManagerScript.requiredStateForTime = true;
                }
            }
        }
        activeTimer = activeTimerNeeded;
        currentCheckText.text = text;
    }

    private void SetTaskCounterText()
    {
        taskCounterText.text = (taskLevel + 1).ToString() + "/" + checkListLength.ToString();
    }
}
