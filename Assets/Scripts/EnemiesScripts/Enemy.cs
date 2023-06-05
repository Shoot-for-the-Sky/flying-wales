using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Game Manager
    GameManager gameManagerScript;
    [SerializeField] protected GameObject meteor;
    [SerializeField] public float hitPoints;
    [SerializeField] public float healthPoints;

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
            gameManagerScript.RegisterDestroyedEnemy("Meteor");
            Destroy(meteor);
        }
    }

    public void AttackByPlayer(float damage)
    {
        healthPoints -= damage;
        Debug.Log("Attack By Player - damage: " + damage);
    }
}
