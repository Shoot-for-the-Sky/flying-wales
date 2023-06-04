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
    public TextAsset jsonFilecheckList;
    private CheckList checkListJson;

    // Checklist
    private int checkIndex = 0;
    private int checkListLenth;

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
        while (activeTimer)
        {
            yield return new WaitForSeconds(0.10f);
            timer += 0.10f;
            nextCheckText.text = "Task Time: " + timer.ToString(timerStringFormat);
            levelTime = timer;
        }
    }

    void Start()
    {
        checkListJson = JsonUtility.FromJson<CheckList>(jsonFilecheckList.text);
        checkListLenth = checkListJson.checkList.Length;
        Debug.Log("checkListLenth: " + checkListLenth);

        // Tasks
        for (int taskIndex = 0; taskIndex < checkListLenth; taskIndex++)
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
            StartCoroutine(RunTimer());
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
            task.currentPoints = gameManagerScript.points;
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
        foreach (Task task in tasks)
        {
            if (task.level == taskLevel) {
                text += task.text;
            }
        }
        currentCheckText.text = text;
    }
}
