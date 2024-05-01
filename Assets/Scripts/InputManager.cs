using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputControl inputControl;
    private static InputManager _instance;
    public static InputManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        inputControl = new InputControl();
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return inputControl.PlayerActions.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return inputControl.PlayerActions.Look_Around.ReadValue<Vector2>();
    }

    public bool PlayerInteractedThisFrame()
    {
        return inputControl.PlayerActions.Interact.triggered;
    }

    public bool PlayerCrouching()
    {
        return inputControl.PlayerActions.Crouch.IsPressed();
    }
}
