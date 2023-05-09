using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIntro3 : CheckListBaseState
{
    private bool doneState = false;

    public override void EnterState(CheckListManager checkListManager) {
        Debug.Log("Enter State CheckIntro3");
    }

    public override void UpdateState(CheckListManager checkListManager) {

    }

    public override bool DoneState(CheckListManager checkListManager)
    {
        return doneState;
    }

    public override void ChangeWhaleState(WhaleState whaleState)
    {
        if (whaleState == WhaleState.Dynamic)
        {
            doneState = true;
        }
    }

    public override void LeftMouseButtonClicked()
    {

    }
}
