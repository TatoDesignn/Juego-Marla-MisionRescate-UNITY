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
        if (photonView.IsMine)
        {
            control.PlayerActions.Interact.performed += Interact;
            control.PlayerActions.Interact.Enable();
            control.PlayerActions.Uninteract.performed += ExitInteraction;
            control.PlayerActions.Uninteract.Enable();
            //control.PlayerActions.Crouch.performed += isCrouching;
            control.PlayerActions.Crouch.Enable();
        }
    }


    new private void OnDisable()
    {
        if (photonView.IsMine)
        {
            control.PlayerActions.Interact.performed -= Interact;
            control.PlayerActions.Uninteract.performed -= ExitInteraction;
            control.PlayerActions.Crouch.performed -= ExitInteraction;
        }
    }

    private void Interact(InputAction.CallbackContext A)
    {
        if (photonView.IsMine)
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
            }
        }
    }

    private void ExitInteraction(InputAction.CallbackContext context)
    {
        if (photonView.IsMine)
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
    }

    private IEnumerator BackToNormalView()
    {
        player.isInteracting = false;
        yield return new WaitForSeconds(3f);
        player.enabled = true;
    }

}