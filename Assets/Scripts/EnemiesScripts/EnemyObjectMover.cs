using UnityEngine;

public class EnemyObjectMover : MonoBehaviour
{
    // Game Manager
    GameManager gameManagerScript;

    // Speed
    [SerializeField] protected float minSpeed;
    [SerializeField] protected float maxSpeed;

    // Rotation
    [SerializeField] public float minRotationSpeed;
    [SerializeField] public float maxRotationSpeed;
    private float rotationSpeed;

    // Directions
    public Direction moveDirection;
    [SerializeField] protected float rangeRandomDirectionDelta = 0.5f;
    private float randomDirectionInNotMovingAxis;

    void Start()
    {
        // Game Object
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();

        // Random direction
        moveDirection = GetRandomDirection();
        ChangePositionByDirection();
        randomDirectionInNotMovingAxis = UtilFunctions.GetRandomDoubleInRange(-rangeRandomDirectionDelta, rangeRandomDirectionDelta);

        // Random rotation speed
        rotationSpeed = UtilFunctions.GetRandomDoubleInRange(minRotationSpeed, maxRotationSpeed);
    }

    void FixedUpdate()
    {
        // Moving
        float speed = Random.Range(minSpeed, maxSpeed);
        Vector3 direction = GetDirection();
        transform.position = transform.position + (direction * speed) * Time.fixedDeltaTime;

        // Rotation
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
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
