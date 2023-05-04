using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhaleStateController : MonoBehaviour
{
    WhaleStateManager stateManager;
    [SerializeField] InputAction dynamicStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction groupStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction trackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction attackStateButton = new InputAction(type: InputActionType.Button);
    public bool isWhaleStateControllerDisabled = false;

    void OnEnable()
    {
        dynamicStateButton.Enable();
        groupStateButton.Enable();
        trackStateButton.Enable();
        attackStateButton.Enable();
    }

    void OnDisable()
    {
        dynamicStateButton.Disable();
        groupStateButton.Disable();
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
        if(isWhaleStateControllerDisabled)
        {
            return;
        }
        if(dynamicStateButton.WasPressedThisFrame())
        {
            stateManager.ChangeStateByName(WhaleState.Dynamic);
        }
        else if(groupStateButton.WasPressedThisFrame())
        {
            stateManager.ChangeStateByName(WhaleState.Group);
        }
        else if(trackStateButton.WasPressedThisFrame())
        {
            stateManager.ChangeStateByName(WhaleState.Track);
        }
        else if(attackStateButton.WasPressedThisFrame())
        {
            stateManager.ChangeStateByName(WhaleState.Attack);
        }
    }
}
