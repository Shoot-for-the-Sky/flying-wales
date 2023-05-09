using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIntro1 : CheckListBaseState
{
    private bool doneState = false;

    public override void EnterState(CheckListManager checkListManager) {
        Debug.Log("Enter State CheckIntro1");
    }

    public override void UpdateState(CheckListManager checkListManager) {

    }

    public override bool DoneState(CheckListManager checkListManager)
    {
        return doneState;
    }

    public override void ChangeWhaleState(WhaleState whaleState)
    {
        Debug.Log("ChangeWhaleState " + WhaleState.Track);
        if (whaleState == WhaleState.Track)
        {
            doneState = true;
        }
    }

    public override void LeftMouseButtonClicked()
    {

    }
}
