using UnityEngine;

public class WhaleTrackState : WhaleBaseState
{
    private const float epsilon = 0.1f;
    private Camera mainCamera;
    private Vector3 mousePosition;

    public override void EnterState(WhaleStateManager whale)
    {
        Debug.Log("EnterState Track State");
        mainCamera = Camera.main;
    }

    public override void UpdateState(WhaleStateManager whale)
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        float stepX;
        float stepY;

        if (!IsWhaleCloseToMouse(whale))
        {
            Vector3 currentPosition = whale.transform.position;

            bool needToGoRight = currentPosition.x < mousePosition.x;
            bool needToGoUp = currentPosition.y < mousePosition.y;

            stepX = Mathf.Abs(currentPosition.x - mousePosition.x) / 3;
            stepY = Mathf.Abs(currentPosition.y - mousePosition.y) / 3;

            if (!needToGoRight)
            {
                stepX = -stepX;
            }

            if (!needToGoUp)
            {
                stepY = -stepY;
            }
        } else
        {
            stepX = 0;
            stepY = 0;
        }
        
        nextStepPosition = new Vector3(stepX, stepY, 0);
    }

    public override void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision)
    {

    }

    public override void OnTriggerEnter2D(WhaleStateManager whale, Collider2D collision)
    {

    }

    private bool IsWhaleCloseToMouse(WhaleStateManager whale)
    {
        float distance = Vector2.Distance(mousePosition, whale.transform.position);
        Debug.Log("distance " + distance);
        return distance < epsilon;
    }
}
