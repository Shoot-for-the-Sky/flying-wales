using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Game Manager
    GameManager gameManagerScript;
    [SerializeField] protected GameObject meteor;
    [SerializeField] public float hitPoints;
    [SerializeField] public float healthPoints;
    [SerializeField] public int xpPointWhenDestroyedByEnemy;

    // Explosion
    [SerializeField] GameObject explosionPrefab;

    // Destroy params
    [SerializeField] public float selfDestroyStep;
    private bool inSelfDestroying = false;
    private int touchBoundariesCounter = 0;
    private const int touchBoundariesTimes = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
        {
            if (!inSelfDestroying)
            {
                gameManagerScript.GenerateScoreUI(transform.position, xpPointWhenDestroyedByEnemy);
                gameManagerScript.AddScore(xpPointWhenDestroyedByEnemy);
                gameManagerScript.RegisterDestroyedEnemy("Meteor");
            }
            else
            {
                gameManagerScript.RegisterSurvivedEnemy("Meteor");
            }
            Destroy(meteor);
        }
        if (inSelfDestroying)
        {
            AttackByPlayer(selfDestroyStep);
        }
    }

    public void AttackByPlayer(float damage)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        healthPoints -= damage;
        Debug.Log("Attack By Player - damage: " + damage);
    }

    private void SelfDestroy()
    {
        inSelfDestroying = true;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Shield")
        {
            SelfDestroy();
        }
        if (collider.gameObject.tag == "LevelBoundaries")
        {

            if (touchBoundariesCounter >= touchBoundariesTimes)
            {
                FunctionTimer.Create(SelfDestroy, 3f);
            }
            else
            {
                touchBoundariesCounter ++;
            }
        }
    }
}
