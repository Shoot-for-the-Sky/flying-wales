using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhalesManager : MonoBehaviour
{
    // Whales
    [SerializeField] protected GameObject whalePrefab;
    [SerializeField] protected int numberOfWhales = 3;
    private List<GameObject> whales;

    // whale speed params
    [SerializeField] public float whaleSpeed = 1f;
    [SerializeField] public float whaleRotateSpeed = 5f;

    // Buttons contolling the whales
    [SerializeField] InputAction dynamicStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction trackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction attackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction LeftMouseButton = new InputAction(type: InputActionType.Button);
    public bool isWhaleStateControllerDisabled = false;

    // States UI Sprites
    [SerializeField] protected List<Sprite> dynamicSprites;
    [SerializeField] protected SpriteRenderer dynamicSpriteRenderer;

    [SerializeField] protected List<Sprite> trackSprites;
    [SerializeField] protected SpriteRenderer trackSpriteRenderer;

    [SerializeField] protected List<Sprite> attackSprites;
    [SerializeField] protected SpriteRenderer attackSpriteRenderer;

    void OnEnable()
    {
        dynamicStateButton.Enable();
        trackStateButton.Enable();
        attackStateButton.Enable();
        LeftMouseButton.Enable();
    }

    void OnDisable()
    {
        dynamicStateButton.Disable();
        trackStateButton.Disable();
        attackStateButton.Disable();
        LeftMouseButton.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Create whales
        whales = new List<GameObject>();
        for (int i = 0; i < numberOfWhales; i++)
        {
            GameObject whale = Instantiate(whalePrefab);
            whales.Add(whale);
        }

        ChangeWhalesState(WhaleState.Dynamic);
        ChangeStatesUI(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWhaleStateControllerDisabled)
        {
            return;
        }
        if (dynamicStateButton.WasPressedThisFrame())
        {
            ChangeWhalesState(WhaleState.Dynamic);
            ChangeStatesUI(1, 0, 0);
        }
        else if (trackStateButton.WasPressedThisFrame())
        {
            ChangeWhalesState(WhaleState.Track);
            ChangeStatesUI(0, 1, 0);
        }
        else if (attackStateButton.WasPressedThisFrame())
        {
            ChangeWhalesState(WhaleState.Attack);
            ChangeStatesUI(0, 0, 1);
        }
        else if (LeftMouseButton.WasPerformedThisFrame())
        {
            foreach (GameObject whale in whales)
            {
                whale.GetComponent<WhaleStateManager>().LeftMouseButtonClicked();
            }
        }
    }

    private void ChangeWhalesState(WhaleState whaleState)
    {
        foreach (GameObject whale in whales)
        {
            whale.GetComponent<WhaleStateManager>().ChangeStateByName(whaleState);
        }
    }

    private void ChangeWhalesSpeed()
    {
        foreach (GameObject whale in whales)
        {
            whale.GetComponent<WhaleStateManager>().ChangeWhaleSpeed(whaleSpeed);
            whale.GetComponent<WhaleStateManager>().ChangeWhaleRotateSpeed(whaleRotateSpeed);
        }
    }

    private void ChangeStatesUI(int dynamic, int track, int attack)
    {
        dynamicSpriteRenderer.sprite = dynamicSprites[dynamic];
        trackSpriteRenderer.sprite = trackSprites[track];
        attackSpriteRenderer.sprite = attackSprites[attack];
    }
}
