using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnerScript : MonoBehaviour
{
    public GameObject missle;
    public float spawnRate = 1f;
    private float timer = 0;
    public float heightOffset = 3f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0;
        }

    }
    void spawnPipe()
    {
        float lowerPoint = transform.position.y - heightOffset;
        float higherPoint = transform.position.y + heightOffset;
        Instantiate(missle, new Vector3(transform.position.x, Random.Range(lowerPoint, higherPoint), 0), transform.rotation);
    }
}
