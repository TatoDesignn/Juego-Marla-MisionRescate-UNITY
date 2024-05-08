using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;
using Photon.Pun.UtilityScripts;

public class Player : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;
    public Animator MyAnimator;
    Animator medidorAnimator;

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
        medidorAnimator = GameObject.FindGameObjectWithTag("Medidor").GetComponent<Animator>();

        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody>();
        MyAnimator = GetComponent<Animator>();

        if (!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }

    }

    private void Update()
    {
        if (Comunicador.canMove)
        {
            if (photonView.IsMine)
            {
                //Get Camera Input
                Vector2 move = inputManager.GetMouseDelta();
                move = move * mouseSensitivity * Time.deltaTime;
                xRotation -= move.y;
                xRotation = Mathf.Clamp(xRotation, -LookClamp, LookClamp);

                //Move Camera
                Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                transform.Rotate(Vector3.up * move.x);

            }
        }
    }

    private void FixedUpdate()
    {
        if (Comunicador.canMove)
        {
            if (photonView.IsMine)
            {
                //Get Input
                Vector2 move = inputManager.GetPlayerMovement();
                Vector3 movement = transform.right * move.x + transform.forward * move.y;
                //Animate
                MyAnimator.SetBool("isCrouching", inputManager.PlayerCrouching());
                if (movement != Vector3.zero)
                {
                    MyAnimator.SetBool("isWalking", true);
                    MyAnimator.SetFloat("X", move.y);
                    MyAnimator.SetFloat("Y", move.x);
                }
                else
                    MyAnimator.SetBool("isWalking", false);
                //Move
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CamaraSensor") || other.CompareTag("LucesDeteccion"))
        {
            medidorAnimator.SetTrigger("Cargar");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CamaraSensor") || other.CompareTag("LucesDeteccion"))
        {
            medidorAnimator.SetTrigger("Reanudar");
        }
    }
}
