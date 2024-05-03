using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PuzzleCamara : PuzzlesFather
{
    Hud hud;
    [Header("Configuracion de las camaras")]
    [SerializeField] private GameObject controladorCamaras;
    public override void Interact()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
        hud.activado = true;
        Invoke("Activar", 2f);
    }

    public override void Exit()
    {
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
