using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleStateManager : MonoBehaviour
{
    WhaleBaseState currentState;
    public WhaleDynamicState DynamicState = new WhaleDynamicState();
    public WhaleGroupState GroupState = new WhaleGroupState();
    public WhaleTrackState TrackState = new WhaleTrackState();
    public WhaleAttackState AttackState = new WhaleAttackState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = DynamicState;
        currentState.EnterState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

    }

    public void ChangeStateByName(WhaleState state)
    {
        switch (state)
        {
            case (WhaleState.Dynamic):
                SwitchState(DynamicState);
                break;
           case (WhaleState.Group):
                SwitchState(GroupState);
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
}
