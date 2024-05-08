using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medidor : MonoBehaviourPun
{
    [SerializeField] GameObject perder;

    public void PantallaPerder()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Comunicador.canMove = false;
        perder.SetActive(true);
    }
}
