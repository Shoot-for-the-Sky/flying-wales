using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] protected float minSpeed = 1.0f;
    [SerializeField] protected float maxSpeed = 3.0f;

    [SerializeField] protected float minDeadAxisXZone = -30.0f;
    [SerializeField] protected float maxDeadAxisXZone = 30.0f;

    [SerializeField] protected float minDeadAxisYZone = -10.0f;
    [SerializeField] protected float maxDeadAxisYZone = 10.0f;

    [SerializeField] protected string moveDirection = "down";

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
        if (moveDirection == "right")
        {
            direction = Vector3.right;
        } else if (moveDirection == "left")
        {
            direction = Vector3.left;
        } else if (moveDirection == "up")
        {
            direction = Vector3.up;
        } else
        {
            direction = Vector3.down;
        }
        return direction;
    }
}
