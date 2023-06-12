using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteScript : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        int randomSpriteIndex = UtilFunctions.GetRandomIntInRange(0, sprites.Count);
        ChangeSprite(randomSpriteIndex);
    }

    private void ChangeSprite(int spriteIndex)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
