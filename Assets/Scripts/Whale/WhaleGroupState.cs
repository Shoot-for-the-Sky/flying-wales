using UnityEngine;

public class WhaleGroupState : WhaleBaseState
{
    public override void EnterState(WhaleStateManager whale)
    {
        Debug.Log("EnterState Group State");
    }

    public override void UpdateState(WhaleStateManager whale)
    {

    }

    public override void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision)
    {

    }
}
