using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WhaleParamsManages : MonoBehaviour
{
    // Whale
    [SerializeField] private GameObject whale;
    [SerializeField] SpriteRenderer whaleBodyRenderer;

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
        healthCanvas.SetActive(false);
    }

    private void ShowSlider()
    {
        healthCanvas.SetActive(true);
    }

    private void ShowWhaleHitColor()
    {
        whaleBodyRenderer.material.color = new Color(1f, 0.3f, 0.3f);
    }

    private void ShowWhaleOriginColor()
    {
        whaleBodyRenderer.material.color = new Color(1f, 1f, 1f);
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MeteorBody")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            HitByEnemy(enemy.hitPoints);
        }
    }
}
