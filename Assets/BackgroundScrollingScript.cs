using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollingScript : MonoBehaviour
{
    public float speed;
    [SerializeField] private Renderer BackgroundRenderer;
    [SerializeField] private float textureOffset;
    [SerializeField] private float offsetStep;
    [SerializeField] private int percentageToChangeOffset;
    [SerializeField] private float changeOffsetEachTime;

    public void Start()
    {
        StartCoroutine(ChangeOffset());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BackgroundRenderer.material.mainTextureOffset += new Vector2(speed * Time.fixedDeltaTime, textureOffset);
    }

    private IEnumerator ChangeOffset()
    {
        while (true)
        {
            bool changeOffset = UtilFunctions.RollInPercentage(percentageToChangeOffset);
            if (changeOffset)
            {
                if (textureOffset < 0)
                {
                    textureOffset += offsetStep * 2;
                }
                else
                {
                    textureOffset -= offsetStep;
                }
            }
            yield return new WaitForSeconds(changeOffsetEachTime);
        }
    }
}
