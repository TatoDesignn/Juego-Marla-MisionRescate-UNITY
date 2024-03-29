using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : PuzzlesFather
{
    [Header("Configuracion de las Puertas")]
    [SerializeField] private GameObject controladorDoor;
    public override void Interact()
    {
        Cursor.lockState = CursorLockMode.Confined;
        controladorDoor.SetActive(true);
    }

    public override void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controladorDoor.SetActive(false);
    }
}
