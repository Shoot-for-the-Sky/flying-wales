using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleMoveScript : MonoBehaviour
{
    [SerializeField] protected float minSpeed = 5.0f;
    [SerializeField] protected float maxSpeed = 10.0f;
    [SerializeField] protected float deadAxisXZone = 30.0f;

    void Update()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        transform.position = transform.position + (Vector3.right * speed) * Time.deltaTime;
        if (transform.position.x > deadAxisXZone)
        {
            Destroy(gameObject);
        }
    }
}
