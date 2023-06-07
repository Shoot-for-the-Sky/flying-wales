using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPositionScript : MonoBehaviour
{
    // Camera
    private Camera mainCamera;

    // Mouse
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        // Camera
        mainCamera = Camera.main;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -1f);
    }
}
