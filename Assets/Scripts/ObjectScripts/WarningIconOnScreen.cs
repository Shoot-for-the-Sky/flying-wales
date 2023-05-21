using UnityEngine;

public class WarningIconOnScreen : MonoBehaviour
{
    [SerializeField] private GameObject trackObject;
    [SerializeField] private float defaultY = 4.0f;
    [SerializeField] private float defaultX = 8.0f;
    private Direction trackObjectDirection;

    // Start is called before the first frame update
    void Start()
    {
        EnemyObjectMover moverScript = trackObject.GetComponent<EnemyObjectMover>();
        trackObjectDirection = moverScript.moveDirection;
        Debug.Log(trackObjectDirection);
        Vector3 warningPosition = GetWarningBorderPosition();
        Vector3 newPosition = new Vector3(warningPosition.x, warningPosition.y, transform.position.z);
        transform.position = newPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 warningPosition = GetWarningBorderPosition();
        transform.position = new Vector3(warningPosition.x, warningPosition.y, transform.position.z);
    }

    private Vector3 GetWarningBorderPosition()
    {
        float positionX;
        float positionY;
        switch (trackObjectDirection)
        {
            case (Direction.Up):
                positionY = -defaultY;
                positionX = trackObject.transform.position.x;
                break;
            case (Direction.Down):
                positionY = defaultY;
                positionX = trackObject.transform.position.x;
                break;
            case (Direction.Right):
                positionX = -defaultX;
                positionY = trackObject.transform.position.y;
                break;
            case (Direction.Left):
                positionX = defaultX;
                positionY = trackObject.transform.position.y;
                break;
            default:
                positionX = transform.position.x;
                positionY = transform.position.y;
                break;
        }
        return new Vector3(positionX, positionY, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MeteorBody"))
        {
            Destroy(gameObject);
        }
    }
}
