using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;
using Photon.Pun.UtilityScripts;

public class Player : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;
    private Animator MyAnimator;

    [Space]
    [Header("Configuracion de Movimiento")]
    [SerializeField] private float speed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float LookClamp;
    public bool isInteracting = false;

    [Header("Variables locales")]
    private float xRotation;

    [Header("New Input System")]
    private InputManager inputManager;

    void Start()
    {
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        MyAnimator = GetComponent<Animator>();

        if (!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }

    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Vector2 move = inputManager.GetMouseDelta();
            move = move * mouseSensitivity * Time.deltaTime;
            xRotation -= move.y;
            xRotation = Mathf.Clamp(xRotation, -LookClamp, LookClamp);

            Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * move.x);

        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Vector2 move = inputManager.GetPlayerMovement();
            Vector3 movement = transform.right * move.x + transform.forward * move.y;
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }
}
