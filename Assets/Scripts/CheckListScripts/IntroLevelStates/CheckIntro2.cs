using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIntro2 : CheckListBaseState
{
    private bool whaleInAttackState = false;
    private bool wasAttacked = false;

    public override void EnterState(CheckListManager checkListManager) {
        Debug.Log("Enter State CheckIntro2");
    }

    public override void UpdateState(CheckListManager checkListManager) {

    }

    public override bool DoneState(CheckListManager checkListManager)
    {
        return whaleInAttackState && wasAttacked;
    }

    public override void ChangeWhaleState(WhaleState whaleState)
    {
        if (whaleState == WhaleState.Attack)
        {
            whaleInAttackState = true;
        }
    }

    public override void LeftMouseButtonClicked()
    {
        if (whaleInAttackState)
        {
            wasAttacked = true;
        }
    }
}
