using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PuzzleLights : PuzzlesFather
{
    [Header("Configuracion de las luces")]
    [SerializeField] private GameObject controladorLuces;

    public override void Interact()
    {
        controladorLuces.SetActive(true);
    }

    public override void Exit()
    {
        if(controladorLuces.activeInHierarchy)
        {
            controladorLuces.SetActive(false);
        }
    }
}
