using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionLight : MonoBehaviour
{
    ControllerLigth controller;

    private void Start()
    {
        controller = GetComponentInParent<ControllerLigth>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MovementLight"))
        {
            if (Input.GetKey(KeyCode.Space)) 
            {
                controller.Encender();
            }
        }
    }
}
