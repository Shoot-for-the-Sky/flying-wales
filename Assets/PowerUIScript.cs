using UnityEngine;

public class PowerUIScript : MonoBehaviour
{
    public bool inUse = false;
    private float useGBColor = 0.3f;
    private const float colorStep = 0.005f;
    private float currentGBColor;
    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        currentGBColor = 1f;
        renderer.material.color = new Color(1f, currentGBColor, currentGBColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (inUse)
        {
            if (currentGBColor > useGBColor)
            {
                currentGBColor -= colorStep;
                renderer.material.color = new Color(1f, currentGBColor, currentGBColor);
            }
        }
        else
        {
            if (currentGBColor < 1f)
            {
                currentGBColor += colorStep;
                renderer.material.color = new Color(1f, currentGBColor, currentGBColor);
            }
        }
    }
}
