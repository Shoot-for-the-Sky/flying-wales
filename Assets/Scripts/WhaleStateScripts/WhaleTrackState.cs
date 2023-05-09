using UnityEngine;
using UnityEngine.UIElements;

public class WhaleTrackState : WhaleBaseState
{
    private const float epsilon = 4f;
    private Camera mainCamera;
    private Vector3 mousePosition;
    private float rotateDeltaDistance = 2f;
    private int step = 0;
    private const int numberOfSteps = 200;
    private float destinationYDelta = 0.02f;
    private float whaleTrackRotateSpeed = 5f;
    private float whaleTrackSpeed = 1f;

    public override void EnterState(WhaleStateManager whale)
    {
        Debug.Log("EnterState Track State");
        mainCamera = Camera.main;
    }

    public override void UpdateState(WhaleStateManager whale)
    {
        whale.whaleRotateSpeed = whaleTrackRotateSpeed;
        whale.whaleSpeed = whaleTrackSpeed;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 currentPosition = whale.transform.position;
        Vector3 destination;
        float stepX;
        float stepY;

        bool isWhaleCloseToMouse = UtilFunctions.IsPointsClose2D(mousePosition, whale.transform.position, epsilon);

        if (!isWhaleCloseToMouse)
        {
            // track mouse from distance
            destination = new Vector3(mousePosition.x, mousePosition.y, mousePosition.z);
        }
        else
        {
            // moving around the mouse
            float destinationX;
            float destinationY;

            if (step < (numberOfSteps * 0.5))
            {
                destinationX = mousePosition.x + rotateDeltaDistance;
            }
            else
            {
                destinationX = mousePosition.x - rotateDeltaDistance;
            }
            destinationY = currentPosition.y - destinationYDelta;
            destination = new Vector3(destinationX, destinationY, 0);
        }

        // track destination
        Vector3 nextStep = UtilFunctions.GetNextStepByDestinationPoint2D(currentPosition, destination, 1);
        stepX = nextStep.x;
        stepY = nextStep.y;

        // step counter for move around mouse
        if (step >= numberOfSteps)
        {
            step = 0;
        }
        step++;
        nextStepPosition = new Vector3(stepX, stepY, 0);
    }

    public override void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision)
    {

    }

    public override void OnTriggerEnter2D(WhaleStateManager whale, Collider2D collision)
    {

    }

    public override void LeftMouseButtonClicked()
    {

    }
}
