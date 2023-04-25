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
    private Vector3 nextPostion;

    private void Start()
    {
        nextPostion = transform.position;
    }

    void FixedUpdate()
    {
        transform.position += nextPostion * speed * Time.fixedDeltaTime;
        stepsCounter++;
        if (stepsCounter >= directionStepsCounter)
        {
            nextPostion = GetRandomPosition();
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
                Debug.Log("Handle Horizontal");
            }
            if (transform.position.y > verticalRangeLimit || transform.position.y < -verticalRangeLimit)
            {
                targetRandomPosition.y = -targetRandomPosition.y;
                Debug.Log("Handle Verical");
            }
            nextPostion = targetRandomPosition;
        }
        return targetRandomPosition;
    }

    private bool OutOfBoarders()
    {
        return transform.position.x > horizontalRangeLimit || transform.position.x < -horizontalRangeLimit || transform.position.y > verticalRangeLimit || transform.position.y < -verticalRangeLimit;
    }
}
