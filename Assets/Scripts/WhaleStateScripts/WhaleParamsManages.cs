using UnityEngine.UI;
using UnityEngine;

public class WhaleParamsManages : MonoBehaviour
{
    // Game manager
    private GameManager gameManagerScript;

    // Whale
    [SerializeField] private GameObject whale;
    [SerializeField] SpriteRenderer whaleBodyRenderer;
    [SerializeField] GameObject whaleStateManager;

    // Canvas
    [SerializeField] private GameObject healthCanvas;
    public Slider slider;

    // Params
    [SerializeField] public float healthPoint = 100f;
    [SerializeField] private float healthSliderShowTime = 3f;
    [SerializeField] private float hitColorShowTime = 0.2f;
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        slider.value = healthPoint;
        HideSlider();   
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoint <= 0)
        {
            Destroy(whale);
        }
    }

    private void HideSlider()
    {
        if (healthCanvas != null)
        {
            healthCanvas.SetActive(false);
        }
    }

    private void ShowSlider()
    {
        if (healthCanvas != null)
        {
            healthCanvas.SetActive(true);
        }
    }

    private void ShowWhaleHitColor()
    {
        if (whaleBodyRenderer != null)
        {
            whaleBodyRenderer.material.color = new Color(1f, 0.3f, 0.3f);
        }
    }

    private void ShowWhaleOriginColor()
    {
        if (whaleBodyRenderer != null)
        {
            whaleBodyRenderer.material.color = new Color(1f, 1f, 1f);
        }
    }

    public void HitByEnemy(float damage)
    {
        ShowWhaleHitColor();
        ShowSlider();
        healthPoint -= damage;
        slider.value = healthPoint;
        Debug.Log("Hit By Enemy - damage: " + damage);
        FunctionTimer.Create(ShowWhaleOriginColor, hitColorShowTime);
        FunctionTimer.Create(HideSlider, healthSliderShowTime);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        bool validEnemyTagName = collider.gameObject.tag == "MeteorBody" || collider.gameObject.tag == "AlienBody";
        
        if (validEnemyTagName && !gameManagerScript.whalesAttacking)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            HitByEnemy(enemy.hitPoints);
        }
    }
}
