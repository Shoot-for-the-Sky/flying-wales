using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollingScript : MonoBehaviour
{
    public float speed;
    [SerializeField] private Renderer BackgroundRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BackgroundRenderer.material.mainTextureOffset += new Vector2(speed * Time.fixedDeltaTime, 0);
    }
}
