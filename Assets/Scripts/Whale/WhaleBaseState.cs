using UnityEngine;

public abstract class WhaleBaseState
{
    public abstract void EnterState(WhaleStateManager whale);

    public abstract void UpdateState(WhaleStateManager whale);

    public abstract void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision);
}
