using UnityEngine;

public class WhaleTrackState : WhaleBaseState
{
    public override void EnterState(WhaleStateManager whale)
    {
        Debug.Log("EnterState Track State");
    }

    public override void UpdateState(WhaleStateManager whale)
    {

    }

    public override void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision)
    {

    }
}
