using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIntro4 : CheckListBaseState
{
    private bool doneState = false;

    public override void EnterState(CheckListManager checkListManager) {
        Debug.Log("Enter State CheckIntro4");
    }

    public override void UpdateState(CheckListManager checkListManager) {

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
