using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float timeToAppear;
    [SerializeField] private float movingSpeed;

    // Start is called before the first frame update
    void Start()
    {
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
