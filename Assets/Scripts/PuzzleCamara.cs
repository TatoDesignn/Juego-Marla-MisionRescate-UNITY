using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PuzzleCamara : PuzzlesFather
{
    public ControllerCamara controller;
    [SerializeField] private GameObject medidor;

    Hud hud;
    [Header("Configuracion de las camaras")]
    [SerializeField] private GameObject controladorCamaras;
    [SerializeField] private Animator animatorPuerta;


    public override void Interact()
    {
        controller.activo = true;
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
        hud.activado = true;
        Invoke("Activar", 2f);
    }

    public override void Exit()
    {
        animatorPuerta.SetTrigger("Rotar");
        controller.activo = false;
        hud.activado = false;
        Cursor.lockState = CursorLockMode.Locked;
        controladorCamaras.SetActive(false);
    }

    private void Activar()
    {
        controladorCamaras.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
}
