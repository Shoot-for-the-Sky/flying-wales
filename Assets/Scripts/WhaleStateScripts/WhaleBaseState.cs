using UnityEngine;

public abstract class WhaleBaseState
{
    public float whaleSpeed;
    public float whaleDegree = .0f;
    public Vector3 nextStepPosition;
    public Vector3 prevStepPosition;
    public Vector3 nextPosition;
    public Vector3 prevPosition;

    public abstract void EnterState(WhaleStateManager whale);

    public abstract void UpdateState(WhaleStateManager whale);

    public abstract void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision);

    public abstract void OnTriggerEnter2D(WhaleStateManager whale, Collider2D collision);

    public abstract void LeftMouseButtonClicked();

    public bool IsWhaleGoingUp()
    {
        return prevPosition.y < nextPosition.y;
    }

    public bool IsWhaleGoingRight()
    {
        return prevPosition.x < nextPosition.x;
    }
}
