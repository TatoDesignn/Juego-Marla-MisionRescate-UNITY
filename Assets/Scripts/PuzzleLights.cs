using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PuzzleLights : PuzzlesFather
{
    [Header("Configuracion de las luces")]
    [SerializeField] private GameObject controladorLuces;
    [SerializeField] private GameObject controles;
    [SerializeField] private GameObject particulas;

    public bool acceder = true;

    public override void Interact()
    {
        controladorLuces.SetActive(true);
    }

    public override void Exit()
    {
        if(controladorLuces.activeInHierarchy)
        {
            controladorLuces.SetActive(false);
            controles.SetActive(false);

            if (!acceder)
            {
                particulas.SetActive(false);
                Destroy(GetComponent<PuzzleLights>());
            }
        }
    }
}
