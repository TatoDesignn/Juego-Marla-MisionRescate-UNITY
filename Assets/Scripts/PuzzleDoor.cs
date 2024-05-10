using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PuzzleDoor : PuzzlesFather
{
    Hud hud;
    [Header("Configuracion de las Puertas")]
    [SerializeField] private GameObject controladorDoor;
    [SerializeField] private GameObject controles;
    [SerializeField] private GameObject particulas;
    [SerializeField] private GameObject medidor;

    public bool acceder = true;

    public override void Interact()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
        hud.activado = true;
        Cursor.lockState = CursorLockMode.Confined;
        controladorDoor.SetActive(true);
        medidor.SetActive(false);
    }

    public override void Exit()
    {
        hud.activado = false;
        Cursor.lockState = CursorLockMode.Locked;
        controladorDoor.SetActive(false);
        controles.SetActive(false);
        medidor.SetActive(true);
        if (!acceder)
        {
            particulas.SetActive(false);
            Destroy(GetComponent<PuzzleDoor>());
        }
    }
}
