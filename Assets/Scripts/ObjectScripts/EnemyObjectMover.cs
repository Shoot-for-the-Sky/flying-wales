using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectMover : MonoBehaviour
{
    // Speed
    [SerializeField] protected float minSpeed = 1.0f;
    [SerializeField] protected float maxSpeed = 3.0f;

    // Dead zones
    [SerializeField] protected float minDeadAxisXZone = -30.0f;
    [SerializeField] protected float maxDeadAxisXZone = 30.0f;
    [SerializeField] protected float minDeadAxisYZone = -10.0f;
    [SerializeField] protected float maxDeadAxisYZone = 10.0f;

    // Directions
    public Direction moveDirection;
    [SerializeField] protected float rangeRandomDirectionDelta = 0.5f;
    private float randomDirectionInNotMovingAxis;

    void Start() {
        moveDirection = GetRandomDirection();
        ChangePositionByDirection();
        randomDirectionInNotMovingAxis = UtilFunctions.GetRandomDoubleInRange(-rangeRandomDirectionDelta, rangeRandomDirectionDelta);
    }

    void Update()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        Vector3 direction = GetDirection();
        transform.position = transform.position + (direction * speed) * Time.deltaTime;
        if (outOfBorders())
        {
            Destroy(gameObject);
        }
    }

    private bool outOfBorders()
    {
        return transform.position.x > maxDeadAxisXZone || transform.position.x < minDeadAxisXZone
            || transform.position.y > maxDeadAxisYZone || transform.position.y < minDeadAxisYZone;
    }

    private Vector3 GetDirection()
    {
        Vector3 direction;
        switch (moveDirection)
        {
            case (Direction.Up):
                direction = Vector3.up;
                direction.x = randomDirectionInNotMovingAxis;
                break;
            case (Direction.Down):
                direction = Vector3.down;
                direction.x = randomDirectionInNotMovingAxis;
                break;
            case (Direction.Right):
                direction = Vector3.right;
                direction.y = randomDirectionInNotMovingAxis;
                break;
            case (Direction.Left):
                direction = Vector3.left;
                direction.y = randomDirectionInNotMovingAxis;
                break;
            default:
                direction = Vector3.one;
                break;
        }
        return direction;
    }

    private void ChangePositionByDirection()
    {
        float startPositionX = 0f;
        float startPositionY = 0f;
        switch (moveDirection)
        {
            case (Direction.Up):
                startPositionY = -10f;
                break;
            case (Direction.Down):
                startPositionY = 10f;
                break;
            case (Direction.Right):
                startPositionX = -15f;
                break;
            case (Direction.Left):
                startPositionX = 15f;
                break;
        }
        transform.position = new Vector3(startPositionX, startPositionY, 0);
    }

    private Direction GetRandomDirection()
    {
        System.Array values = System.Enum.GetValues(typeof(Direction));
        System.Random random = new System.Random();
        Direction randomDirection = (Direction)values.GetValue(random.Next(values.Length));
        return randomDirection;
    }
}
