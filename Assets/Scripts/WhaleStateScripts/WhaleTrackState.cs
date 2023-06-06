using UnityEngine;
using UnityEngine.UIElements;

public class WhaleTrackState : WhaleBaseState
{
    // Get mouse position by camera
    private Camera mainCamera;
    private Vector3 mousePosition;

    // Determinate if whale is close to mouse by epslions
    private const float epsilonX = 4f;
    private const float epsilonY = 2f;

    // Close to mouse steps of whale for each X axis side
    private int step = 0;
    private const int minMumberOfSteps = 180;
    private const int maxMumberOfSteps = 200;
    private int numberOfSteps;
    private bool goToMouse = false;

    // Delta params
    private readonly float rotateDeltaDistance = 2f;
    private readonly float destinationTrackYDelta = 0.02f;
    private readonly float delteaYDistance = 30f;

    // Speed params
    private readonly float whaleTrackRotateSpeed = 5f;
    private readonly float whaleTrackSpeed = 1f;

    // The range around the mouse track destination
    private float destinationRangeDelta = 1f;
    private float destinationXDelta = 0f;
    private float destinationYDelta = 0f;

    public override void EnterState(WhaleStateManager whale)
    {
        mainCamera = Camera.main;
        numberOfSteps = Random.Range(minMumberOfSteps, maxMumberOfSteps);
    }

    public override void UpdateState(WhaleStateManager whale)
    {
        // Update track speed
        whale.whaleRotateSpeed = whaleTrackRotateSpeed;
        whale.whaleSpeed = whaleTrackSpeed;

        // Positions
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 currentPosition = whale.transform.position;
        Vector3 destination;
        float stepX;
        float stepY;

        bool isWhaleCloseToMouse = UtilFunctions.IsPointsClose2D(mousePosition, whale.transform.position, epsilonX, epsilonY);

        if (!isWhaleCloseToMouse || goToMouse)
        {
            // track mouse from distance
            destination = new Vector3(mousePosition.x + destinationXDelta, mousePosition.y + destinationYDelta, mousePosition.z);
        }
        else
        {
            // Moving around the mouse
            float destinationX;
            float destinationY;

            // Half of steps go to same direction
            if (step < (numberOfSteps * 0.5))
            {
                destinationX = mousePosition.x + rotateDeltaDistance;
            }
            else
            {
                destinationX = mousePosition.x - rotateDeltaDistance;
            }
            destinationY = currentPosition.y - destinationTrackYDelta;
            destination = new Vector3(destinationX, destinationY, 0);
        }

        // Next step toward the track destination
        Vector3 nextStep = UtilFunctions.GetNextStepByDestinationPoint2D(currentPosition, destination, 1);
        stepX = nextStep.x;
        stepY = nextStep.y;

        // step counter for move around mouse
        if (step >= numberOfSteps)
        {
            step = 0;
            // stepY += UtilFunctions.GetRandomDoubleInRange(-delteaYDistance, delteaYDistance);
            goToMouse = false;
        }
        step++;
        nextStepPosition = new Vector3(stepX, stepY, 0);
    }

    public override void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision)
    {

    }

    public override void OnTriggerEnter2D(WhaleStateManager whale, Collider2D collision)
    {
        if (collision.gameObject.tag == "MeteorBody")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            whale.HitByEnemy(enemy.hitPoints);
        }
    }

    public override void LeftMouseButtonClicked()
    {
        // Make whale go to mouse
        if (!goToMouse)
        {
            goToMouse = true;
            step = (int)(numberOfSteps * 0.75);
            destinationXDelta = UtilFunctions.GetRandomDoubleInRange(-destinationRangeDelta, destinationRangeDelta);
            destinationYDelta = UtilFunctions.GetRandomDoubleInRange(-destinationRangeDelta, destinationRangeDelta);
        }
    }
}
