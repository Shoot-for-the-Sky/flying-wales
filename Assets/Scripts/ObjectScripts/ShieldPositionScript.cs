using UnityEngine;

public class ShieldPositionScript : MonoBehaviour
{
    // Camera
    private Camera mainCamera;

    // Mouse
    private Vector3 mousePosition;

    // Inner shield
    [SerializeField] private GameObject innerShield;
    [SerializeField] private float initLocalScale;
    private Vector3 initLocalScaleVector;
    [SerializeField] private float maxLocalScale;
    [SerializeField] private float localScaleStep;
    private Vector3 localScaleStepVector;

    // Start is called before the first frame update
    void Start()
    {
        // Camera
        mainCamera = Camera.main;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -1f);

        // Inner shield
        initLocalScaleVector = new Vector3(initLocalScale, initLocalScale, initLocalScale);
        localScaleStepVector = new Vector3(localScaleStep, localScaleStep, localScaleStep);
        SetInitLocalScaleInnerShield();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Shield position
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -1f);

        // Inner Shield
        GrowInnerShield();
    }

    private void GrowInnerShield()
    {
        bool isLocalScaleNotMaximum = innerShield.transform.localScale.x < maxLocalScale;
        if (isLocalScaleNotMaximum)
        {
            innerShield.transform.localScale += localScaleStepVector;
        }
        else
        {
            SetInitLocalScaleInnerShield();
        }
    }

    private void SetInitLocalScaleInnerShield()
    {
        innerShield.transform.localScale = initLocalScaleVector;
    }
}
