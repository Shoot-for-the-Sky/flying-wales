using UnityEngine;

public class UtilFunctions : MonoBehaviour
{
    public static Vector3 GetNextStepByDestinationPoint2D(Vector3 source, Vector3 destination, float stepsSlices)
    {
        // return the step we need to take to get from source to destination by steps slices
        bool needToGoRight = source.x < destination.x;
        bool needToGoUp = source.y < destination.y;

        float stepX = Mathf.Abs(source.x - destination.x) / stepsSlices;
        float stepY = Mathf.Abs(source.y - destination.y) / stepsSlices;

        if (!needToGoRight)
            stepX = -stepX;

        if (!needToGoUp)
            stepY = -stepY;

        return new Vector3(stepX, stepY, 0);
    }

    public static bool IsPointsClose2D(Vector3 pointA, Vector3 pointB, float epsilon)
    {
        float distance = Vector2.Distance(pointA, pointB);
        return distance < epsilon;
    }

    public static bool IsPointsClose2D(Vector3 pointA, Vector3 pointB, float epsilonXAxis, float epsilonYAxis)
    {
        float distanceX = Mathf.Abs(pointA.x - pointB.x);
        float distanceY = Mathf.Abs(pointA.y - pointB.y);
        return distanceX < epsilonXAxis && distanceY < epsilonYAxis;
    }

    // take help from: https://stackoverflow.com/questions/3975290/produce-a-random-number-in-a-range-using-c-sharp
    public static float GetRandomDoubleInRange(float min, float max)
    {
        System.Random r = new System.Random();
        double rDouble = r.NextDouble() * (max - min);
        return (float)(min + rDouble);
    }
}
