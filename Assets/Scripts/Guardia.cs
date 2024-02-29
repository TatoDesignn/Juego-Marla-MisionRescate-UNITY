using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardia : MonoBehaviour
{
    Rigidbody rb;

    [Space]
    [Header("Configuracion del guardia")]
    public float velocity;
    public float distanceFront;
    public LayerMask capeFront;
    public Transform objectFront;

    [Space]
    [Header("Variables locales")]
    bool seeFront = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        Vector3 move = transform.forward * velocity * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        seeFront = Physics.Raycast(objectFront.position, transform.forward, distanceFront, capeFront);

        if (seeFront)
        {
            Spin();
        }
    }

    private void Spin()
    {
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + 180, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(objectFront.transform.position, objectFront.transform.position + transform.forward * distanceFront);
    }
}
