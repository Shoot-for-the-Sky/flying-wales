using System;
using UnityEngine;
using System.Collections;

public class WhaleStateManager : MonoBehaviour
{
    // Game Manager
    GameManager gameManagerScript;

    // Whale states
    WhaleBaseState currentState;
    public WhaleDynamicState DynamicState = new WhaleDynamicState();
    public WhaleTrackState TrackState = new WhaleTrackState();
    public WhaleAttackState AttackState = new WhaleAttackState();
    public WhaleState currentWhaleEnumState;

    // Whale speed params
    public float whaleSpeed = 1f;
    public float whaleRotateSpeed = 5f;

    // Whale other params
    [SerializeField] public float damagePoints;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        currentState = DynamicState;
        currentWhaleEnumState = WhaleState.Dynamic;
        currentState.whaleSpeed = whaleSpeed;
        currentState.EnterState(this);
        StartCoroutine(RandomScore());
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

    public void HitByEnemy(float damage)
    {
        Debug.Log("Hit By Enemy - damage: " + damage);
    }

    public void ChangeWhaleSpeed(float speed)
    {
        whaleSpeed = speed;
    }

    public void ChangeWhaleRotateSpeed(float speed)
    {
        whaleRotateSpeed = speed;
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
                currentWhaleEnumState = WhaleState.Dynamic;
                SwitchState(DynamicState);
                break;
            case (WhaleState.Track):
                currentWhaleEnumState = WhaleState.Track;
                SwitchState(TrackState);
                break;
            case (WhaleState.Attack):
                currentWhaleEnumState = WhaleState.Attack;
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

    public void LeftMouseButtonClicked()
    {
        currentState.LeftMouseButtonClicked();
    }

    private void UpdateWhalePositions()
    {
        currentState.prevPosition = new Vector3(transform.position.x, transform.position.y, 0.0f);
        float nextX = currentState.prevPosition.x + currentState.nextStepPosition.x;
        float nextY = currentState.prevPosition.y + currentState.nextStepPosition.y;
        currentState.nextPosition = new Vector3(nextX, nextY, 0.0f);
    }

    private void RotateWhaleByDegree()
    {
        // make the whale to point is body to the direction he goes by degree
        // between prev and next position
        double y = currentState.nextPosition.y - currentState.prevPosition.y;
        double x = currentState.nextPosition.x - currentState.prevPosition.x;
        double radians = Math.Atan2(y, x);
        int degree = (int)(radians * (180 / Math.PI));
        currentState.whaleDegree = degree;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, currentState.whaleDegree), Time.deltaTime * whaleRotateSpeed);
    }

    private void FlipWhaleByDirection()
    {
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

    private IEnumerator RandomScore()
    {
        while (true)
        {
            if (currentWhaleEnumState == WhaleState.Dynamic)
            {
                if (UtilFunctions.RollInPercentage(10))
                {
                    int scoreToAdd = UtilFunctions.GetRandomIntInRange(3, 6);
                    gameManagerScript.RandomWhaleTakeScore(scoreToAdd);
                    gameManagerScript.AddScore(scoreToAdd);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
