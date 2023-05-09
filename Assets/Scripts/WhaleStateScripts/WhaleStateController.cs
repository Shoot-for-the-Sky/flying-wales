using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhaleStateController : MonoBehaviour
{
    /**
     The purepose if this class is to change the state of an whale using input button (controller)
     */
    WhaleStateManager stateManager;
    [SerializeField] InputAction dynamicStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction trackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction attackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction LeftMouseButton = new InputAction(type: InputActionType.Button);
    public bool isWhaleStateControllerDisabled = false;

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

    private void Start()
    {
        stateManager = GetComponent<WhaleStateManager>();
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
            stateManager.ChangeStateByName(WhaleState.Dynamic);
            ChangeStatesUI(1, 0, 0);
        }
        else if (trackStateButton.WasPressedThisFrame())
        {
            stateManager.ChangeStateByName(WhaleState.Track);
            ChangeStatesUI(0, 1, 0);
        }
        else if (attackStateButton.WasPressedThisFrame())
        {
            stateManager.ChangeStateByName(WhaleState.Attack);
            ChangeStatesUI(0, 0, 1);
        }
        else if (LeftMouseButton.WasPerformedThisFrame())
        {
            stateManager.LeftMouseButtonClicked();
        }
    }

    private void ChangeStatesUI(int dynamic, int track, int attack)
    {
        dynamicSpriteRenderer.sprite = dynamicSprites[dynamic];
        trackSpriteRenderer.sprite = trackSprites[track];
        attackSpriteRenderer.sprite = attackSprites[attack];
    }
}
