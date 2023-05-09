using UnityEngine;

public class WhaleAttackState : WhaleBaseState
{
    private Camera mainCamera;
    private Vector3 mousePosition;

    private readonly float whaleAttackRotateSpeed = 20f;
    private readonly int attackTime = 30;

    private float whaleAttackSpeed = 1f;
    private float whaleStepSlice = 10f;
    private int attackTimeCounter = 0;

    bool whaleAttack = false;

    float attackStepX;
    float attackStepY;

    private const float maxAttackSpeed = 22f;
    private const float minAttackSpeed = 18f;

    public override void EnterState(WhaleStateManager whale)
    {
        Debug.Log("EnterState Attack State");
        nextStepPosition = Vector3.zero;
        nextPostion = Vector3.zero;
        prevPostion = Vector3.zero;
        mainCamera = Camera.main;
    }

    public override void UpdateState(WhaleStateManager whale)
    {
        whale.whaleRotateSpeed = whaleAttackRotateSpeed;
        whale.whaleSpeed = whaleAttackSpeed;

        if (whaleAttack)
        {
            whaleStepSlice = 1;
            whaleAttackSpeed = UtilFunctions.GetRandomDoubleInRange(minAttackSpeed, maxAttackSpeed);
            Debug.Log("Attcking! " + attackTimeCounter);
            if (attackTimeCounter >= attackTime)
            {
                attackTimeCounter = 0;
                whaleAttack = false;
            }
            attackTimeCounter++;

        } else
        {
            whaleStepSlice = 10;
            whaleAttackSpeed = 1;

            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 currentPosition = whale.transform.position;
            Vector3 destination = new Vector3(mousePosition.x, mousePosition.y, 0);
            Vector3 nextStep = UtilFunctions.GetNextStepByDestinationPoint2D(currentPosition, destination, whaleStepSlice);

            attackStepX = nextStep.x;
            attackStepY = nextStep.y;
        }
        
        nextStepPosition = new Vector3(attackStepX, attackStepY, 0);
    }

    public override void OnCollisionEnter2D(WhaleStateManager whale, Collision2D collision)
    {

    }

    public override void OnTriggerEnter2D(WhaleStateManager whale, Collider2D collision)
    {

    }

    public override void LeftMouseButtonClicked()
    {
        whaleAttack = true;
    }
}
