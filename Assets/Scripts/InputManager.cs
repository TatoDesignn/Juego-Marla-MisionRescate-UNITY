using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static InputManager instance;
    public static InputManager Instance
    {
        get { return instance; }
    }
    private PlayerControl PlayerControl;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        PlayerControl = new PlayerControl();
        Cursor.visible = false;
    }
    private void OnEnable()
    {
        PlayerControl.Enable();
    }
    private void OnDisable()
    {
        PlayerControl.Disable();
    }
    public Vector2 GetPlayerMovement()
    {
        return PlayerControl.Player.MovementActions.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return PlayerControl.Player.LookAction.ReadValue<Vector2>();
    }
    public bool PlayerInteractedThisFrame()
    {
        return PlayerControl.Player.InteractAction.triggered;
    }
}



