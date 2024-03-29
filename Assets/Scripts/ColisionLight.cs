using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionLight : MonoBehaviour
{
    ControllerLigth controller;

    [Space]
    [Header("Variables locales:")]
    private bool activo;

    private void Start()
    {
        controller = GetComponentInParent<ControllerLigth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activo)
        {
            controller.Correctas();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MovementLight"))
        {
            activo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MovementLight"))
        {
            activo = false;
        }
    }
}
