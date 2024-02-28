using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5.0f;
    /*[SerializeField]
    private float gravityValue = -9.81f;*/

    //private CharacterController controller;
    //private Vector3 playerVelocity;
    private Rigidbody rb;

    private InputManager inputManager;
    [SerializeField] private Transform MyCameraTransform;



    private void Start()
    {
        //controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = MyCameraTransform.forward*move.z + MyCameraTransform.right * move.x;

        rb.MovePosition(transform.position + move * Time.deltaTime * playerSpeed);


    }
}
