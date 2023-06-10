using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ScoreCollectorScript : MonoBehaviour
{
    public TMP_Text scoreText;
    // [SerializeField] public GameObject scoreText;
    [SerializeField] public int score;
    [SerializeField] public float movingSpeed;
    [SerializeField] public float timeToAppear;
    [SerializeField] private GameObject scoreCollector;

    // Start is called before the first frame update
    void Start()
    {
        FunctionTimer.Create(SelfDestroy, timeToAppear);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreText.text = "+" + score.ToString();
        transform.position = transform.position + (Vector3.up * movingSpeed) * Time.deltaTime;
    }

    private void SelfDestroy()
    {
        Destroy(scoreCollector);
    }
}
