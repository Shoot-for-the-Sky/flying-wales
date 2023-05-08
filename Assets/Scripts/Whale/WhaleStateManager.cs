using System;
using UnityEngine;

public class WhaleStateManager : MonoBehaviour
{
    [SerializeField] public float whaleSpeed = 1f;
    [SerializeField] public float whaleRotateSpeed = 1f;
    WhaleBaseState currentState;
    public WhaleDynamicState DynamicState = new WhaleDynamicState();
    public WhaleTrackState TrackState = new WhaleTrackState();
    public WhaleAttackState AttackState = new WhaleAttackState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = DynamicState;
        currentState.EnterState(this);
        currentState.whaleSpeed = whaleSpeed;
    }

    void FixedUpdate()
    {
        currentState.UpdateState(this);
        UpdateWhalePositions();
        RotateWhaleByDegree();
        FlipWhaleByDirection();
        currentState.whaleSpeed = whaleSpeed;
        transform.position += Time.fixedDeltaTime * currentState.whaleSpeed * currentState.nextStepPosition;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter2D(this, collision);
    }

    public void ChangeStateByName(WhaleState state)
    {
        switch (state)
        {
            case (WhaleState.Dynamic):
                SwitchState(DynamicState);
                break;
            case (WhaleState.Track):
                SwitchState(TrackState);
                break;
            case (WhaleState.Attack):
                SwitchState(AttackState);
                break;
        }
    }

    public void SwitchState(WhaleBaseState state)
    {
        if (currentState != state)
        {
            currentState = state;
            state.EnterState(this);
        }
    }

    private void UpdateWhalePositions()
    {
        currentState.prevPostion = new Vector3(transform.position.x, transform.position.y, 0.0f);
        float nextX = currentState.prevPostion.x + currentState.nextStepPosition.x;
        float nextY = currentState.prevPostion.y + currentState.nextStepPosition.y;
        currentState.nextPostion = new Vector3(nextX, nextY, 0.0f);
    }

    private void RotateWhaleByDegree()
    {
        // make the whale to point is body to the direction he goes by degree
        // between prev and next position
        double y = currentState.nextPostion.y - currentState.prevPostion.y;
        double x = currentState.nextPostion.x - currentState.prevPostion.x;
        double radians = Math.Atan2(y, x);
        int degree = (int)(radians * (180 / Math.PI));
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, degree), Time.deltaTime * whaleRotateSpeed);
    }

    private void FlipWhaleByDirection() {
        // flip the whale Y axis direction if going right or left
        if (currentState.prevStepPosition != currentState.nextStepPosition)
        {
            float newScaleY;
            if (currentState.IsWhaleGoingRight())
            {
                newScaleY = 1;
            }
            else
            {
                newScaleY = -1;
            }
            transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
        }
    }
}
