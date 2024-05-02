using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : PuzzlesFather
{
    [Header("Configuracion de las Puertas")]
    [SerializeField] private GameObject controladorDoor;
    [SerializeField] private GameObject controles;

    public bool acceder = true;

    public override void Interact()
    {
        Cursor.lockState = CursorLockMode.Confined;
        controladorDoor.SetActive(true);
    }

    public override void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controladorDoor.SetActive(false);
        controles.SetActive(false);

        if(!acceder)
        {
            Destroy(GetComponent<PuzzleDoor>());
        }
    }
}
