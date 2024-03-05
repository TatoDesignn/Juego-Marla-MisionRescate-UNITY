using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
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
    private void OnEnable() //Photonview.ismine
    {
        control.PlayerActions.Interact.performed += Interact;
        control.PlayerActions.Interact.Enable();
        control.PlayerActions.Uninteract.performed += ExitInteraction;
        control.PlayerActions.Uninteract.Enable();
    }


    private void OnDisable() //Photonview.ismine
    {
       control.PlayerActions.Interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext A) //Photonview.ismine
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
    private void ExitInteraction(InputAction.CallbackContext context) //Photonview.ismine
    {
        Ray r = new Ray(rb.position, rb.transform.forward);
        if (Physics.Raycast(r, out RaycastHit hit, InteractRange))
        {
            if (hit.collider.gameObject.TryGetComponent(out PuzzlesFather interactObj))
            {
                interactObj.ChangeCamera(false);
                player.enabled = true;
            }
        }
    }

}
