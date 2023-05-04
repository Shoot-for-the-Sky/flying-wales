using UnityEngine;

public class WhaleAttackState : WhaleBaseState
{
    public override void EnterState(WhaleStateManager whale) {
        Debug.Log("EnterState Attack State");
    }

    public override void UpdateState(WhaleStateManager whale)
    {

    }

    public override void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision)
    {

    }
}
