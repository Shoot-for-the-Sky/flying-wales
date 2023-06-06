using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoverScript : MonoBehaviour
{
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected const int directionStepsCounter = 100;
    [SerializeField] protected float horizontalRangeLimit = 38.0f;
    [SerializeField] protected float verticalRangeLimit = 17.0f;

    private int stepsCounter = 0;
    private Vector3 nextPosition;

    private void Start()
    {
        nextPosition = transform.position;
    }

    void FixedUpdate()
    {
        transform.position += nextPosition * speed * Time.fixedDeltaTime;
        stepsCounter++;
        if (stepsCounter >= directionStepsCounter)
        {
            nextPosition = GetRandomPosition();
            stepsCounter = 0;
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 targetRandomPosition = Random.onUnitSphere * 1;
        targetRandomPosition.z = 0;
        if (OutOfBoarders())
        {
            if (transform.position.x > horizontalRangeLimit || transform.position.x < -horizontalRangeLimit)
            {
                targetRandomPosition.x = -targetRandomPosition.x;
            }
            if (transform.position.y > verticalRangeLimit || transform.position.y < -verticalRangeLimit)
            {
                targetRandomPosition.y = -targetRandomPosition.y;
            }
            nextPosition = targetRandomPosition;
        }
        return targetRandomPosition;
    }

    private bool OutOfBoarders()
    {
        return transform.position.x > horizontalRangeLimit || transform.position.x < -horizontalRangeLimit || transform.position.y > verticalRangeLimit || transform.position.y < -verticalRangeLimit;
    }
}
