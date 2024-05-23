using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;
using Photon.Pun.UtilityScripts;

public class Player : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;
    public Animator MyAnimator;
    public Animator medidorAnimator;
    CapsuleCollider capsuleCollider;
    private AudioSource audioSource;

    [Space]
    [Header("Configuracion de Movimiento")]
    [SerializeField] private float speed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float LookClamp;
    [SerializeField] private GameObject ray;
    public bool isInteracting = false;

    [Header("Configuracion de camara")]
    [SerializeField] public Camera MyCamera;
    [SerializeField] Transform camaraTransform;
    [SerializeField] private Vector3 camaraAgachado;
    [SerializeField] private Vector3 camaraAcostado;
    private float xRotation;
    private Vector3 camaraInicial;
    private Vector3 centroCapsula;
    private bool agachado = false;
    private bool acostado = false;

    [Header("New Input System")]
    public InputManager inputManager;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip footstepSound;

    void Start()
    {
        medidorAnimator = GameObject.FindGameObjectWithTag("Medidor").GetComponent<Animator>();

        capsuleCollider = GetComponent<CapsuleCollider>();
        centroCapsula = capsuleCollider.center;

        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody>();
        MyAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        camaraInicial = camaraTransform.localPosition;

        if (!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponent<Interaction>());
            Destroy(ray);
        }

        // Ensure the AudioSource is set up correctly
        if (audioSource != null)
        {
            audioSource.clip = footstepSound;
            audioSource.loop = false; // Steps usually do not loop
        }
    }

    private void Update()
    {
        if (Comunicador.canMove)
        {
            if (photonView.IsMine)
            {
                // Get Camera Input
                Vector2 move = inputManager.GetMouseDelta();
                move = move * mouseSensitivity * Time.deltaTime;
                xRotation -= move.y;
                xRotation = Mathf.Clamp(xRotation, -LookClamp, LookClamp);

                // Move Camera
                MyCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
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
                // Get Input
                Vector2 move = inputManager.GetPlayerMovement();
                Vector3 movement = transform.right * move.x + transform.forward * move.y;

                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

                // Play footstep sound if moving
                if (movement.magnitude > 0 && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                /*if (isInteracting)
                {
                    move.x = 0f;
                    move.y = 0f;
                }*/

                // Animacion Parado
                MyAnimator.SetFloat("MovX", move.x);
                MyAnimator.SetFloat("MovY", move.y);

                // Animacion Agachado
                if (Input.GetKey(KeyCode.LeftShift) && !acostado)
                {
                    MyAnimator.SetBool("Agacharse", true);
                    capsuleCollider.height = 1.4f;
                    centroCapsula.y = 0.7f;
                    capsuleCollider.center = centroCapsula;
                    camaraTransform.localPosition = camaraInicial + camaraAgachado;
                    agachado = true;
                }
                else if (agachado && !Input.GetKey(KeyCode.LeftShift))
                {
                    capsuleCollider.height = 1.8f;
                    centroCapsula.y = 0.9f;
                    capsuleCollider.center = centroCapsula;
                    MyAnimator.SetBool("Agacharse", false);
                    camaraTransform.localPosition = camaraInicial;
                    agachado = false;
                }

                // Animacion Acostado
                if (Input.GetKey(KeyCode.C) && !agachado)
                {
                    MyAnimator.SetBool("Acostado", true);
                    capsuleCollider.height = 1;
                    centroCapsula.y = 0.5f;
                    capsuleCollider.center = centroCapsula;
                    camaraTransform.localPosition = camaraInicial + camaraAcostado;
                    acostado = true;
                }
                else if(acostado && !Input.GetKey(KeyCode.C))
                {
                    capsuleCollider.height = 1.8f;
                    centroCapsula.y = 0.9f;
                    capsuleCollider.center = centroCapsula;
                    MyAnimator.SetBool("Acostado", false);
                    camaraTransform.localPosition = camaraInicial;
                    acostado = false;
                }

                // Para que no se quede moviendo mientras esta en un puzzle
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("LucesDeteccion"))
        {
            medidorAnimator.SetTrigger("Cargar");
        }

        else if (other.CompareTag("CamaraSensor") || other.CompareTag("Perder"))
        {
            medidorAnimator.SetTrigger("Cargar");
            medidorAnimator.SetFloat("Multiplier", 10f);
        }

        else if (other.CompareTag("Guardia"))
        {
            Patrullar GuardiaAnimator = other.GetComponent<Patrullar>();
            GuardiaAnimator.SetAnimator(true);
            medidorAnimator.SetTrigger("Cargar");
            medidorAnimator.SetFloat("Multiplier", 2.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CamaraSensor") || other.CompareTag("LucesDeteccion"))
        {
            medidorAnimator.SetTrigger("Reanudar");
        }
        else if (other.CompareTag("Guardia"))
        {
            Patrullar GuardiaAnimator = other.GetComponent<Patrullar>();
            GuardiaAnimator.SetAnimator(false);
            medidorAnimator.SetTrigger("Reanudar");
            medidorAnimator.SetFloat("Multiplier", 1f);
        }
    }
}
