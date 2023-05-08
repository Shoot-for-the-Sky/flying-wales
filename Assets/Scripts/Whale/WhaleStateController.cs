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
    public bool isWhaleStateControllerDisabled = false;

    void OnEnable()
    {
        dynamicStateButton.Enable();
        trackStateButton.Enable();
        attackStateButton.Enable();
    }

    void OnDisable()
    {
        dynamicStateButton.Disable();
        trackStateButton.Disable();
        attackStateButton.Disable();
    }

    private void Start()
    {
        stateManager = GetComponent<WhaleStateManager>();
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
        }
        else if (trackStateButton.WasPressedThisFrame())
        {
            stateManager.ChangeStateByName(WhaleState.Track);
        }
        else if (attackStateButton.WasPressedThisFrame())
        {
            stateManager.ChangeStateByName(WhaleState.Attack);
        }
    }
}
