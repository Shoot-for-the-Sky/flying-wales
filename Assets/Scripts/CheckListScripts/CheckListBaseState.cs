using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CheckListBaseState : MonoBehaviour
{
    public abstract void EnterState(CheckListManager checkListManager);

    public abstract void UpdateState(CheckListManager checkListManager);

    public abstract bool DoneState(CheckListManager checkListManager);

    public abstract void ChangeWhaleState(WhaleState whaleState);

    public abstract void LeftMouseButtonClicked();
}
