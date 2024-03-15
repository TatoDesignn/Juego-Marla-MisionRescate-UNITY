using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCamara : PuzzlesFather
{
    [Header("Configuracion de las camaras")]
    [SerializeField] private GameObject controladorCamaras;
    public override void Interact()
    {
        Invoke("Activar", 2f);
    }

    public override void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controladorCamaras.SetActive(false);
    }

    private void Activar()
    {
        controladorCamaras.SetActive(true);
    }
}
