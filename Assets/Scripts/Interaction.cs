using System;
using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun.UtilityScripts;

public class Interaction : MonoBehaviourPunCallbacks
{
    private InputControl control;
    private Rigidbody rb;
    [SerializeField] private float InteractRange;
    [SerializeField] private float InteractView;
    [SerializeField] private GameObject objetoRay;
    private Player player;

    [SerializeField] Canvas Provisional;
    private void Awake()
    {
        control = new InputControl();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }
    new private void OnEnable()
    {
        control.PlayerActions.Interact.performed += Interact;
        control.PlayerActions.Interact.Enable();
        control.PlayerActions.Uninteract.performed += ExitInteraction;
        control.PlayerActions.Uninteract.Enable();
        //control.PlayerActions.Crouch.performed += isCrouching;
        control.PlayerActions.Crouch.Enable();
    }


    new private void OnDisable()
    {
            control.PlayerActions.Interact.performed -= Interact;
            control.PlayerActions.Uninteract.performed -= ExitInteraction;
            control.PlayerActions.Crouch.performed -= ExitInteraction;
    }

    private void Interact(InputAction.CallbackContext A)
    {
        Vector3 rayOrigin = objetoRay.transform.position;
        Vector3 rayDirection = objetoRay.transform.forward;

        Ray r = new Ray(rayOrigin, rayDirection);

        if (Physics.Raycast(r, out RaycastHit hit, InteractRange))
        {
            if (hit.collider.gameObject.TryGetComponent(out PuzzlesFather interactObj))
            {
                interactObj.enabled = true;
                interactObj.ChangeCamera(true);
                player.isInteracting = true;
                player.enabled = false;
            }

            else if(hit.collider.gameObject.TryGetComponent(out Vent_JonnoEntrance interact))
            {
                interact.Interact(player.MyCamera, this.gameObject);
            }
            else if (hit.collider.gameObject.TryGetComponent(out Vent_JonnoExit interactable))
            {
                interactable.Interact(player.MyCamera, this.gameObject);
            }
        }
    }

    private void ExitInteraction(InputAction.CallbackContext context)
    {
        if (player.isInteracting)
        {
            Vector3 rayOrigin = objetoRay.transform.position;
            Vector3 rayDirection = objetoRay.transform.forward;

            Ray r = new Ray(rayOrigin, rayDirection);

            if (Physics.Raycast(r, out RaycastHit hit, InteractRange))
            {
                if (hit.collider.gameObject.TryGetComponent(out PuzzlesFather interactObj))
                {
                    interactObj.ChangeCamera(false);
                    interactObj.Exit();
                    StartCoroutine("BackToNormalView");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {

        // Configurar el color del gizmo
        Gizmos.color = Color.red;

        // Dibujar el rayo desde objetoRay en la dirección hacia adelante
        Vector3 rayOrigin = objetoRay.transform.position;
        Vector3 rayDirection = objetoRay.transform.forward;
        Gizmos.DrawRay(rayOrigin, rayDirection * InteractRange);
    }

    private IEnumerator BackToNormalView()
    {
        player.isInteracting = false;
        yield return new WaitForSeconds(3f);
        player.enabled = true;
    }

}