using UnityEngine;

public class WhaleDynamicState : WhaleBaseState
{
    private const int directionStepsCounter = 100;
    private int stepsCounter = 0;
    private const float chanceToGoSameDirection = 0.9f;
    private const float chanceToChangeAxisDirection = 0.5f;
    private float whaleDynamicRotateSpeed = 1f;
    private float whaleDynamicSpeed = 1f;

    public override void EnterState(WhaleStateManager whale)
    {
        nextStepPosition = GetRandomPosition();
        nextPostion = Vector3.zero;
        prevPostion = Vector3.zero;
    }

    public override void UpdateState(WhaleStateManager whale)
    {
        whale.whaleRotateSpeed = whaleDynamicRotateSpeed;
        whale.whaleSpeed = whaleDynamicSpeed;
        if (stepsCounter >= directionStepsCounter)
        {
            prevStepPosition = new Vector3(nextStepPosition.x, nextStepPosition.y, nextStepPosition.z);
            nextStepPosition = GetRandomPosition();
            stepsCounter = 0;
        }
        stepsCounter++;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 targetRandomPosition;
        if (prevPostion != Vector3.zero && prevPostion != nextStepPosition)
        {
            // check whale previous directions
            bool goingRight = IsWhaleGoingRight();
            bool goingUp = IsWhaleGoingUp();

            // check if whale should change is direction of swimming
            float directionProbability = UtilFunctions.GetRandomDoubleInRange(0, 1);
            bool goSameDirection = directionProbability < chanceToGoSameDirection;


            // if we change whale direction
            if (!goSameDirection)
            {
                // if we change whale X Axis direction
                float changeAxisXDirectionProbability = UtilFunctions.GetRandomDoubleInRange(0, 1);
                bool changeAxisXDirection = changeAxisXDirectionProbability > chanceToChangeAxisDirection;
                if (changeAxisXDirection)
                {
                    goingRight = !goingRight;
                }

                // if we change whale Y Axis direction
                float changeAxisYDirectionProbability = UtilFunctions.GetRandomDoubleInRange(0, 1);
                bool changeAxisYDirection = changeAxisYDirectionProbability > chanceToChangeAxisDirection;
                if (changeAxisYDirection)
                {
                    goingUp = !goingUp;
                }
            }
            Vector3 nextStepPositionByDirection = GetNextStepPositionByDirection(goingUp, goingRight);
            targetRandomPosition = new Vector3(nextStepPositionByDirection.x, nextStepPositionByDirection.y, 0);
        }
        else
        {
            // first direction get random next position
            targetRandomPosition = Random.onUnitSphere * 1;
        }
        targetRandomPosition.z = 0;
        return targetRandomPosition;
    }

    private Vector3 GetNextStepPositionByDirection(bool up, bool right)
    {
        float nextX;
        float nextY;

        if (up)
        {
            nextY = UtilFunctions.GetRandomDoubleInRange(0, 1);
        }
        else
        {
            nextY = UtilFunctions.GetRandomDoubleInRange(-1, 0);
        }
        if (right)
        {
            nextX = UtilFunctions.GetRandomDoubleInRange(0, 1);
        }
        else
        {
            nextX = UtilFunctions.GetRandomDoubleInRange(-1, 0);
        }
        return new Vector3(nextX, nextY, .0f);
    }

    public override void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision)
    {

    }

    public override void OnTriggerEnter2D(WhaleStateManager whale, Collider2D collision)
    {
        // if the whale touch the level boundaries 
        if (collision.gameObject.tag == "LevelBoundaries")
        {
            // swap direction of next step position
            stepsCounter = 0;
            nextStepPosition.x = -nextStepPosition.x;
            nextStepPosition.y = -nextStepPosition.y;

            // swap positions
            Vector3 tempPosition = new Vector3(prevPostion.x, prevPostion.y, prevPostion.y);
            prevPostion = new Vector3(nextPostion.x, prevPostion.y, prevPostion.z);
            nextPostion = new Vector3(tempPosition.x, tempPosition.y, tempPosition.z);
        }

        if (collision.gameObject.tag == "MeteorBody")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            whale.HitByEnemey(enemy.hitPoints);
        }
    }

    public override void LeftMouseButtonClicked()
    {

    }
}
