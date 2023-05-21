using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CheckListManager : MonoBehaviour
{
    [SerializeField] InputAction dynamicStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction trackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction attackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction LeftMouseButton = new InputAction(type: InputActionType.Button);
    public bool isWhaleStateControllerDisabled = false;

    CheckListBaseState currentState;
    [SerializeField] CheckListBaseState[] checkListStates;
    
    [SerializeField] Text currentCheckText;
    [SerializeField] Text nextCheckText;
    public TextAsset jsonFilecheckList;
    private CheckList checkListJson;
    private int checkIndex = 0;
    private int checkListLenth;

    void OnEnable()
    {
        dynamicStateButton.Enable();
        trackStateButton.Enable();
        attackStateButton.Enable();
        LeftMouseButton.Enable();
    }

    void OnDisable()
    {
        dynamicStateButton.Disable();
        trackStateButton.Disable();
        attackStateButton.Disable();
        LeftMouseButton.Disable();
    }

    void Start()
    {
        checkListJson = JsonUtility.FromJson<CheckList>(jsonFilecheckList.text);
        checkListLenth = checkListJson.checkList.Length;
        SetCheckText(currentCheckText, 0);
        SetCheckText(nextCheckText, 1);
        currentState = checkListStates[checkIndex];
        currentState.EnterState(this);
    }

    private void Update()
    {
        CheckInput();
        currentState.UpdateState(this);
        if(currentState.DoneState(this))
        {
            checkIndex++;
            Debug.Log("Going to next state " + checkIndex);
            currentState = checkListStates[checkIndex];
            NextCheck();
            currentState.EnterState(this);
        }
    }

    public void CheckInput() {
        if (isWhaleStateControllerDisabled)
        {
            return;
        }
        if (dynamicStateButton.WasPressedThisFrame())
        {
            currentState.ChangeWhaleState(WhaleState.Dynamic);
        }
        else if (trackStateButton.WasPressedThisFrame())
        {
            currentState.ChangeWhaleState(WhaleState.Track);
        }
        else if (attackStateButton.WasPressedThisFrame())
        {
            currentState.ChangeWhaleState(WhaleState.Attack);
        }
        else if (LeftMouseButton.WasPerformedThisFrame())
        {
            currentState.LeftMouseButtonClicked();
        }
    }

    private void SetCheckText(Text check, int index)
    {
        check.text = checkListJson.checkList[index].text;
    }

    private void NextCheck()
    {
        // current check
        if (checkIndex < checkListLenth)
        {
            SetCheckText(currentCheckText, checkIndex);
        }
        else
        {
            Debug.Log("Done Check List");
        }

        // next check
        if (checkIndex + 1 < checkListLenth)
        {
            SetCheckText(nextCheckText, checkIndex + 1);
        }
        else
        {
            nextCheckText.text = "";
        }
    }
}
