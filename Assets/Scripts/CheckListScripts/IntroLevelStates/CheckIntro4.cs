using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIntro4 : CheckListBaseState
{
    [SerializeField] public GameManager gameManagerScript;
    private bool doneState = false;
    private int numberOfMeteorPass = 0;

    public override void EnterState(CheckListManager checkListManager)
    {
        Debug.Log("Enter State CheckIntro4");
        Debug.Log("numberOfMeteorPass: " + numberOfMeteorPass);
        gameManagerScript.createMeteors = true;
        numberOfMeteorPass = gameManagerScript.numberOfMeteorPass;
    }

    public override void UpdateState(CheckListManager checkListManager)
    {
        numberOfMeteorPass = gameManagerScript.numberOfMeteorPass;
        Debug.Log("numberOfMeteorPass: " + numberOfMeteorPass);
        if (numberOfMeteorPass >= 3)
        {
            doneState = true;
        }
    }

    public override bool DoneState(CheckListManager checkListManager)
    {
        // implement

        return doneState;
    }

    public override void ChangeWhaleState(WhaleState whaleState)
    {

    }

    public override void LeftMouseButtonClicked()
    {

    }
}
