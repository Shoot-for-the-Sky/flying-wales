using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float timeToAppear;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float minRandomScale;
    [SerializeField] private float maxRandomScale;
    [SerializeField] private float rangeRandomPositionX;
    [SerializeField] private float rangeRandomPositionY;

    // Start is called before the first frame update
    void Start()
    {
        // Scale
        float randomScale = UtilFunctions.GetRandomDoubleInRange(minRandomScale, maxRandomScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        // Position
        float randomAxisX = UtilFunctions.GetRandomDoubleInRange(-rangeRandomPositionX, rangeRandomPositionX);
        float randomAxisY = UtilFunctions.GetRandomDoubleInRange(-rangeRandomPositionY, rangeRandomPositionY);
        Vector3 newPosition = new Vector3(transform.position.x + randomAxisX, transform.position.y + randomAxisY, transform.position.z);
        transform.position = newPosition;
        
        // Destroy
        FunctionTimer.Create(SelfDestroy, timeToAppear);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.up * movingSpeed) * Time.deltaTime;
    }

    private void SelfDestroy()
    {
        Destroy(explosionPrefab);
    }
}
