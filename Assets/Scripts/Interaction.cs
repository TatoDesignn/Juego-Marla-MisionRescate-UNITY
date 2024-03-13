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
    private Player player;
    private void Awake()
    {
        control = new InputControl();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }
    new private void OnEnable() //Photonview.ismine
    {
        if (photonView.IsMine)
        {
            control.PlayerActions.Interact.performed += Interact;
            control.PlayerActions.Interact.Enable();
            control.PlayerActions.Uninteract.performed += ExitInteraction;
            control.PlayerActions.Uninteract.Enable();
        }
    }


    new private void OnDisable() 
    {
        if(photonView.IsMine)
        {
            control.PlayerActions.Interact.performed -= Interact;
        }
    }

    private void Interact(InputAction.CallbackContext A) //Photonview.ismine
    {
        if (photonView.IsMine)
        {
            Ray r = new Ray(rb.position, rb.transform.forward);
            if(Physics.Raycast(r, out RaycastHit hit, InteractRange) ) 
            {
                if(hit.collider.gameObject.TryGetComponent(out PuzzlesFather interactObj))
                {
                    interactObj.ChangeCamera(true);
                    player.enabled = false;
                }
            }
        }
    }
    private void ExitInteraction(InputAction.CallbackContext context) //Photonview.ismine
    {
        if (photonView.IsMine)
        {
            Ray r = new Ray(rb.position, rb.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hit, InteractRange))
            {
                if (hit.collider.gameObject.TryGetComponent(out PuzzlesFather interactObj))
                {
                    interactObj.ChangeCamera(false);
                    StartCoroutine("BackToNormalView");
                }
            }
        }
    }

    private IEnumerator BackToNormalView()
    {
        yield return new WaitForSeconds(2f);
        player.enabled = true;
    }

}
